// Copyright (c) 2015 - 2019 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using System;
using Doozy.Editor.Internal;
using Doozy.Editor.Utils;
using Doozy.Engine.UI;
using Doozy.Engine.Utils;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;
using UnityEngine.UI;

namespace Doozy.Editor.UI
{
    [CustomEditor(typeof(UIImage))]
    [CanEditMultipleObjects]
    public class UIImageEditor : BaseEditor
    {
        public enum Range
        {
            Small,
            Medium,
            Large,
            Custom
        }

        protected override ColorName ComponentColorName { get { return DGUI.Colors.UIImageColorName; } }
        private UIImage m_target;

        private UIImage Target
        {
            get
            {
                if (m_target != null) return m_target;
                m_target = (UIImage) target;
                return m_target;
            }
        }

        public Range FeatherRange = Range.Small;
        private float m_maxFeatherValue = 1;
        private bool m_lockCorners;

        private float MaxCornerValue
        {
            get
            {
                Rect rect = Target.RectTransform.rect;
                return Mathf.Max(rect.width, rect.height) + (m_featherExpandsSize.boolValue ? m_feather.floatValue : 0);
            }
        }


        private SerializedProperty
            m_color,
            m_raycastTarget,
            m_material,
            m_type,
            m_preserveAspect,
            m_fillCenter,
            m_fillMethod,
            m_fillAmount,
            m_fillClockwise,
//            m_alphaHitTestMinimumThreshold,
            m_sprite,
//            m_overrideSprite,
            m_fillOrigin,
            m_topLeftCorner,
            m_topRightCorner,
            m_bottomRightCorner,
            m_bottomLeftCorner,
            m_borderWidth,
            m_feather,
            m_featherExpandsSize;

        private SerializedProperty m_topLeftRadius;
        private SerializedProperty m_topRightRadius;
        private SerializedProperty m_bottomLeftRadius;
        private SerializedProperty m_bottomRightRadius;

        private AnimBool
            m_imageTypeFilledExpanded;

        protected override void LoadSerializedProperty()
        {
            base.LoadSerializedProperty();

            m_color = GetProperty(PropertyName.m_Color);
            m_raycastTarget = GetProperty(PropertyName.m_RaycastTarget);
            m_material = GetProperty(PropertyName.m_Material);
            m_type = GetProperty(PropertyName.m_Type);
            m_preserveAspect = GetProperty(PropertyName.m_PreserveAspect);
            m_fillCenter = GetProperty(PropertyName.m_FillCenter);
            m_fillMethod = GetProperty(PropertyName.m_FillMethod);
            m_fillAmount = GetProperty(PropertyName.m_FillAmount);
            m_fillClockwise = GetProperty(PropertyName.m_FillClockwise);
//            m_alphaHitTestMinimumThreshold = GetProperty(PropertyName.m_AlphaHitTestMinimumThreshold);
            m_sprite = GetProperty(PropertyName.m_Sprite);
//            m_overrideSprite = GetProperty(PropertyName.m_OverrideSprite);
            m_fillOrigin = GetProperty(PropertyName.m_FillOrigin);
            m_topLeftCorner = GetProperty(PropertyName.m_topLeftCorner);
            m_topRightCorner = GetProperty(PropertyName.m_topRightCorner);
            m_bottomRightCorner = GetProperty(PropertyName.m_bottomRightCorner);
            m_bottomLeftCorner = GetProperty(PropertyName.m_bottomLeftCorner);
            m_borderWidth = GetProperty(PropertyName.m_borderWidth);
            m_feather = GetProperty(PropertyName.m_feather);
            m_featherExpandsSize = GetProperty(PropertyName.m_featherExpandsSize);

            m_topLeftRadius = m_topLeftCorner.FindPropertyRelative("m_radius");
            m_topRightRadius = m_topRightCorner.FindPropertyRelative("m_radius");
            m_bottomLeftRadius = m_bottomLeftCorner.FindPropertyRelative("m_radius");
            m_bottomRightRadius = m_bottomRightCorner.FindPropertyRelative("m_radius");
        }

        protected override void InitAnimBool()
        {
            base.InitAnimBool();

            m_imageTypeFilledExpanded = GetAnimBool("ImageTypeFilled", (Image.Type) m_type.enumValueIndex == Image.Type.Filled);
        }

        public override bool RequiresConstantRepaint() { return true; }

        /// <summary> Can this component be Previewed in its current state? </summary>
        /// <returns> True if this component can be Previewed in its current state </returns>
        public override bool HasPreviewGUI() { return true; }

        /// <summary> Custom preview for Image component </summary>
        /// <param name="rect"> Rectangle in which to draw the preview </param>
        /// <param name="background"> Background image </param>
        public override void OnPreviewGUI(Rect rect, GUIStyle background)
        {
            var image = target as Image;
            if (image == null)
                return;
            Sprite sprite = image.sprite;
            if (sprite == null)
                return;
            SpriteDrawUtils.DrawSprite(sprite, rect, image.canvasRenderer.GetColor());
        }

        /// <summary> A string containing the Image details to be used as a overlay on the component Preview </summary>
        /// <returns> The Image details </returns>
        public override string GetInfoString()
        {
            Sprite sprite = ((Image) target).sprite;
            return string.Format("Image Size: {0}x{1}", !(sprite != null) ? 0 : Mathf.RoundToInt(sprite.rect.width), !(sprite != null) ? 0 : Mathf.RoundToInt(sprite.rect.height));
        }

        
        protected override void OnEnable()
        {
            base.OnEnable();
            Target.Init();
            Target.UpdateRenderCanvasShaderChannels();
            UpdateCornersRadius();
            UpdateFeatherRange();
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            DrawHeader(Styles.GetStyle(Styles.StyleName.ComponentHeaderUIImage), MenuUtils.UIImage_Manual, MenuUtils.UIImage_YouTube);
            GUILayout.Space(DGUI.Properties.Space(2));
            DrawSourceImageAndMaterial();
            GUILayout.Space(DGUI.Properties.Space());
            DrawRaycastTargetAndColor();
            GUILayout.Space(DGUI.Properties.Space());
            DrawImageType();
            DrawFilledOptions();
            GUILayout.Space(DGUI.Properties.Space(4));
            DrawCorners();
            GUILayout.Space(DGUI.Properties.Space(4));
            DrawFeather();
            GUILayout.Space(DGUI.Properties.Space());
            DrawBorderWidth();
            GUILayout.Space(DGUI.Properties.Space(4));
            serializedObject.ApplyModifiedProperties();
        }

        private void DrawSourceImageAndMaterial()
        {
            EditorGUILayout.BeginHorizontal();
            DGUI.Property.Draw(m_sprite, UILabels.SourceImage, ComponentColorName, ComponentColorName);
            GUILayout.Space(DGUI.Properties.Space());
            DGUI.Property.Draw(m_material, UILabels.Material, ComponentColorName, ComponentColorName);
            EditorGUILayout.EndHorizontal();
        }

        private void DrawRaycastTargetAndColor()
        {
            EditorGUILayout.BeginHorizontal();
            DGUI.Toggle.Switch.Draw(m_raycastTarget, UILabels.RaycastTarget, ComponentColorName, true, false);
            GUILayout.Space(DGUI.Properties.Space());
            DGUI.Property.Draw(m_color, UILabels.Color, ComponentColorName, ComponentColorName);
            EditorGUILayout.EndHorizontal();
        }

        private void DrawImageType()
        {
            EditorGUILayout.BeginHorizontal();
            DGUI.Property.Draw(m_type, UILabels.ImageType, ComponentColorName, ComponentColorName);
            switch ((Image.Type) m_type.enumValueIndex)
            {
                case Image.Type.Simple:
                case Image.Type.Filled:
                    DGUI.Toggle.Switch.Draw(m_preserveAspect, UILabels.PreserveAspect, ComponentColorName, true, false);
                    break;
                case Image.Type.Sliced:
                case Image.Type.Tiled:
                    DGUI.Toggle.Switch.Draw(m_fillCenter, UILabels.FillCenter, ComponentColorName, true, false);
                    break;
            }

            EditorGUILayout.EndHorizontal();
        }

        private void DrawFillMethodAndFillOrigin()
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUI.BeginChangeCheck();
                DGUI.Property.Draw(m_fillMethod, UILabels.FillMethod, ComponentColorName, ComponentColorName);
                if (EditorGUI.EndChangeCheck())
                    m_fillOrigin.intValue = 0;
                GUILayout.Space(DGUI.Properties.Space());
                DGUI.Line.Draw(false, ComponentColorName, true,
                               () =>
                               {
                                   GUILayout.Space(DGUI.Properties.Space(2));
                                   DGUI.Label.Draw(UILabels.FillOrigin, Size.S, ComponentColorName, DGUI.Properties.SingleLineHeight);
                                   GUILayout.Space(DGUI.Properties.Space());
                                   GUI.color = DGUI.Colors.PropertyColor(ComponentColorName);
                                   EditorGUILayout.BeginVertical();
                                   GUILayout.Space(0);
                                   switch (m_fillMethod.enumValueIndex)
                                   {
                                       case 0:
                                           m_fillOrigin.intValue = (int) (Image.OriginHorizontal) EditorGUILayout.EnumPopup((Image.OriginHorizontal) m_fillOrigin.intValue);
                                           break;
                                       case 1:
                                           m_fillOrigin.intValue = (int) (Image.OriginVertical) EditorGUILayout.EnumPopup((Image.OriginVertical) m_fillOrigin.intValue);
                                           break;
                                       case 2:
                                           m_fillOrigin.intValue = (int) (Image.Origin90) EditorGUILayout.EnumPopup((Image.Origin90) m_fillOrigin.intValue);
                                           break;
                                       case 3:
                                           m_fillOrigin.intValue = (int) (Image.Origin180) EditorGUILayout.EnumPopup((Image.Origin180) m_fillOrigin.intValue);
                                           break;
                                       case 4:
                                           m_fillOrigin.intValue = (int) (Image.Origin360) EditorGUILayout.EnumPopup((Image.Origin360) m_fillOrigin.intValue);
                                           break;
                                   }

                                   EditorGUILayout.EndVertical();
                                   GUI.color = InitialGUIColor;
                                   GUILayout.Space(DGUI.Properties.Space(2));
                               });
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawFillAmountAndClockwise()
        {
            EditorGUILayout.BeginHorizontal();
            {
                DGUI.Property.Draw(m_fillAmount, UILabels.FillAmount, ComponentColorName);
                if (m_fillMethod.enumValueIndex > 1)
                {
                    GUILayout.Space(DGUI.Properties.Space());
                    DGUI.Toggle.Switch.Draw(m_fillClockwise, UILabels.Clockwise, ComponentColorName, true, false);
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawFilledOptions()
        {
            m_imageTypeFilledExpanded.target = (Image.Type) m_type.enumValueIndex == Image.Type.Filled;
            if (DGUI.FoldOut.Begin(m_imageTypeFilledExpanded, false))
            {
                GUILayout.Space(DGUI.Properties.Space() * m_imageTypeFilledExpanded.faded);
                DrawFillMethodAndFillOrigin();
                GUILayout.Space(DGUI.Properties.Space() * m_imageTypeFilledExpanded.faded);
                DrawFillAmountAndClockwise();
            }

            DGUI.FoldOut.End(m_imageTypeFilledExpanded, false);
        }

        private void DrawFeather()
        {
            EditorGUILayout.BeginHorizontal();
            {
                DGUI.Icon.Draw(Styles.GetStyle(Styles.StyleName.IconFeather), DGUI.Properties.SingleLineHeight * 0.8f, DGUI.Properties.SingleLineHeight, ComponentColorName);
                GUILayout.Space(DGUI.Properties.Space(2));
                switch (FeatherRange)
                {
                    case Range.Small:
                    case Range.Medium:
                    case Range.Large:
                        DGUI.Line.Draw(false, ComponentColorName, true,
                                       () =>
                                       {
                                           GUILayout.Space(DGUI.Properties.Space(2));
                                           DGUI.Label.Draw(UILabels.Feather, Size.S, ComponentColorName, DGUI.Properties.SingleLineHeight);
                                           GUILayout.Space(DGUI.Properties.Space());
                                           GUI.color = DGUI.Colors.PropertyColor(ComponentColorName);
                                           EditorGUILayout.BeginVertical();
                                           GUILayout.Space(0);
                                           m_feather.floatValue = EditorGUILayout.Slider(m_feather.floatValue, 0, m_maxFeatherValue);
                                           EditorGUILayout.EndVertical();
                                           GUI.color = InitialGUIColor;
                                       });
                        break;
                    default:
                        DGUI.Property.Draw(m_feather, UILabels.Feather, ComponentColorName);
                        break;
                }

                GUILayout.Space(DGUI.Properties.Space());
                DGUI.Line.Draw(false, ComponentColorName, true,
                               () =>
                               {
                                   GUILayout.Space(DGUI.Properties.Space(2));
                                   DGUI.Label.Draw(UILabels.Range, Size.S, ComponentColorName, DGUI.Properties.SingleLineHeight);
                                   GUI.color = DGUI.Colors.PropertyColor(ComponentColorName);
                                   EditorGUI.BeginChangeCheck();
                                   EditorGUILayout.BeginVertical();
                                   GUILayout.Space(0);
                                   FeatherRange = (Range) EditorGUILayout.EnumPopup(FeatherRange, GUILayout.Width(DGUI.Properties.DefaultFieldWidth * 2));
                                   EditorGUILayout.EndVertical();
                                   if (EditorGUI.EndChangeCheck())
                                   {
                                       SetFeatherRange(FeatherRange);
                                   }

                                   GUI.color = InitialGUIColor;
                               });
                GUILayout.Space(DGUI.Properties.Space());
                DGUI.Toggle.Switch.Draw(m_featherExpandsSize, UILabels.FeatherExpandsSize, ComponentColorName, true, false);
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawBorderWidth()
        {
            EditorGUILayout.BeginHorizontal();

            DGUI.Icon.Draw(Styles.GetStyle(Styles.StyleName.IconBorder), DGUI.Properties.SingleLineHeight * 0.8f, DGUI.Properties.SingleLineHeight, ComponentColorName);
            GUILayout.Space(DGUI.Properties.Space(2));

            DGUI.Line.Draw(false, ComponentColorName, true,
                           () =>
                           {
                               GUILayout.Space(DGUI.Properties.Space(2));
                               DGUI.Label.Draw(UILabels.BorderWidth, Size.S, ComponentColorName, DGUI.Properties.SingleLineHeight);
                               GUILayout.Space(DGUI.Properties.Space());
                               GUI.color = DGUI.Colors.PropertyColor(ComponentColorName);
                               EditorGUILayout.BeginVertical();
                               GUILayout.Space(0);
                               m_borderWidth.floatValue = EditorGUILayout.Slider(m_borderWidth.floatValue, 0, Target.RectTransform.rect.width / 2);
                               EditorGUILayout.EndVertical();
                               GUI.color = InitialGUIColor;
                           });

            EditorGUILayout.EndHorizontal();
        }

        private void DrawCorners()
        {
            float maxCornerValue = MaxCornerValue;
            float cornerIconSize = DGUI.Properties.SingleLineHeight * 0.8f;
            ColorName cornerColorName = m_lockCorners ? ComponentColorName : DGUI.Colors.DisabledIconColorName;

            EditorGUILayout.BeginHorizontal();
            {
                DrawCorner(m_topLeftRadius, maxCornerValue);
                GUILayout.Space(DGUI.Properties.Space(4));
                DGUI.Icon.Draw(Styles.GetStyle(Styles.StyleName.IconRoundedCornerTopLeft), cornerIconSize, DGUI.Properties.SingleLineHeight, cornerColorName);
                GUILayout.Space(DGUI.Properties.Space(4));
                DGUI.Icon.Draw(Styles.GetStyle(Styles.StyleName.IconRoundedCornerTopRight), cornerIconSize, DGUI.Properties.SingleLineHeight, cornerColorName);
                GUILayout.Space(DGUI.Properties.Space(4));
                DrawCorner(m_topRightRadius, maxCornerValue);
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(DGUI.Properties.Space());
            EditorGUILayout.BeginHorizontal();
            {
                DrawCorner(m_bottomLeftRadius, maxCornerValue);
                GUILayout.Space(DGUI.Properties.Space(4));
                DGUI.Icon.Draw(Styles.GetStyle(Styles.StyleName.IconRoundedCornerBottomLeft), cornerIconSize, DGUI.Properties.SingleLineHeight, cornerColorName);
                GUILayout.Space(DGUI.Properties.Space(4));
                DGUI.Icon.Draw(Styles.GetStyle(Styles.StyleName.IconRoundedCornerBottomRight), cornerIconSize, DGUI.Properties.SingleLineHeight, cornerColorName);
                GUILayout.Space(DGUI.Properties.Space(4));
                DrawCorner(m_bottomRightRadius, maxCornerValue);
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(-DGUI.Properties.SingleLineHeight * 2f + DGUI.Properties.Space());

            float iconSize = DGUI.Properties.SingleLineHeight * 1.4f;

            EditorGUILayout.BeginHorizontal(GUILayout.Height(DGUI.Properties.SingleLineHeight));
            {
                GUILayout.FlexibleSpace();
                if (m_lockCorners)
                {
                    if (DGUI.Button.IconButton.Draw(Styles.GetStyle(Styles.StyleName.IconButtonLink), ComponentColorName, DGUI.Properties.SingleLineHeight, iconSize))
                        m_lockCorners = false;
                }
                else
                {
                    if (DGUI.Button.IconButton.Draw(Styles.GetStyle(Styles.StyleName.IconButtonUnlink), DGUI.Colors.DisabledIconColorName, DGUI.Properties.SingleLineHeight, iconSize))
                        m_lockCorners = true;
                }

                GUILayout.FlexibleSpace();
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(DGUI.Properties.SingleLineHeight / 2 + DGUI.Properties.Space() / 2);
        }

        private void DrawCorner(SerializedProperty radiusProperty, float maxCornerValue)
        {
            DGUI.Line.Draw(false, ComponentColorName, true,
                           () =>
                           {
                               GUILayout.Space(DGUI.Properties.Space(2));
                               GUI.color = DGUI.Colors.PropertyColor(ComponentColorName);
                               EditorGUILayout.BeginVertical();
                               GUILayout.Space(0);
                               float value = radiusProperty.floatValue;
                               EditorGUI.BeginChangeCheck();
                               value = EditorGUILayout.Slider(value, 0, maxCornerValue);
                               if (EditorGUI.EndChangeCheck())
                               {
                                   if (m_lockCorners)
                                   {
                                       UpdateAllCornersBy(value - radiusProperty.floatValue);
                                   }
                                   else
                                   {
                                       radiusProperty.floatValue = value;
                                   }
                               }

                               EditorGUILayout.EndVertical();
                               GUI.color = InitialGUIColor;
                           });
        }

        private void UpdateAllCornersBy(float by)
        {
            m_topLeftRadius.floatValue = Mathf.Max(0, m_topLeftRadius.floatValue + by);
            m_topRightRadius.floatValue = Mathf.Max(0, m_topRightRadius.floatValue + by);
            m_bottomLeftRadius.floatValue = Mathf.Max(0, m_bottomLeftRadius.floatValue + by);
            m_bottomRightRadius.floatValue = Mathf.Max(0, m_bottomRightRadius.floatValue + by);
        }

        private void UpdateFeatherRange()
        {
            if (m_feather.floatValue <= GetMaxFeatherRange(Range.Small))
            {
                SetFeatherRange(Range.Small);
                return;
            }

            if (m_feather.floatValue <= GetMaxFeatherRange(Range.Medium))
            {
                SetFeatherRange(Range.Medium);
                return;
            }

            if (m_feather.floatValue <= GetMaxFeatherRange(Range.Large))
            {
                SetFeatherRange(Range.Large);
                return;
            }

            SetFeatherRange(Range.Custom);
        }

        private float GetMaxFeatherRange(Range featherRange)
        {
            switch (featherRange)
            {
                case Range.Small:  return 1;
                case Range.Medium: return 5;
                case Range.Large:
                    Rect rect = Target.RectTransform.rect;
                    return Mathf.Max(rect.width, rect.height);
                case Range.Custom: return 100000;
                default:           throw new ArgumentOutOfRangeException("featherRange", featherRange, null);
            }
        }

        private void SetFeatherRange(Range range)
        {
            FeatherRange = range;
            m_maxFeatherValue = GetMaxFeatherRange(range);
            if (m_feather.floatValue > GetMaxFeatherRange(range))
                m_feather.floatValue = GetMaxFeatherRange(range);
        }

        private void UpdateCornersRadius()
        {
            float maxCornerRadius = MaxCornerValue;
            m_topLeftRadius.floatValue = Mathf.Max(0, maxCornerRadius);
            m_topRightRadius.floatValue = Mathf.Max(0, maxCornerRadius);
            m_bottomLeftRadius.floatValue = Mathf.Max(0, maxCornerRadius);
            m_bottomRightRadius.floatValue = Mathf.Max(0, maxCornerRadius);
        }
    }
}