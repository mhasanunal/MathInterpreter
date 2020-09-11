namespace MathInterpreter.Operators
{
    public sealed class LeftParenthesis : MathMetaBase, IMathOperator
    {
        public LeftParenthesis()
        {
            this.Keyword = "(";
            this.Priority = OrderOfOperators.LEVEL_0;
            this.RightAssociated = false;
            this.NumberOfArguments = 0;
        }
        public override void Operate(ref double result, params double[] args)
        {

        }
    }
}
