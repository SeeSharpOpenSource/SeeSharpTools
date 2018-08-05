using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SeeSharpTools.JY.GUI
{
    class TickMajor
    {
        private int m_Length;

        private int m_Thickness;

        private Color m_Color;

        private double m_LocationValue;

        private Size m_TextSize;

        public int Length
        {
            get
            {
                return m_Length;
            }

            set
            {
                m_Length = value;
            }
        }

        public int Thickness
        {
            get
            {
                return m_Thickness;
            }

            set
            {
                m_Thickness = value;
            }
        }

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

        public double LocationValue
        {
            get
            {
                return m_LocationValue;
            }

            set
            {
                m_LocationValue = value;
            }
        }

        public Size TextSize
        {
            get
            {
                return m_TextSize;
            }

            set
            {
                m_TextSize = value;
            }
        }
    }
}
