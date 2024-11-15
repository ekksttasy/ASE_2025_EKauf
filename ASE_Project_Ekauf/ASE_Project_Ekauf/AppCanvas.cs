using BOOSE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Project_Ekauf
{
    /// <summary>
    /// Class implements ICanvas from Boose to render Boose code on form.
    /// </summary>
    public class AppCanvas : ICanvas
    {
        private Color pColor;
        private Pen myPen;
        private int x_Pos, y_Pos;
        
        public Color PenColor
        {
            get
            {
                return pColor;
            }
            set
            {
                if (value != pColor)
                {
                    pColor = value;
                }
            }
        }
        public int Xpos
        {
            get
            {
                return x_Pos;
            }
            set
            {
                x_Pos = value;
            }
        }

        public int Ypos
        {
            get
            {
                return y_Pos;   
            }
            set
            {
                y_Pos = value;
            }
        }
    }
}
