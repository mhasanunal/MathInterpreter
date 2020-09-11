using System;

namespace MathInterpreter.Functions
{
    public sealed class Ln : MathMetaBase, IMathFunction
    {
        public Ln()
        {
            this.Keyword = "ln";
            this.Priority = OrderOfOperators.LEVEL_5;
            this.RightAssociated = false;
            this.NumberOfArguments = 1;
        }
        public override void Operate(ref double result, params double[] args)
        {
            result = Math.Log(args[0]);
        }
    }
}
