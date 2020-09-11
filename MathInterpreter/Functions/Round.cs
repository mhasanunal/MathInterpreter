using MathInterpreter;
using System;

namespace MathInterpreter.Functions
{
    public class Round : MathMetaBase, IMathFunction
    {
        public Round()
        {
            this.Keyword = "round";
            this.NumberOfArguments = 1;
            this.Priority = OrderOfOperators.LEVEL_5;

        }
        public override void Operate(ref double result, params double[] args)
        {
            result = Math.Round(args[0]);
        }
    }
}
