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
    public class StoredProgram : ArrayList, IStoredProgram
    {
        protected const int INFINITE_LOOP_THRESHOLD = 50000;

        protected const int TYPICAL_LOOP_SIZE = 20;

        protected ICanvas Canvas;

        protected int pc;

        public bool SyntaxOk;

        protected ArrayList Variables = new ArrayList();

        protected ArrayList Methods = new ArrayList();

        protected Stack ProgramStack = new Stack();

        public int PC
        {
            get
            {
                return pc;
            }
            set
            {
                if (value >= 0 && value < Count)
                {
                    pc = value;
                }
            }
        }

        public StoredProgram(ICanvas canvas)
        {
            Canvas = canvas;
            ResetProgram();
        }

        public void AddMethod(Method M)
        {
            Methods.Add(M);
        }

        public int FindMethod(Method M)
        {
            return Variables.IndexOf(M);
        }

        public Method GetMethod(string MethodName)
        {
            int num = FindMethod(MethodName);
            if (num == -1)
            {
                throw new StoredProgramException("No such varieble");
            }

            return (Method)Methods[num];
        }

        public int FindMethod(string methodName)
        {
            int result = -1;
            methodName = methodName.Trim();
            for (int i = 0; i < Variables.Count; i++)
            {
                if (((Method)Methods[i]).MethodName.Equals(methodName))
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        public virtual void AddVariable(Evaluation Variable)
        {
            if (!VariableExists(Variable.VarName))
            {
                Variables.Add(Variable);
            }
        }

        public Evaluation GetVariable(string VarName)
        {
            int num = FindVariable(VarName);
            if (num == -1)
            {
                throw new StoredProgramException("No such varieble");
            }

            return (Evaluation)Variables[num];
        }

        public Evaluation GetVariable(int index)
        {
            if (index >= Variables.Count)
            {
                throw new StoredProgramException("No such variable");
            }

            return (Evaluation)Variables[index];
        }

        public int FindVariable(Evaluation Variable)
        {
            return Variables.IndexOf(Variable);
        }

        public int FindVariable(string varName)
        {
            int result = -1;
            varName = varName.Trim();
            for (int i = 0; i < Variables.Count; i++)
            {
                if (((Evaluation)Variables[i]).VarName.Equals(varName))
                {
                    result = i;
                    break;
                }
            }

            return result;
        }

        public virtual bool VariableExists(string varName)
        {
            if (FindVariable(varName) == -1)
            {
                return false;
            }

            return true;
        }

        public virtual string GetVarValue(string varName)
        {
            int num = FindVariable(varName);
            if (num == -1)
            {
                throw new StoredProgramException("Attempt to retrieve non-existant variable.");
            }

            Evaluation evaluation = (Evaluation)Variables[num];
            if (evaluation is Real)
            {
                return ((Real)evaluation).Value.ToString();
            }

            return evaluation.Value.ToString();
        }

        public virtual void UpdateVariable(string varName, int value)
        {
            int index = FindVariable(varName);
            Evaluation evaluation = (Evaluation)Variables[index];
            if (evaluation is Int)
            {
                ((Int)evaluation).Value = value;
            }
            else
            {
                evaluation.Value = value;
            }
        }

        public virtual void UpdateVariable(string varName, double value)
        {
            if (((Evaluation)Variables[FindVariable(varName)]) is Real)
            {
                ((Real)Variables[FindVariable(varName)]).Value = value;
                return;
            }

            throw new CommandException("Type mismatch, expected a real value");
        }

        public virtual void UpdateVariable(string varName, bool value)
        {
            ((BOOSE.Boolean)Variables[FindVariable(varName)]).BoolValue = value;
        }

        public virtual void DeleteVariable(string varName)
        {
            int num = FindVariable(varName);
            if (num >= 0)
            {
                Variables.Remove(Variables[num]);
            }
        }

        public virtual bool IsExpression(string expression)
        {
            int result;
            bool flag = !int.TryParse(expression, out result);
            if (expression.Contains("\""))
            {
                return false;
            }

            if (flag || expression.Contains("=") || expression.Contains("+") || expression.Contains("-") || expression.Contains("*") || expression.Contains("/") || expression.Contains(">") || expression.Contains("<"))
            {
                return true;
            }

            return false;
        }

        public string EvaluateExpressionWithString(string expression)
        {
            string text = "";
            string[] array = expression.Split("+");
            int num = 0;
            while (num < array.Length)
            {
                if (IsExpression(array[num]))
                {
                    array[num] = EvaluateExpression(array[num]);
                }
                else
                {
                    array[num] = array[num].Trim().Trim('"');
                }

                text += array[num++];
            }

            return text;
        }

        public virtual string EvaluateExpression(string Exp)
        {
            string text = "";
            if (!IsExpression(Exp))
            {
                throw new StoredProgramException("Could not evaluate expression <" + Exp + ">");
            }

            string[] array = Exp.Split(" ");
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = array[i].Trim();
                if (VariableExists(array[i]))
                {
                    array[i] = GetVarValue(array[i]);
                }

                text = text + array[i] + " ";
            }

            DataTable dataTable = new DataTable();
            string text2 = "";
            try
            {
                return Convert.ToString(dataTable.Compute(text, ""));
            }
            catch (SyntaxErrorException)
            {
                throw new StoredProgramException("Invalid expression " + Exp);
            }
            catch (EvaluateException)
            {
                throw new StoredProgramException("Invalid expression " + Exp);
            }
        }

        public void Push(ConditionalCommand Com)
        {
            ProgramStack.Push(Com);
        }

        public ConditionalCommand Pop()
        {
            try
            {
                return (ConditionalCommand)ProgramStack.Pop();
            }
            catch (InvalidOperationException)
            {
                throw new StoredProgramException("Orphaned end-while/if/for/method");
            }
        }

        public virtual int Add(Command C)
        {
            if (C == null)
            {
                throw new StoredProgramException("Null command object passed");
            }

            if (C is CanvasCommand)
            {
                ((CanvasCommand)C).Canvas = Canvas;
            }

            return base.Add(C);
        }

        private object NextCommand()
        {
            return this[pc++];
        }

        public virtual void ResetProgram()
        {
            Canvas.Reset();
            Canvas.Clear();
            Clear();
            pc = 0;
            Variables.Clear();
        }

        public virtual bool Commandsleft()
        {
            if (pc < Count)
            {
                return true;
            }

            return false;
        }

        public virtual void Run()
        {
            int num = 0;
            while (Commandsleft())
            {
                ICommand command = (ICommand)NextCommand();
                try
                {
                    num++;
                    command.Execute();
                }
                catch (BOOSEException ex)
                {
                    SyntaxOk = false;
                    throw new StoredProgramException(ex.Message + " at line " + PC);
                }

                if (num > 50000 && PC < 20)
                {
                    throw new StoredProgramException("Probable infinite loop detected");
                }
            }

            if (ProgramStack.Count != 0)
            {
                Pop();
                SyntaxOk = false;
                throw new StoredProgramException("Missing end");
            }
        }

    }
}
