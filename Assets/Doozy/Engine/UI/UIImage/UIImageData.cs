// Copyright (c) 2015 - 2019 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using System;
using UnityEngine;

namespace Doozy.Engine.UI
{
    /// <summary> Holds all the settings of an UIImage component </summary>
    [Serializable]
    public struct UIImageData
    {
        #region Public Variables

        /// <summary> Converts the image into a border with a set width if greater than 0 </summary>
        public float BorderWidth;

        /// <summary> Also known as 'soft edge' value. Used to adjust harsh edges for graphics </summary>
        public float Feather;
        
        /// <summary> Rect height </summary>
        public float Height;
        
        /// <summary> Actual size of a pixel after having been adjusted by the pixel perfect rect </summary>
        public float PixelSize;
        
        /// <summary> Radius values for each rect corner (starting upper-left, clockwise) </summary>
        public Vector4 Radius;

        /// <summary> Rect width </summary>
        public float Width;
        
        #endregion

        #region Constructors
        
        /// <summary> Initializes an UIImageData struct with the given settings </summary>
        /// <param name="width"> Rect width </param>
        /// <param name="height"> Rect height </param>
        /// <param name="radius"> Radius values for each rect corner (starting upper-left, clockwise) </param>
        /// <param name="borderWidth"> Converts the image into a border with a set width if greater than 0 </param>
        /// <param name="feather"> Also known as 'soft edge' value. Used to adjust harsh edges for graphics </param>
        /// <param name="pixelSize"> Actual size of a pixel after having been adjusted by the pixel perfect rect </param>
        public UIImageData(float width, float height, Vector4 radius, float borderWidth, float feather, float pixelSize)
        {
            Width = Mathf.Abs(width);
            Height = Mathf.Abs(height);
            Radius = radius;
            BorderWidth = Mathf.Max(borderWidth, 0);
            Feather = Mathf.Max(0, feather);
            PixelSize = Mathf.Max(0, pixelSize);
        }
        
        #endregion
    }
}