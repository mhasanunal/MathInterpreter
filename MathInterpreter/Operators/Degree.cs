using System;

namespace MathInterpreter.Operators
{
    public sealed class Degree : MathMetaBase, IMathOperator
    {
        public Degree()
        {
            this.Keyword = "^";
            this.Priority = OrderOfOperators.LEVEL_4;
            this.RightAssociated = true;
            this.NumberOfArguments = 2;

        }
        public override void Operate(ref double result, params double[] args)
        {
            var number = args[0];
            var power = args[1];
            result = Math.Pow(number,power);
        }
    }
}
