using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOOSE;

namespace ASE_Project_Ekauf.ParserPrograms
{
    internal class Parser : IParser
    {
        protected ICommandFactory MyFactory;
        protected StoredProgram Program;
        public Parser(CommandFactory Factory, ParserPrograms.StoredProgram Program)
        {
            this.Program = Program;
            MyFactory = Factory;
        }

        public ICommand ParseCommand(string Line)
        {
            if (Line[0] == '*')
            {
                return null;
            }

            Line = TidyExpression(Line);
            string[] array = Line.Split(' ');
            _ = new string[10];
            string text = array[0];
            string text2 = "";
            for (int i = 1; i < array.Length; i++)
            {
                text2 = text2 + array[i] + " ";
            }

            if (array.Length > 1 && array[1].Trim().Equals("=") && !text.Equals("int") && !text.Equals("real") && !text.Equals("boolean"))
            {
                if (!Program.VariableExists(text))
                {
                    throw new ParserException("Use of undefined variable" + text);
                }

                text2 = text + " " + text2.Trim();
                Evaluation variable = Program.GetVariable(text);
                if (variable is Int)
                {
                    text = "int";
                }
                else if (variable is Real)
                {
                    text = "real";
                }
                else
                {
                    if (!(variable is BOOSE.Boolean))
                    {
                        throw new ParserException("unknown variable type");
                    }

                    text = "boolean";
                }
            }

            ICommand command = MyFactory.MakeCommand(text);
            command.Set(Program, text2);
            command.Compile();
            return command;
        }

        public void ParseProgram(string program)
        {
            program += "\nint endofprogram = 0";
            string text = "";
            string[] array = program.Split('\n');
            Program.SyntaxOk = false;
            for (int i = 0; i < array.Length; i++)
            {
                if (i > 10)
                {
                    throw new RestrictionException("Program exceeds restricted size");
                }

                array[i] = array[i].Trim();
                if (array[i].Equals(""))
                {
                    continue;
                }

                try
                {
                    ICommand command = ParseCommand(array[i]);
                    if (command is Method)
                    {
                        Method method = (Method)command;
                        _ = method.MethodName;
                        command = ParseCommand(method.Type + " " + method.MethodName);
                        Program.Remove(command);
                        for (int j = 0; j < method.LocalVariables.Length; j++)
                        {
                            command = ParseCommand(method.LocalVariables[j]);
                            ((Evaluation)command).Local = true;
                            Program.Remove(command);
                        }
                    }
                }
                catch (BOOSEException ex)
                {
                    if (ex.Message.Length != 0)
                    {
                        string text2 = ex.Message + " at line " + (i + 1) + "\n";
                        text += text2;
                        Program.SyntaxOk = false;
                    }
                }
            }

            text = text.Trim();
            if (text.Length != 0)
            {
                throw new ParserException(text);
            }
        }

        private string TidyExpression(string expression)
        {
            expression = expression.Trim();
            expression = expression.Replace("=", " = ");
            expression = expression.Replace("+", " + ");
            expression = expression.Replace("/", " / ");
            expression = expression.Replace("*", " * ");
            expression = expression.Replace("  ", " ");
            expression = expression.Replace("  ", " ");
            return expression;
        }
    }
}
