namespace MathInterpreter
{
    public class KeyNumberPair : NumberResult
    {
        public void SetKeyword(string keyword)
        {
            this._keyword = keyword;
        }
        internal string _keyword { get; set; }
        public override string Keyword { get => _keyword; set => _keyword = value; }
    }
    public class PredefinedConstant : KeyNumberPair
    {

    }
    public class KnownVariable : KeyNumberPair
    {
      
    }
}
