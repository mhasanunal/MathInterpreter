using System;

namespace MathInterpreter.Functions
{
    public sealed class Exp : MathMetaBase, IMathFunction
    {
        public Exp()
        {
            this.Keyword = "exp";
            this.Priority = OrderOfOperators.LEVEL_5;
            this.RightAssociated = false;
            this.NumberOfArguments = 1;
        }
        public override void Operate(ref double result, params double[] args)
        {
            result = Math.Exp(args[0]);
        }
    }
}
