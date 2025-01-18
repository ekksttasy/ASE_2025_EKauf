using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BOOSE;

namespace ASE_Project_Ekauf.Commands
{
    /// <summary>
    /// Array class extends BOOSE Evaluation class to construct and analyze arrays.
    /// </summary>
    class Array : BOOSE.Evaluation
    {
        protected int varCount;
        protected int myRows;
        protected int myCols;
        protected string ppRows = "0";
        protected string ppCols = "0";
        protected string ppVal;
        protected string type = "";

        protected int varInt;
        protected int[,] intArray;
        protected double varReal;
        protected double[,] realArray;
        private bool passed = true;

        public Array()
        {
            
        }

        /// <summary>
        /// Compiles array and checks for valid array type declaration and parameters.
        /// </summary>
        /// <exception cref="CommandException"></exception>
        public override void Compile()
        {
            base.Compile();
            int.TryParse(parameters[2], out myRows);
            int.TryParse(parameters[3], out myCols);
            varName = parameters[1];

            if (!parameters[0].Equals("int") && !parameters[0].Equals("real"))
            {
                passed = false;
                throw new CommandException("Undefined array type");
            }

        }

        public override void CheckParameters(string[] parameterList)
        {
            base.Parameters = base.ParameterList.Trim().Split(" ");
            if (base.Parameters.Length != 3 && base.Parameters.Length != 4)
            {
                throw new CommandException("Invalid array declaration");
            }
        }

        public override void Execute()
        {
            base.Execute();
            if (type.Equals("int"))
            {
                intArray = new int[myRows, myCols];
                return;
            }

            if (type.Equals("real"))
            {
                realArray = new double[myRows, myCols];
                return;
            }
        }

        protected void ArrayProcessingCompilation(bool pp)
        {
            int poker;
            int peeker;
            if (pp)
            {
                poker = 1;
                peeker = 0;
            }
            else
            {
                poker = 0;
                peeker = 1;
            }

            string[] litList = parameterList.Split('=');
            if (litList.Length >= 2)
            {
                ppVal = litList[poker].Trim();
            }
            else
            {
                throw new CommandException("Array parameters unacceptable: missing values for command");
            }
            string[] moreLitList = litList[peeker].Trim().Split(" ");
            if (moreLitList.Length < 1)
            {
                throw new CommandException("Array parameters unacceptable: missing values for command");
            }

            varName = moreLitList[0].Trim();
            if (!program.VariableExists(varName))
            {
                throw new CommandException("No such array");
            }

            ppRows = moreLitList[1];
            if (moreLitList.Length > 2)
            {
                ppCols = moreLitList[2];
            }
        }

        protected void ArrayProcessingExecution(bool pp)
        {
            string checkRow = CheckPPString(ppRows);
            string checkCol = CheckPPString(ppCols);
            string checkVal = CheckPPString(ppVal);

            passed = int.TryParse(checkRow, out myRows);
            if (!passed)
            {
                throw new CommandException("Invalid row in array");
            }

            passed = int.TryParse(checkCol, out myCols);
            if (!passed)
            {
                throw new CommandException("Invalid column in array");
            }

            Array checkVar = (Array)program.GetVariable(varName);
            if (checkVar.type.Equals("int") ||  checkVar.type.Equals("real"))
            {
                if (checkVar.type.Equals("int"))
                {
                    type = "int";
                    if (int.TryParse(checkVal, out varInt))
                    {
                        if(pp)
                        {
                            checkVar.SetIntArray(varInt, myRows, myCols);
                        }
                        else
                        {
                            varInt = checkVar.GetIntArray(myRows, myCols);
                        }
                    }
                    else
                    {
                        throw new CommandException("Invalid poke value");
                    }
                }
                else
                {
                    type = "real";
                    if (double.TryParse(checkVal, out varReal))
                    {
                        if (pp)
                        {
                            checkVar.SetRealArray(varReal, myRows, myCols);
                        }
                        else
                        {
                            varReal = checkVar.GetRealArray(myRows, myCols);
                        }
                    }
                    else
                    {
                        throw new CommandException("Invalid poke value");
                    }
                }
            }
            else
            {
                throw new CommandException("Undefined array type: must be 'real' or 'int'");
            }
        }

        private string CheckPPString(string pp)
        {
            if (program.IsExpression(pp))
            {
                string newPP = base.Program.EvaluateExpression(pp).Trim().ToLower();
                return newPP;
            }
            return pp;
        }

        private void CheckIndex(int row, int col)
        {
            if (row >= myRows || col >= myCols)
            {
                throw new CommandException("Array index out of bounds");
            }
        }

        public void SetIntArray(int val, int row, int col)
        {
            CheckIndex(row, col);
            intArray[row, col] = val;
        }

        public int GetIntArray(int row, int col)
        {
            CheckIndex(row, col);
            return intArray[row, col];
        }

        public void SetRealArray(double val, int row, int col)
        {
            CheckIndex(row, col);
            realArray[row, col] = val;
        }

        public double GetRealArray(int row, int col)
        {
            CheckIndex(row, col);
            return realArray[row, col];
        }

    }
}
