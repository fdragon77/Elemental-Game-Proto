// Copyright (c) 2015 - 2019 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using Doozy.Engine.Utils;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

// ReSharper disable DelegateSubtraction
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global

namespace Doozy.Engine.UI
{
    /// <inheritdoc />
    /// <summary>
    /// Displays a Sprite for the UI System just like the Image component, but it has a few extra settings like corner radius (rounded corners), feather (soft edges) and border width (display as a border).
    /// </summary>
    [AddComponentMenu(MenuUtils.UIImage_AddComponentMenu_MenuName, MenuUtils.UIImage_AddComponentMenu_Order)]
    [RequireComponent(typeof(RectTransform))]
    [ExecuteInEditMode]
    [DefaultExecutionOrder(DoozyExecutionOrder.UIIMAGE)]
    public class UIImage : Image
    {
        #region UNITY_EDITOR

#if UNITY_EDITOR
        [MenuItem(MenuUtils.UIImage_MenuItem_ItemName, false, MenuUtils.UIImage_MenuItem_Priority)]
        private static void CreateComponent(MenuCommand menuCommand) { CreateUIImage(GetParent(menuCommand.context as GameObject)); }

        /// <summary> (EDITOR ONLY) Creates a UIImage and returns a reference to it </summary>
        public static UIImage CreateUIImage(GameObject parent)
        {
            var go = new GameObject(MenuUtils.UIImage_GameObject_Name, typeof(RectTransform), typeof(UIImage));
            GameObjectUtility.SetParentAndAlign(go, parent);
            Undo.RegisterCreatedObjectUndo(go, "Create " + go.name); //undo option
            var uiImage = go.GetComponent<UIImage>();
            uiImage.UpdateRenderCanvasShaderChannels();
            uiImage.Init();
            SceneView.RepaintAll();
            Selection.activeObject = go; //select the newly created gameObject
            return uiImage;
        }

        /// <summary>
        ///     Method used when creating an UIComponent.
        ///     It looks if the selected object has a RectTransform (and returns it as the parent).
        ///     If the selected object is null or does not have a RectTransform, it returns the MasterCanvas GameObject as the parent.
        /// </summary>
        /// <param name="selectedObject"> Selected object </param>
        private static GameObject GetParent(GameObject selectedObject)
        {
            if (selectedObject == null) return UICanvas.GetMasterCanvas().gameObject; //selected object is null -> returns the MasterCanvas GameObject
            return selectedObject.GetComponent<RectTransform>() != null ? selectedObject : UICanvas.GetMasterCanvas().gameObject;
        }

        /// <summary> Convert an Image component to an UIImage component </summary>
        [MenuItem("CONTEXT/Image/Convert to UIImage")]
        public static void ConvertToUIImage(MenuCommand command)
        {
            if (command.context is UIImage) return;

            var image = (Image) command.context;
            GameObject targetObject = image.gameObject;

            Color color = image.color;
            bool raycastTarget = image.raycastTarget;
            Sprite sprite = image.sprite;
            Type type = image.type;
            bool preserveAspect = image.preserveAspect;
            float fillAmount = image.fillAmount;
            bool fillCenter = image.fillCenter;
            FillMethod fillMethod = image.fillMethod;
            bool fillClockwise = image.fillClockwise;
            int fillOrigin = image.fillOrigin;
            float alphaHitTestMinimumThreshold = image.alphaHitTestMinimumThreshold;

            DestroyImmediate(image);

            var uiImage = targetObject.AddComponent<UIImage>();

            uiImage.color = color;
            uiImage.raycastTarget = raycastTarget;
            if (sprite != null) uiImage.sprite = sprite;
            uiImage.type = type;
            uiImage.preserveAspect = preserveAspect;
            uiImage.fillAmount = fillAmount;
            uiImage.fillCenter = fillCenter;
            uiImage.fillMethod = fillMethod;
            uiImage.fillClockwise = fillClockwise;
            uiImage.fillOrigin = fillOrigin;
            uiImage.alphaHitTestMinimumThreshold = alphaHitTestMinimumThreshold;
            uiImage.UpdateRenderCanvasShaderChannels();
            uiImage.Init();

            SceneView.RepaintAll();
        }

        /// <summary> Convert an UIImage component to an Image component </summary>
        [MenuItem("CONTEXT/UIImage/Convert to Image")]
        public static void ReplaceWithImage(MenuCommand command)
        {
            if (!(command.context is UIImage)) return;

            var uiImage = (UIImage) command.context;
            GameObject targetObject = uiImage.gameObject;

            Color color = uiImage.color;
            bool raycastTarget = uiImage.raycastTarget;
            Sprite sprite = uiImage.sprite;
            Type type = uiImage.type;
            bool preserveAspect = uiImage.preserveAspect;
            float fillAmount = uiImage.fillAmount;
            bool fillCenter = uiImage.fillCenter;
            FillMethod fillMethod = uiImage.fillMethod;
            bool fillClockwise = uiImage.fillClockwise;
            int fillOrigin = uiImage.fillOrigin;
            float alphaHitTestMinimumThreshold = uiImage.alphaHitTestMinimumThreshold;

            DestroyImmediate(uiImage);

            var image = targetObject.AddComponent<Image>();

            image.color = color;
            image.raycastTarget = raycastTarget;
            if (sprite != null) image.sprite = sprite;
            image.type = type;
            image.preserveAspect = preserveAspect;
            image.fillAmount = fillAmount;
            image.fillCenter = fillCenter;
            image.fillMethod = fillMethod;
            image.fillClockwise = fillClockwise;
            image.fillOrigin = fillOrigin;
            image.alphaHitTestMinimumThreshold = alphaHitTestMinimumThreshold;

            SceneView.RepaintAll();
        }
#endif

        #endregion

        #region Constants

        private const float TWO_AT_SIXTEEN = 65535.0f;
        private const float DEFAULT_BORDER_WIDTH = 0;
        private const float DEFAULT_FEATHER = 0f;
        private const bool DEFAULT_FEATHER_EXPAND_SIZE = true;

        private const string DEFAULT_SPRITE_NAME = "UIImagePolygon";
        private const string DEFAULT_MATERIAL_NAME = "UIImageMaterial";
        private const string DEFAULT_SHADER_NAME = "UI/UIImage";

        #endregion

        #region Static Properties

        /// <summary> Internal static variable that keeps a reference to the default sprite assigned to all UIImages </summary>
        private static Sprite s_defaultSprite;
        
        /// <summary> Internal static variable that keeps a reference to the default material shared by all UIImages </summary>
        private static Material s_materialInstance;

        private static Vector2 DecodeDot { get { return new Vector2(1f, 1f / TWO_AT_SIXTEEN); } }
        
        /// <summary> Default Sprite assigned to all UIImages if the sprite reference is null </summary>
        public static Sprite DefaultSprite
        {
            get
            {
                if (s_defaultSprite != null) return s_defaultSprite;
                s_defaultSprite = Resources.Load<Sprite>(DEFAULT_SPRITE_NAME);
                return s_defaultSprite;
            }
        }

        /// <summary> Default Material shared by all UIImages </summary>
        public static Material MaterialInstance
        {
            get
            {
                if (s_materialInstance != null) return s_materialInstance;
                s_materialInstance = Resources.Load<Material>(DEFAULT_MATERIAL_NAME);
                Shader shader = Shader.Find(DEFAULT_SHADER_NAME);
                if (s_materialInstance == null) s_materialInstance = new Material(shader) {name = DEFAULT_MATERIAL_NAME};
                s_materialInstance.enableInstancing = true;
                s_materialInstance.shader = shader;
                return s_materialInstance;
            }
        }

        #endregion

        #region Properties

        /// <summary> Border width for the UIImage. Default value is 0 (no border) </summary>
        public float BorderWidth
        {
            get { return m_borderWidth; }
            set
            {
                m_borderWidth = Mathf.Max(0, value);
                SetVerticesDirty();
            }
        }

    /// <summary> Set the image feather (also known as 'soft edges'). Used to adjust harsh edges for graphics by applying a blur effect. </summary>
        public float Feather
        {
            get { return m_feather; }
            set
            {
                m_feather = Mathf.Max(0, value);
                SetVerticesDirty();
            }
        }

        /// <summary> Get the radius for all the 4 corners (starting upper-left, clockwise) </summary>
        public Vector4 Radius
        {
            get
            {
                return new Vector4(m_topLeftCorner.Radius,
                                   m_topRightCorner.Radius,
                                   m_bottomRightCorner.Radius,
                                   m_bottomLeftCorner.Radius);
            }
        }

        /// <summary> Reference to the RectTransform component </summary>
        public RectTransform RectTransform
        {
            get
            {
                if (m_rectTransform == null) m_rectTransform = GetComponent<RectTransform>();

                return m_rectTransform;
            }
        }

        #endregion

        #region Private Variables

        /// <summary> Settings for the Top Left corner. Corner 1 </summary>
        [SerializeField]
        private UIImageCorner m_topLeftCorner;

        /// <summary> Settings for the Top Right corner. Corner 2 </summary>
        [SerializeField]
        private UIImageCorner m_topRightCorner;

        /// <summary> Settings for the Bottom Right corner. Corner 3 </summary>
        [SerializeField]
        private UIImageCorner m_bottomRightCorner;

        /// <summary> Settings for the Bottom Left corner. Corner 4 </summary>
        [SerializeField]
        private UIImageCorner m_bottomLeftCorner;

        /// <summary> Set the border width </summary>
        [SerializeField]
        private float m_borderWidth = DEFAULT_BORDER_WIDTH;

        /// <summary> Set the image feather (also known as 'soft edges'). Used to adjust harsh edges for graphics by applying a blur effect </summary>
        [SerializeField]
        private float m_feather = DEFAULT_FEATHER;

        /// <summary> Toggle if the feather effect should expand beyond image size or not </summary>
        [SerializeField]
        private bool m_featherExpandsSize = DEFAULT_FEATHER_EXPAND_SIZE;

        /// <summary> Internal variable that holds a reference to the RectTransform component </summary>
        private RectTransform m_rectTransform;

        #endregion

        #region Unity Methods

        protected override void Awake()
        {
            base.Awake();
            UpdateRenderCanvasShaderChannels();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            m_OnDirtyVertsCallback += OnDirtyVertices;
            Init();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            m_OnDirtyVertsCallback -= OnDirtyVertices;
        }

#if UNITY_EDITOR

        protected override void Reset()
        {
            base.Reset();
            OnEnable();
        }

        public void Update()
        {
            if (Application.isPlaying) return;
            UpdateGeometry();
        }

        protected override void OnValidate()
        {
            base.OnValidate();

            m_topLeftCorner.ValidateRadius();
            m_topRightCorner.ValidateRadius();
            m_bottomRightCorner.ValidateRadius();
            m_bottomLeftCorner.ValidateRadius();

            m_borderWidth = Mathf.Max(0, m_borderWidth);
            m_feather = Mathf.Max(0, m_feather);

            UpdateRenderCanvasShaderChannels();
        }
#endif

        /// <summary> Callback function when a UI element needs to generate vertices </summary>
        /// <param name="vh"> VertexHelper utility </param>
        protected override void OnPopulateMesh(VertexHelper vh)
        {
            base.OnPopulateMesh(vh);
            EncodeDataToVertices(vh, GetUIImageData());
        }

        #endregion

        #region Public Methods

        /// <summary> Initialize the UIImage </summary>
        public void Init()
        {
            ValidateSprite();
            CreateMaterial();
        }


        /// <summary> Add additional shader channel to the Canvas that renders this UIImage </summary>
        public void UpdateRenderCanvasShaderChannels()
        {
            if (canvas == null) return;
            Canvas renderCanvas = canvas;
            AdditionalCanvasShaderChannels additionalShaderChannels = renderCanvas.additionalShaderChannels;
            additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord1;
            additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord2;
            additionalShaderChannels |= AdditionalCanvasShaderChannels.TexCoord3;
            renderCanvas.additionalShaderChannels = additionalShaderChannels;
        }

        #endregion

        #region Private Methods

        /// <summary> Calculate the adjusted Radius according to the current settings </summary>
        /// <param name="rect"> Pixel adjusted rect </param>
        /// <returns> Returns the calculated radius values as a Vector4 (for each corner starting upper-left, clockwise)</returns>
        private Vector4 CalculateRadius(Rect rect) { return Radius; }

        /// <summary> Creates a material if missing </summary>
        private void CreateMaterial()
        {
            s_materialInstance = MaterialInstance;
            if (material != MaterialInstance)
                material = MaterialInstance;
        }

        /// <summary> Get an UIImageData with the current settings </summary>
        private UIImageData GetUIImageData()
        {
            var corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);

            Rect pixelAdjustedRect = GetPixelAdjustedRect();
            float pixelSize = Vector3.Distance(corners[1], corners[2]) / pixelAdjustedRect.width;

            pixelSize /= Mathf.Max(0, Feather);

            Vector4 radius = ValidateRadius(CalculateRadius(pixelAdjustedRect));
            float minimumSize = Mathf.Min(pixelAdjustedRect.width,
                                          pixelAdjustedRect.height);

            return new UIImageData(pixelAdjustedRect.width + Feather,
                                   pixelAdjustedRect.height + Feather,
                                   radius / minimumSize,
                                   BorderWidth / minimumSize * 2,
                                   Feather,
                                   pixelSize);
        }
        
        /// <summary> Inject the UIImage data into the additional shader channels </summary>
        /// <param name="vh"> VertexHelper utility </param>
        /// <param name="data"> UIImageData that contains the current settings </param>
        private void EncodeDataToVertices(VertexHelper vh, UIImageData data)
        {
            var vert = new UIVertex();
            var uv1 = new Vector2(data.Width, data.Height);                                                                //size
            var uv2 = new Vector2(EncodeFloats(data.Radius.x, data.Radius.y), EncodeFloats(data.Radius.z, data.Radius.w)); //radius
            var uv3 = new Vector2(data.BorderWidth > 0 ? Mathf.Clamp01(data.BorderWidth) : 1, data.PixelSize);             //border and pixel size

            for (int i = 0; i < vh.currentVertCount; i++)
            {
                vh.PopulateUIVertex(ref vert, i);
                if (m_featherExpandsSize)
                {
                    vert.position += ((Vector3) vert.uv0 - new Vector3(0.5f, 0.5f)) * data.Feather;
                }

                vert.uv1 = uv1;
                vert.uv2 = uv2;
                vert.uv3 = uv3;
                vh.SetUIVertex(vert, i);
            }
        }
        
        /// <summary> Whenever the the vertices are marked as dirty a sprite validation is in order </summary>
        private void OnDirtyVertices() { ValidateSprite(); }

        /// <summary> Prevent radius to get bigger than rect size by validating the values </summary>
        /// <returns> Returns the adjusted values for the corners radius </returns>
        /// <param name="cornerRadius"> Border-radius as Vector4 (starting upper-left, clockwise)</param>
        // 1 2
        // 4 3
        private Vector4 ValidateRadius(Vector4 cornerRadius)
        {
            cornerRadius = new Vector4(Mathf.Max(0, cornerRadius.x),
                                       Mathf.Max(0, cornerRadius.y),
                                       Mathf.Max(0, cornerRadius.z),
                                       Mathf.Max(0, cornerRadius.w));

            Rect rect = RectTransform.rect;
            float scaleFactor = Mathf.Min(rect.width / (cornerRadius.x + cornerRadius.y),
                                          rect.width / (cornerRadius.z + cornerRadius.w),
                                          rect.height / (cornerRadius.x + cornerRadius.w),
                                          rect.height / (cornerRadius.z + cornerRadius.y),
                                          1);

            return cornerRadius * scaleFactor;
        }
        
        /// <summary> Check that the sprite reference is not null. If null, it will assign the default sprite for UIImages </summary>
        private void ValidateSprite()
        {
            if (sprite == null) sprite = DefaultSprite;
        }

        #endregion
        
        #region Static Methods
        
        /// <summary> Encode two values between [0,1] into a single float (using 16 bits). (0-1 16 16) </summary>
        /// <param name="a"> Value A </param>
        /// <param name="b"> Value B </param>
        private static float EncodeFloats(float a, float b)
        {
            return Vector2.Dot(new Vector2(Mathf.Floor(a * 65534) / 65535f,
                                           Mathf.Floor(b * 65534) / 65535f),
                               DecodeDot);
        }
        
        #endregion
    }
}