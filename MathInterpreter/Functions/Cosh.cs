using System;

namespace MathInterpreter.Functions
{
    public sealed class Cosh : MathMetaBase,IMathFunction
    {
        public Cosh()
        {
            this.Keyword = "cosh";
            this.Priority = OrderOfOperators.LEVEL_5;
            this.RightAssociated = false;
            this.NumberOfArguments = 1;
        }
        public override void Operate(ref double result, params double[] args)
        {
            result = Math.Cosh(args[0]);
        }
    }
}
