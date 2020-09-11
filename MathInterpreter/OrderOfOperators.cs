namespace MathInterpreter
{
  
    public enum OrderOfOperators
    {
        NULL=-1,
        LEVEL_0=0,// Left Paranthesis
        LEVEL_1 =2,// Addition and subtraction
        LEVEL_2 =4,// UnPlus-UnMinus
        LEVEL_3 =6,// Multiply-Divide-Modulus
        LEVEL_4 =8,// Degree-Sqrt
        LEVEL_5=10// Sin,Cos,Tan,Cot,Sinh,Cosh,Tanh,Log,Ln,Exp,Abs
    }
   
}
