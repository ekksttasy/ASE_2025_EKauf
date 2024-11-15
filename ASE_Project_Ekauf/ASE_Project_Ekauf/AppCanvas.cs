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
        
        public Color PenColour
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

    public void SetColour(int red, int green, int blue)
        {

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

        public void Clear()
        {

        }

        public void Reset()
        {

        }

        public void Set(int width, int height)
        {

        }

        public void DrawTo(int x, int y)
        {

        }

        public void MoveTo(int x, int y)
        {

        }

        public void WriteText(string text)
        {

        }

        public void Circle(int radius, bool filled)
        {

        }

        public void Rect(int width, int height, bool filled)
        {

        }

        public void Tri(int width, int height)
        {

        }

        public void getBitmap()
        {
            return object; //help
        }
    }
}
