namespace MathInterpreter.Operators
{
    public sealed class Division : MathMetaBase, IMathOperator
    {
        public Division()
        {
            this.Keyword = "/";
            this.Priority = OrderOfOperators.LEVEL_3;
            this.RightAssociated = false;
            this.NumberOfArguments = 2;

        }
        public override void Operate(ref double result, params double[] args)
        {
            result = args[0] / args[1];
        }
    }
}
