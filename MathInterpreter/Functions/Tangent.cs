using System;

namespace MathInterpreter.Functions
{
    public sealed class TangentWithRadiant : MathMetaBase, IMathFunction
    {
        public TangentWithRadiant()
        {
            this.Keyword = "tanr";
            this.Priority = OrderOfOperators.LEVEL_5;
            this.RightAssociated = false;
            this.NumberOfArguments = 1;
        }
        public override void Operate(ref double result, params double[] args)
        {
            result = Math.Tan(args[0]);
        }
    }
    public sealed class TangentWithDegrees : MathMetaBase, IMathFunction
    {
        public TangentWithDegrees()
        {
            this.Keyword = "tand";
            this.Priority = OrderOfOperators.LEVEL_5;
            this.RightAssociated = false;
            this.NumberOfArguments = 1;
        }
        public override void Operate(ref double result, params double[] args)
        {
            result = Math.Tan(args[0].ApplyTrigonometricFunction());
        }
    }
}
