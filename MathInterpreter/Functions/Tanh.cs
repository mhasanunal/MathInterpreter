using System;

namespace MathInterpreter.Functions
{
    public sealed class Tanh : MathMetaBase, IMathFunction
    {
        public Tanh()
        {
            this.Keyword = "tanh";
            this.Priority = OrderOfOperators.LEVEL_5;
            this.RightAssociated = false;
            this.NumberOfArguments = 1;
        }
        public override void Operate(ref double result, params double[] args)
        {
            result = Math.Tanh(args[0]);
        }
    }
}
