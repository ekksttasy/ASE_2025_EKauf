using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOOSE;

namespace ASE_Project_Ekauf.Commands
{
    class MoveTo : CommandTwoParameters
    {
        public override void Execute()
        {
            base.Execute();
            base.Canvas.MoveTo(base.Paramsint[0], base.Paramsint[1]);
        }
    }
}
