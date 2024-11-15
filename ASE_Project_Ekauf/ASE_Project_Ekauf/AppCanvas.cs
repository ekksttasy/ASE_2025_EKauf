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
        private object pColor;
        private Pen myPen;
        private int x_Pos, y_Pos;
        //Graphics g; 

        static int mapX = 256;
        static int mapY = 256;

        Graphics g = Graphics.FromImage(new Bitmap(mapX, mapY));
        object booseMap = new object(); //(mapX, mapY);

        public object PenColour
        {
            //retrieve/change pen color
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
            //pen X position
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
            // pen Y position
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
            //clear bitmap to default color
        }

        public void Reset()
        {
            //return pen to (0,0)
        }

        public void Set(int width, int height)
        {
            int mapX = width;
            int mapY = height;
        }

        public void DrawTo(int x, int y)
        {
            //draw a line from previous pen position to specified pen position
            this.x_Pos = x;
            this.y_Pos = y;
        }

        public void MoveTo(int x, int y)
        {
            //move pen to specified position
            this.x_Pos = x;
            this.y_Pos = y;
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
            //this.
        }

        public object getBitmap()
        {
            return booseMap;
        }

        public static implicit operator Image(AppCanvas v)
        {
            throw new NotImplementedException();
        }
    }
}
