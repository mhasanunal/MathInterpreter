using System;

namespace MathInterpreter.Functions
{
    public sealed class SinusWithRadiant : MathMetaBase, IMathFunction
    {
        public SinusWithRadiant()
        {
            this.Keyword = "sinr";
            this.Priority = OrderOfOperators.LEVEL_5;
            this.RightAssociated = false;
            this.NumberOfArguments = 1;
        }
        public override void Operate(ref double result, params double[] args)
        {
            //var degree = args[0] * Math.PI / 180;
            result = Math.Sin(args[0]);
        }
    }
    public sealed class SinusWithDegrees : MathMetaBase, IMathFunction
    {
        public SinusWithDegrees()
        {
            this.Keyword = "sind";
            this.Priority = OrderOfOperators.LEVEL_5;
            this.RightAssociated = false;
            this.NumberOfArguments = 1;
        }
        public override void Operate(ref double result, params double[] args)
        {
            //var degree = args[0] * Math.PI / 180;
            result = Math.Sin(args[0].ApplyTrigonometricFunction());
        }
    }
}
