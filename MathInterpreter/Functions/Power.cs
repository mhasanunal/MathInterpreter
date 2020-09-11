using MathInterpreter;
using System;

namespace MathInterpreter.Functions
{
    public class Power : MathMetaBase, IMathFunction
    {
        public Power()
        {
            this.Keyword = "pow";
            this.Priority = OrderOfOperators.LEVEL_3;
            this.NumberOfArguments = 2;
        }
        public override void Operate(ref double result, params double[] args)
        {
            result = Math.Pow(args[1], args[0]);
        }
    }
}
