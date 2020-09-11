using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathInterpreter.Operators
{
    public sealed class RightParenthesis : MathMetaBase, IMathOperator
    {
        public RightParenthesis()
        {
            this.Keyword = ")";
            this.Priority = OrderOfOperators.LEVEL_0;
            this.RightAssociated = false;
            this.NumberOfArguments = 0;
        }
        public override void Operate(ref double result, params double[] args)
        {

        }
    }
}
