using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamentals.Apps.MathExpressionEvaluator
{
    public class MathExpression
    {
        public double LeftSideOperand { get; set; }
        public double RigthSideOperand { get; set; }
        public MathOperation Operation { get; set; }
    }
}
