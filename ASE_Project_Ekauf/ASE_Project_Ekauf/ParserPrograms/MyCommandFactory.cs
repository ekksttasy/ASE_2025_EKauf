using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOOSE;

namespace ASE_Project_Ekauf.ParserPrograms
{
    class MyCommandFactory : CommandFactory
    {
        public override ICommand MakeCommand(string commandType)
        {
            commandType = commandType.ToLower().Trim();
            if (commandType.Equals("moveto"))
            {
                return new Variables.MoveTo();
            }

            if (commandType.Equals("drawto"))
            {
                return new DrawTo();
            }

            if (commandType.Equals("circle"))
            {
                return new BOOSE.Circle();
            }

            if (commandType.Equals("rect"))
            {
                return new BOOSE.Rect();
            }

            if (commandType.Equals("pen"))
            {
                return new PenColour();
            }

            if (commandType.Equals("eval"))
            {
                return new Evaluation();
            }

            if (commandType.Equals("if"))
            {
                return new If();
            }

            if (commandType.Equals("end"))
            {
                return new End();
            }

            if (commandType.Equals("else"))
            {
                return new Else();
            }

            if (commandType.Equals("while"))
            {
                return new While();
            }

            if (commandType.Equals("for"))
            {
                return new For();
            }

            if (commandType.Equals("int"))
            {
                return new Int();
            }

            if (commandType.Equals("real"))
            {
                return new Real();
            }

            if (commandType.Equals("write"))
            {
                return new Write();
            }

            if (commandType.Equals("array"))
            {
                return new BOOSE.Array();
            }

            if (commandType.Equals("poke"))
            {
                return new Poke();
            }

            if (commandType.Equals("peek"))
            {
                return new Peek();
            }

            if (commandType.Equals("cast"))
            {
                return new Cast();
            }

            if (commandType.Equals("method"))
            {
                return new Method();
            }

            if (commandType.Equals("call"))
            {
                return new Call();
            }

            throw new FactoryException("No such command '" + commandType + "'");

        }
    }
}
