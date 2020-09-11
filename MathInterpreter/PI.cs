using System;

namespace MathInterpreter
{
    public class PI : PredefinedConstant
    {
        public PI()
        {
            this.Value = Math.PI;
            this.SetKeyword("pi");
        }
    }
}
