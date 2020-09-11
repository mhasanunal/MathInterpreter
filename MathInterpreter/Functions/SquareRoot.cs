using System;

namespace MathInterpreter.Functions
{
    public sealed class SquareRoot : MathMetaBase, IMathFunction
    {
        public SquareRoot()
        {
            this.Keyword = "sqrt";
            this.Priority = OrderOfOperators.LEVEL_4;
            this.RightAssociated = false;
            this.NumberOfArguments = 1;
        }
        public override void Operate(ref double result, params double[] args)
        {
            result = Math.Sqrt(args[0]);
        }
    }
}
