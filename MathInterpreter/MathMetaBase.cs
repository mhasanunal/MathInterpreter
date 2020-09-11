using System;

namespace MathInterpreter
{
    public abstract class MathMetaBase : IMathMeta
    {
        public bool RightAssociated { get; set; }
        public OrderOfOperators Priority { get; set; }
        public virtual string Keyword { get; set; }
        public byte NumberOfArguments { get; set; }

        public virtual void Operate(ref double result, params double[] args)
        {
            throw new NotImplementedException();
        }
        public static bool operator >(MathMetaBase a, MathMetaBase b)
        {
            if (a.RightAssociated)
            {
                return true;
            }
            return a.Priority > b.Priority;
        }
        public static bool operator <(MathMetaBase a, MathMetaBase b)
        {
            if (a.RightAssociated)
            {
                return false;
            }
            return a.Priority < b.Priority;
        }
        public static bool operator ==(MathMetaBase a, MathMetaBase b)
        {
            if (a?.Keyword == null || b?.Keyword == null)
            {
                return false;
            }
            return a?.Keyword == b?.Keyword;
        }
        public static bool operator !=(MathMetaBase a, MathMetaBase b)
        {
            if (a?.Keyword == null)
            {
                if (b?.Keyword == null)
                {
                    return false;
                }
                return true;
            }
            return a?.Keyword != b?.Keyword;
        }
        public static bool operator <=(MathMetaBase a, MathMetaBase b)
        {
            return a == b ? true : a < b;
        }
        public static bool operator >=(MathMetaBase a, MathMetaBase b)
        {
            return a == b ? true : a > b;
        }
        public override bool Equals(object obj)
        {
            return this.Keyword == ((MathMetaBase)(obj)).Keyword;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            var prefix = "";
            if (this.IsOperator())
            {
                prefix = BaseInterpreter.OperatorMarker.ToString();
            }
            else if (this.IsFunction())
            {
                prefix = BaseInterpreter.FunctionMarker.ToString();
            }
            else if (this.IsNumber())
            {
                prefix = BaseInterpreter.NumberMarker.ToString();
            }
            return prefix + this.Keyword;
        }
        public void AppendKeyword(char str)
        {
            Keyword += str;
        }
    }
}
