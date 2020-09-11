using System;

namespace MathInterpreter.Functions
{
    public sealed class Sinh : MathMetaBase, IMathFunction
    {
        public Sinh()
        {
            this.Keyword = "sinh";
            this.Priority = OrderOfOperators.LEVEL_5;
            this.RightAssociated = false;
            this.NumberOfArguments = 1;
        }
        public override void Operate(ref double result, params double[] args)
        {
            result = Math.Sinh(args[0].ApplyTrigonometricFunction());
        }
    }
}
