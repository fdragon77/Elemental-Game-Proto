Shader "UI/UIImage"
{
	Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        
        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15
        
        [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
        
        _TexSize ("Texture Size", Float) = 4096
        _CutoutThreshold ("Cutout Alpha Threshold", Range(0.0,1.0)) = 0.001
    }

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}
		
		Stencil
		{
			Ref [_Stencil]
			Comp [_StencilComp]
			Pass [_StencilOp] 
			ReadMask [_StencilReadMask]
			WriteMask [_StencilWriteMask]
		}

		Cull Off
        Lighting Off
        ZWrite Off //disable writing to the depth buffer  
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha //alpha blending (allows transparency) -> SourceColor * SourceAlpha + DestinationColor * OneMinusSourceAlpha
        ColorMask [_ColorMask]
        
		Pass
		{
		    Name "Default"
			CGPROGRAM
			#pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0
            //enable GPU Instancing (https://docs.unity3d.com/Manual/GPUInstancing.html)
            #pragma multi_compile_instancing 
            //prevent Unity from applying GPU Instancing to LOD fade values
            #pragma instancing_options nolodfade 
            //prevent Unity from applying GPU Instancing to Light Probe values (including their occlusion data). This is useful for performance if you are absolutely sure that there are no GameObjects using both GPU Instancing and Light Probes.
            #pragma instancing_options nolightprobe
            //prevent Unity from applying GPU Instancing to Lightmap ST (atlas information) values. This is useful for performance if you are absolutely sure that there are no GameObjects using both GPU Instancing and lightmaps.
            #pragma instancing_options nolightmap

            #pragma multi_compile __ UNITY_UI_CLIP_RECT
            #pragma multi_compile __ UNITY_UI_ALPHACLIP

            #include "UnityCG.cginc"
            #include "UnityUI.cginc"

		    sampler2D _MainTex;
			fixed4 _Color;
            fixed4 _TextureSampleAdd;
            float4 _ClipRect;
            float4 _MainTex_ST;
			float _TexSize;
            float _CutoutThreshold;
			
			struct VertexInput
			{
				float4 vertex   : POSITION; //local vertex position
				float4 color    : COLOR;
				float2 uv0      : TEXCOORD0;
				float2 uv1      : TEXCOORD1;
				float2 uv2      : TEXCOORD2;
				float2 uv3      : TEXCOORD3;
                UNITY_VERTEX_INPUT_INSTANCE_ID //necessary to access instanced properties in fragment Shader
			};

			struct VertexOutput
			{
				float4 vertex           : POSITION; 
				fixed4 color            : COLOR;
				float4 worldPosition    : TEXCOORD0; //worldPosition for each corner
				float4 radius           : TEXCOORD1; //radius for each corner
				float2 texcoord         : TEXCOORD2;
				float2 size             : TEXCOORD3; //width and height
				float border            : TEXCOORD4; //border
				float pixelWorldScale   : TEXCOORD5; //scaled size
                UNITY_VERTEX_OUTPUT_STEREO
			};
			
		    #define f3_f(c) (dot(round((c) * 255), float3(65536, 256, 1)))
            #define f_f3(f) (frac((f) / float3(16777216, 65536, 256)))

            // https://github.com/TwoTailsGames/Unity-Built-in-Shaders/blob/master/CGIncludes/UnityCG.cginc [Line: 610]
            // Encode [0..1) floats into 256 bit/channel. Note that 1.0 will not be encoded properly.
			float2 EncodeFloat(float value) 
			{
				float2 kEncodeMul = float2(1.0, 65535.0f);
				float kEncodeBit = 1.0 / 65535.0f;
				float2 enc = kEncodeMul * value;
				enc = frac(enc);
				enc.x -= enc.y * kEncodeBit;
				return enc;
			}
			
			VertexOutput vert(VertexInput input)
			{
				VertexOutput output;
                UNITY_SETUP_INSTANCE_ID(input); //necessary to access instanced properties in fragment Shader
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
				output.worldPosition = input.vertex;
				output.vertex = UnityObjectToClipPos(output.worldPosition);
				
				output.size = input.uv1;
				output.texcoord = TRANSFORM_TEX(input.uv0, _MainTex);

				float minside = min(output.size.x, output.size.y);

				output.border = input.uv3.x*minside/2;

				output.radius = float4(EncodeFloat(input.uv2.x), EncodeFloat(input.uv2.y)) * minside;
								
				output.pixelWorldScale = clamp(input.uv3.y,1/_TexSize,_TexSize);
				
                output.color = input.color * _Color;
				return output;
			}
			
			half visible(float2 pos,float4 radius,float2 size)
			{
				half4 p = half4(pos,
				                size.x-pos.x,
				                size.y-pos.y);
				                
				half v = min(min(min(p.x,p.y),p.z),p.w);
				
				bool4 b = bool4(all(p.xw<radius[0]),
				                all(p.zw<radius[1]),
				                all(p.zy<radius[2]),
				                all(p.xy<radius[3]));

                ///////////////////
                ///////////////////
			    //ROUND
			    // 1 2
				// 4 3
				
				half p1 = length(p.xw-radius[0]);
				half p2 = length(p.zw-radius[1]);
				half p3 = length(p.zy-radius[2]);
				half p4 = length(p.xy-radius[3]);
				
				half4 vis = radius-half4(p1,p2,p3,p4);
				half4 foo = min(b*max(vis,0),v)+(1-b)*v;
				v = any(b)*min(min(min(foo.x,foo.y),foo.z),foo.w)+v*(1-any(b));
				
				///////////////////
				///////////////////
				//INVERTED ROUND
				// 1 2
				// 4 3
				
				//half p1 = length(p.xw);
				//half p2 = length(p.zw);
				//half p3 = length(p.zy);
				//half p4 = length(p.xy);
				//
				//
				//half4 vis = radius-half4(p1,p2,p3,p4);
				//half4 foo = min(b-max(vis,0),v)+(1-b)*v;
				//v = any(b)*min(min(min(foo.x,foo.y),foo.z),foo.w)+v*(1-any(b));
				
				///////////////////
				///////////////////
				//CHAMFER
				// 1 2
				// 4 3
								
				//half p1 = length(p.xw+radius[0]);
				//half p2 = length(p.zw+radius[1]);
				//half p3 = length(p.zy+radius[2]);
				//half p4 = length(p.xy+radius[3]);
				
				//half4 vis = radius - half4(p1,p2,p3,p4);
				//half4 foo = min(b*max(vis,0),v)+(1-b)*v;
				//v = any(b)*min(min(min(foo.x,foo.y),foo.z),foo.w)+v*(1-any(b));
				
				return v;
			}

			fixed4 frag (VertexOutput output) : SV_Target
			{
                half4 color = (tex2D(_MainTex, output.texcoord) + _TextureSampleAdd) * output.color;

				#ifdef UNITY_UI_CLIP_RECT
                color.a *= UnityGet2DClipping(output.worldPosition.xy, _ClipRect);
                #endif

                #ifdef UNITY_UI_ALPHACLIP
                clip (color.a - _CutoutThreshold); //clip any pixels that have the alpha less than _CutoutThreshold
                #endif

				half v = visible(output.texcoord*output.size,output.radius,output.size); //body
				float l = (output.border+1/output.pixelWorldScale)/2; //border
				color.a *= saturate((l-distance(v,l))*output.pixelWorldScale); //feather
				
				if(color.a <= _CutoutThreshold) discard; //discard any pixels that have the alpha less than _CutoutThreshold
				//if(color.a <= _CutoutThreshold) color = float4(1, 0, 0, 0.2);

				return color;
			}
			ENDCG
		}
	}
}

