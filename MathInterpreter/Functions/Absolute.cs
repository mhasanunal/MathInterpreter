using System;

namespace MathInterpreter.Functions
{
    public sealed class Absolute : MathMetaBase, IMathFunction
    {
        public Absolute()
        {
            this.Keyword = "abs";
            this.Priority = OrderOfOperators.LEVEL_5;
            this.RightAssociated = false;
            this.NumberOfArguments = 1;
        }
        public override void Operate(ref double result, params double[] args)
        {
            result = Math.Abs(args[0]);
        }
    }
}
