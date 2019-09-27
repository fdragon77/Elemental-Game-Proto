// Copyright (c) 2015 - 2019 Doozy Entertainment / Marlink Trading SRL. All Rights Reserved.
// This code can only be used under the standard Unity Asset Store End User License Agreement
// A Copy of the EULA APPENDIX 1 is available at http://unity3d.com/company/legal/as_terms

using System;
using UnityEngine;

namespace Doozy.Engine.UI
{
    /// <summary> Holds all the info about an UIImage corner (rectangle corner) </summary>
    [Serializable]
    public struct UIImageCorner
    {
        #region Constants
        
        private const float DEFAULT_RADIUS = 4;
        private const bool DEFAULT_LOCKED_VALUE = false;

        #endregion
        
        #region Private Variables
        
        /// <summary> Internal variable for the corner radius </summary>
        [SerializeField]
        private float m_radius;
        
        /// <summary> Internal variable used to keep track if the radius is locked (cannot be changed) </summary>
        [SerializeField]
        private bool m_locked;

        #endregion

        #region Properties

        /// <summary> Corner radius </summary>
        public float Radius
        {
            get { return m_radius; }
            set
            {
                if(Locked) return;
                m_radius = value;
                ValidateRadius();
            }
        }

        /// <summary> If TRUE, the radius cannot be changed </summary>
        public bool Locked
        {
            get { return m_locked; }
            set { m_locked = value; }
        }

        #endregion

        #region Constructors
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="radius"> Initial Radius </param>
        public UIImageCorner(float radius) : this()
        {
            Reset();
            Radius = radius;
        }
        
        #endregion
        
        #region Public Methods
        
        /// <summary> Resets all the variables to their default values </summary>
        public void Reset()
        {
            m_radius = DEFAULT_RADIUS;
            m_locked = DEFAULT_LOCKED_VALUE;
        }

        /// <summary> Validates the radius value by making sure it does not go below 0 </summary>
        public void ValidateRadius() { m_radius = Mathf.Max(0, m_radius); }
        
        #endregion
    }
}