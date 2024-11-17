using System.Diagnostics;
using BOOSE;

namespace ASE_Project_Ekauf
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            Debug.WriteLine(AboutBOOSE.about());
            AppCanvas BooseCanvas = new AppCanvas();
            pictureBox1.Image = BooseCanvas;
            //pictureBox1.SizeMode = SizeMode.StretchImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string uInput = textBox1.Text;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}