namespace MathInterpreter
{
    public interface IMathMeta
    {
        string Keyword { get;  }
        OrderOfOperators Priority { get;  }
        byte NumberOfArguments { get; }
        void Operate(ref double result, params double[] args);
        bool RightAssociated { get;  }
        void AppendKeyword(char str);
        
    }
   
}
