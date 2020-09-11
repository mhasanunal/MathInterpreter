using System;

namespace MathInterpreter.Functions
{
    public sealed class Log : MathMetaBase, IMathFunction
    {
        public Log()
        {
            this.Keyword = "log";
            this.Priority = OrderOfOperators.LEVEL_5;
            this.RightAssociated = false;
            this.NumberOfArguments = 2;

        }
        public override void Operate(ref double result, params double[] args)
        {
            result = Math.Log(args[1], args[0]);
        }
    }
}
