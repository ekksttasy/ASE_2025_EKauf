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
        public StoredProgram(ICanvas canvas) : base(canvas)
        {
        }

        private object NextCommand()
        {
            
            return this[PC++];
        }
        public override void Run()
        {
            PC = 0;
            while (Commandsleft())
            {
                ICommand command = (ICommand)NextCommand();
                try
                {
                    PC++;
                    command.Execute();
                }
                catch (BOOSEException ex)
                {
                    SyntaxOk = false;
                    throw new StoredProgramException(ex.Message + " at line " + PC);
                }
            }
        }
    }
}
