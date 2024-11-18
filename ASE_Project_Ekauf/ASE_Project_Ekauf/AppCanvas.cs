using BOOSE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASE_Project_Ekauf
{
    /// <summary>
    /// Class implements ICanvas from Boose to render Boose code on form.
    /// </summary>
    public class AppCanvas : Canvas, ICanvas
    {
        private object pColor;
        private Pen myPen;
        private int x_Pos = 0;
        private int y_Pos = 0;
        private Color background_color = Color.Gray;

        static int mapX = 256;
        static int mapY = 256;

        Font defaultFont = new Font("Arial", 16, FontStyle.Bold);

        Bitmap bitmap;
        

        public AppCanvas(Color background_color)
        {
            this.background_colour = background_color;  
        }

        public override object PenColour
        {
            //retrieve/change pen color
            get;
            set;
        }

        public override void SetColour(int red, int green, int blue)
        {
            pColor = Color.FromArgb(red, green, blue);
            Debug.WriteLine("Color is set to RGB {pcolor}");
        }
        public override int Xpos
        {
            //pen X position
            get;
            set;
        }

        public override int Ypos
        {
            // pen Y position
            get;
            set;
        }

        public override void Clear()
        {
            //clear bitmap to default color
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(background_color); 
        }

        public override void Reset()
        {
            //return pen to (0,0)
            x_Pos = 0;
            y_Pos = 0;
        }

        public override void Set(int width, int height)
        {
            //set display size of output window
            int mapX = width;
            int mapY = height;
        }

        public override void DrawTo(int x, int y)
        {
            //draw a line from previous pen position to specified pen position
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawLine(myPen, x_Pos, y_Pos, x, y);

            x_Pos = x;
            y_Pos = y;
        }

        public override void MoveTo(int x, int y)
        {
            //move pen to specified position
            x_Pos = x;
            y_Pos = y;
        }

        public void WriteText(string text)
        {
            Brush brush = new SolidBrush((Color)pColor);
            Graphics g = Graphics.FromImage(bitmap);
            g.DrawString(text, defaultFont, brush, x_Pos, y_Pos);
        }

        public override void Circle(int radius, bool filled)
        {
            //draws a circle from pen position
            Circle myCircle = new Circle((Color)pColor, x_Pos, y_Pos, radius);
            Graphics g = Graphics.FromImage(bitmap);
            myCircle.Draw(g, myPen, filled);
        }

        public override void Rect(int width, int height, bool filled)
        {
            //draws a rectangle from pen position
            Rect myRect = new Rect((Color)pColor, x_Pos, y_Pos, width, height);
            Graphics g = Graphics.FromImage(bitmap);
            myRect.Draw(g, myPen, filled);
        }

        public override void Tri(int width, int height)
        {
            //draws a triangle from pen position
            Debug.WriteLine("Triangle method not yet implemented");
        }

        public override object getBitmap()
        {
            bitmap = new Bitmap(mapX, mapY);


            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Gray);
            }
            return bitmap;
        }

        public static implicit operator Image(AppCanvas v)
        {
            throw new NotImplementedException();
        }
    }
}
