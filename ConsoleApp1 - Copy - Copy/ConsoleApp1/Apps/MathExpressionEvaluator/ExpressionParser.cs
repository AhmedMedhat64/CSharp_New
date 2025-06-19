using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CSharpFundamentals.Apps.MathExpressionEvaluator
{
    public class ExpressionParser
    {
        private const string MathSymbols = "+*/%^";
        public static MathExpression Parse(string input)
        {
            input = input.Trim();
            var expr = new MathExpression();
            string token = "";
            bool IsLeftIntialized = false;
            for (int i = 0; i < input.Length; i++)
            {
                var currentChar = input[i];
                if (char.IsDigit(currentChar))
                {
                    token += currentChar;
                    if (i == input.Length - 1 && IsLeftIntialized)
                    {
                        expr.RigthSideOperand = double.Parse(token);
                        break;
                    }
                }
                else if (MathSymbols.Contains(currentChar))
                {
                    if (!IsLeftIntialized)
                    {
                    expr.LeftSideOperand = double.Parse(token);
                    IsLeftIntialized = true;
                    }
                    expr.Operation = ParseMathOperation(currentChar.ToString());
                    token = "";
                }
                else if (currentChar == '-' && i > 0)
                {
                    if (expr.Operation == MathOperation.None)
                    {
                        expr.Operation = MathOperation.Subtraction;
                        if (!IsLeftIntialized)
                        {
                            expr.LeftSideOperand = double.Parse(token);
                            IsLeftIntialized = true;
                        }
                        token = "";
                    }
                    else 
                        token += currentChar;
                }
                else if (char.IsLetter(currentChar))
                {
                    token += currentChar;
                    IsLeftIntialized = true;
                }
                else if (char.IsWhiteSpace(currentChar))
                {
                    if (!IsLeftIntialized)
                    {
                        expr.LeftSideOperand = double.Parse(token);
                        IsLeftIntialized = true;
                        token = "";
                    }
                    else if (expr.Operation == MathOperation.None)
                    {
                        expr.Operation = ParseMathOperation(token);
                        token = "";
                    }
                }
                else
                    token += currentChar;
            }
            return expr;
        }
        public static MathOperation ParseMathOperation(string token)
        {
            switch (token.ToLower())
            {
                case "+":
                    return MathOperation.Addition;
                case "*":
                    return MathOperation.MultiPlication;
                case "/":
                    return MathOperation.Division;
                case "%":
                case "mod":
                    return MathOperation.Modulus;
                case "^":
                case "pow":
                    return MathOperation.Power;
                case "sin":
                    return MathOperation.Sin;
                case "cos":
                    return MathOperation.Cos;
                case "tan":
                    return MathOperation.Tan;
                default:
                    return MathOperation.None;
            }
        }
    }
}
