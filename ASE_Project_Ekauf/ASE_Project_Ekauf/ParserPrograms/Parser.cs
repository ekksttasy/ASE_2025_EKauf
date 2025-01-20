using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOOSE;
using static System.Net.Mime.MediaTypeNames;

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

            if (CheckCustomVar(array, command))
            {

                Evaluation variable = Program.GetVariable(command);
                parameters = command + " " + parameters.Trim();

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

                    command = "boolean";
                }
            }

            ICommand cmdParsed = MyFactory.MakeCommand(command);
            cmdParsed.Set(Program, parameters);
            cmdParsed.Compile();
            return cmdParsed;
        }

        private bool CheckCustomVar(string[] array, string variable)
        {
            if (array.Length > 1 && array[1].Trim().Equals("=") && !variable.Equals("int") && !variable.Equals("real") && !variable.Equals("boolean"))
            {
                if(!Program.VariableExists(variable))
                {
                    throw new ParserException("Variable" + variable + "is undefined");
                }
                return true;
            }
            return false;
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
