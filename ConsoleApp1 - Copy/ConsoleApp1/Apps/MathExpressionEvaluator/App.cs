using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentals.Apps.MathExpressionEvaluator
{
    public class App
    {
        public static void Run(string[] args)
        {
            while (true)
            {
                Console.Write("Enter a Math Expression: ");
                var input = Console.ReadLine();
                var expr = ExpressionParser.Parse(input);
                Console.WriteLine($"left Side = {expr.LeftSideOperand}, Operation Is {expr.Operation}, Right Side = {expr.RigthSideOperand}");
                Console.WriteLine($"{input} = {EvaluateExpression(expr)}");
            } 
        }
        public static object EvaluateExpression(MathExpression expr)
        {
            if (expr.Operation == MathOperation.Addition)
                return expr.LeftSideOperand + expr.RigthSideOperand;
            else if (expr.Operation == MathOperation.Subtraction)
                return expr.LeftSideOperand - expr.RigthSideOperand;
            else if (expr.Operation == MathOperation.MultiPlication)
                return expr.LeftSideOperand * expr.RigthSideOperand;
            else if (expr.Operation == MathOperation.Division)
                return expr.LeftSideOperand / expr.RigthSideOperand;
            else if (expr.Operation == MathOperation.Modulus)
                return expr.LeftSideOperand % expr.RigthSideOperand;
            else if (expr.Operation == MathOperation.Power)
                return Math.Pow(expr.LeftSideOperand, expr.RigthSideOperand);
            else if (expr.Operation == MathOperation.Sin)
                return Math.Sin(expr.RigthSideOperand);
            else if (expr.Operation == MathOperation.Cos)
                return Math.Cos(expr.RigthSideOperand);
            else if (expr.Operation == MathOperation.Tan)
                return Math.Tan(expr.RigthSideOperand);
            else
                return 0;
        }
    }
}
