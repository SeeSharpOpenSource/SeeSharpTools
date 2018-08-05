using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System;
using System.Collections.Generic;

namespace SeeSharpTools.JY.GUI
{
    public class ColorSection
    {

        private double m_Start;

        private double m_Stop;

        private Color m_Color;

    
        public double Start
        {
            get
            {
                return this.m_Start;
            }
            set
            {
                if (this.Start != value)
                {
                    this.m_Start = value;
                }
            }
        }

        public double Stop
        {
            get
            {
                return this.m_Stop;
            }
            set
            {
                if (this.Stop != value)
                {
                    this.m_Stop = value;
                }
            }
        }

        [Description(""), RefreshProperties(RefreshProperties.All)]
        public Color Color
        {
            get
            {
                return m_Color;
            }
            set
            {
                m_Color = value;
            }
        }

    }
}
