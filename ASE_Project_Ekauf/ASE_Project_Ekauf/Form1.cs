using System.Diagnostics;
using System.Drawing.Text;
using System.Text.RegularExpressions;
using BOOSE;
using static System.Windows.Forms.LinkLabel;

namespace ASE_Project_Ekauf
{
    public partial class Form1 : Form
    {
        private AppCanvas BooseCanvas;
        private CommandFactory CmdFactory;
        private ParserPrograms.StoredProgram Program;
        private ParserPrograms.Parser ParserF;

        public Form1()
        {
            InitializeComponent();
            
            Debug.WriteLine(BOOSE.AboutBOOSE.about());
            BooseCanvas = new AppCanvas(Color.Gray);

            Bitmap myBooseMap= (Bitmap)BooseCanvas.getBitmap();   
            pictureBox1.Image = myBooseMap;
            CmdFactory = new ParserPrograms.MyCommandFactory();
            Program = new ParserPrograms.StoredProgram(BooseCanvas);
            ParserF = new ParserPrograms.Parser(CmdFactory, Program);  
        }


        private bool buttonClicked = false; //flag to mark if textbox has been run

        private void button1_Click(object sender, EventArgs e)
        {
            buttonClicked = true;
            string uInput = textBox1.Text;
            textBox1.Enabled = false; //stop text being inputted while boose code is running

            ParserF.ParseProgram(uInput);
            Program.Run();
            //searchUInput(uInput, BooseCanvas); //run boose code
            pictureBox1.Image = (Bitmap)BooseCanvas.getBitmap();
            pictureBox1.Refresh(); //update picturbox with bitmap changes

            textBox1.Enabled = true; //reenable textbox
            buttonClicked = false; //return flag to original state
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!buttonClicked)
            {
                return;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException("Picturebox interact handler not implemented.");
        }





        /// <summary>
        /// Reads through the user input from the textbox, separates commands into an array and calls each respective command.
        /// </summary>
        /// <param name="uInput">The user input from the textbox, in string form. </param>
        /// <param name="BooseCanvas">The AppCanvas instance being used for bitmap operations. </param>
        private static void searchUInput(string uInput, AppCanvas BooseCanvas)
        {
            string[] inputLines = uInput.Split(new[] { "\r\n", "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < inputLines.Length; i++)
            {
                var command = Regex.Matches(inputLines[i], @"\w+").Cast<Match>().Select(m => m.Value).ToArray(); //separate words in line to a command array
                var digitsString = Regex.Matches(inputLines[i], @"\d+").Cast<Match>().Select(m => m.Value).ToArray(); //separate numerical identifiers to an array
                int[] digits = digitsString.Select(s => int.Parse(s)).ToArray();

                for (int x = 0; x < command.Length; x++)
                {
                    string meow = command[x].ToString();
                    Debug.WriteLine(meow);
                }

                for (int j = 0; j < command.Length; j++)
                {
                    switch (command[j]) //switch to check what command has been called.
                    {
                        case "moveto":
                            BooseCanvas.MoveTo(digits[0], digits[1]);
                            break;
                        case "pen":
                            BooseCanvas.SetColour(digits[0], digits[1], digits[2]);
                            break;
                        case "circle":
                            BooseCanvas.Circle(digits[0], true);
                            break;
                        case "rect":
                            BooseCanvas.Rect(digits[0], digits[1], true);
                            break;
                        case "drawto":
                            BooseCanvas.DrawTo(digits[0], digits[1]);
                            break;
                        default:
                            Console.WriteLine($"{inputLines[i]} is not a valid command.");
                            break;
                    }
                    
                }

            }

            Console.WriteLine($"{inputLines}");
        }
    }
}