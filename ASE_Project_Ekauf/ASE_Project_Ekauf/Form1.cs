using System.Diagnostics;
using BOOSE;
using System.Drawing;


namespace ASE_Project_Ekauf
{
    public partial class Form1 : Form
    {
        public static class AboutBOOSE : Object
        {
            public const int ARRAYLIMIT = 2;
            public const int BODYLIMIT = 2;
            public const int COMPOUNCOMMANDLIMIT = 2;
            public const int MAXPROGRAMSIZE = 10;
            public const bool RESTRICTIONS = false;
            public const int SIZELIMIT = 100;
            public const int VARIABLELIMIT = 5;

            public static float Version
            {
                get
                {
                    return float value; //HELP LMAO
                }
            }
            public static string about();
        }
        
        public TextBox BooseInput
        {
            get;
            private set;
        }
        public Form1()
        {
            InitializeComponent();
            Debug.WriteLine(AboutBOOSE.about());
            BooseCanvas = new AppCanvas();
            BooseInput = new TextBox();

        }
    }
}