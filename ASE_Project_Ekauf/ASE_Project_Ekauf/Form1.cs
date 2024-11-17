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
            //AppCanvas BooseCanvas = new AppCanvas.booseMap();
            //pictureBox1.Set(BooseCanvas);
            //pictureBox1.SizeMode = SizeMode.StretchImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string uInput = textBox1.Text;
            textBox1.Enabled = false;
            // run uinput boose code
            textBox1.Enabled = true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException("Text changed handler not implemented.");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException("Picturebox interact handler not implemented.");
        }
    }
}