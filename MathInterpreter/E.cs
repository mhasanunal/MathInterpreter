using System;

namespace MathInterpreter
{
    public class E : PredefinedConstant
    {
        public E()
        {
            this.Value = Math.E;
            this.SetKeyword("e");
        }
    }
}
