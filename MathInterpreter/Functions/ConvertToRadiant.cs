using MathInterpreter;
using System;

namespace MathInterpreter.Functions
{
    public class ConvertRadiantToDegree : MathMetaBase, IMathFunction
    {
        public ConvertRadiantToDegree()
        {
            this.Keyword = "deg";
            this.NumberOfArguments = 1;
            this.Priority = OrderOfOperators.LEVEL_5;

        }
        public override void Operate(ref double result, params double[] args)
        {
            result = args[0] * 180 / Math.PI;
        }
    }
    public class ConvertToRadiant : MathMetaBase, IMathFunction

    {
        public ConvertToRadiant()
        {
            this.Keyword = "rad";
            this.NumberOfArguments = 1;
            this.Priority = OrderOfOperators.LEVEL_5;

        }
        public override void Operate(ref double result, params double[] args)
        {
            result = args[0] * Math.PI / 180;
        }
    }
}
