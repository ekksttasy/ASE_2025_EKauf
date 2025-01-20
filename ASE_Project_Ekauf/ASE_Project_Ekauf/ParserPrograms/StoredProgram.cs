using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOOSE;

namespace ASE_Project_Ekauf.ParserPrograms
{
    public class StoredProgram : BOOSE.StoredProgram
    {
        private int x = base.pc;
        public StoredProgram(ICanvas canvas) : base(canvas)
        {
        }

        private object NextCommand()
        {
            return this[x++];
        }
        public override void Run()
        {
            int x = 0;
            while (Commandsleft())
            {
                ICommand command = (ICommand)NextCommand();
                try
                {
                    x++;
                    command.Execute();
                }
                catch (BOOSEException ex)
                {
                    SyntaxOk = false;
                    throw new StoredProgramException(ex.Message + " at line " + x);
                }
            }
        }
    }
}
