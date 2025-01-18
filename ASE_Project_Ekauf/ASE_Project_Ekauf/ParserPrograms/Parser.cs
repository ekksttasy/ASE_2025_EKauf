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

        public ICommand ParseCommand(string cmd)
        {
            if (cmd[0] == '*')
            {
                return null;
            }

            cmd = CleanHouse(cmd);
            string[] array = cmd.Split(' ');
            string command = array[0];
            string parameters = "";
            for (int i = 1; i < array.Length; i++)
            {
                parameters += array[i] + " ";
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
                    command = "int";
                }
                else if (variable is Real)
                {
                    command = "real";
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

            ICommand cmdParsed = MyFactory.MakeCommand(command);
            cmdParsed.Set(Program, parameters);
            cmdParsed.Compile();
            return cmdParsed;
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

        private string CleanHouse(string expression)
        {
            expression = expression.Trim()
                        .Replace("=", " = ")
                        .Replace("+", " + ")
                        .Replace("/", " / ")
                        .Replace("*", " * ")
                        .Replace("  ", " ")
                        .Replace("  ", " ");
            return expression;
        }
    }
}
