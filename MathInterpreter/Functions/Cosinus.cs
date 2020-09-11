using MathInterpreter;
using System;

namespace MathInterpreter.Functions
{
    public class CosinusWithRadians : MathMetaBase, IMathFunction
    {
        public CosinusWithRadians()
        {
            this.Keyword = "cosr";
            this.Priority = OrderOfOperators.LEVEL_5;
            this.RightAssociated = false;
            this.NumberOfArguments = 1;
        }
        public override void Operate(ref double result, params double[] args)
        {
            //var degree = args[0] * Math.PI / 180;
            result = Math.Cos(args[0]);
        }
    }

    public class CosinusWithDegreees : MathMetaBase, IMathFunction
    {
        public CosinusWithDegreees()
        {
            this.Keyword = "cosd";
            this.Priority = OrderOfOperators.LEVEL_5;
            this.RightAssociated = false;
            this.NumberOfArguments = 1;
        }
        public override void Operate(ref double result, params double[] args)
        {
            //var degree = args[0] * Math.PI / 180;
            result = Math.Cos(args[0].ApplyTrigonometricFunction());
        }
    }
}
