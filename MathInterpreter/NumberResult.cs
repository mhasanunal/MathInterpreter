using System;

namespace MathInterpreter
{
    public class NumberResult : MathMetaBase
    {
        public static NumberResult Create(string value)
        {
            if (value[0]==BaseInterpreter.NumberMarker)
            {
                value = value.Substring(1, value.Length - 1);
            }
            return new NumberResult
            {
                Keyword = value.ToString()
            };
        }
        public virtual double Value { get; set; }
        public override string Keyword { get { return Value.ToString(); } set { this.Value = Convert.ToDouble(value); } }
        public NumberResult()
        {
            this.Keyword = 0.ToString();
        }
        public static double operator +(NumberResult @this, NumberResult that)
        {
            return @this.Value + that.Value;
        }
        public static double operator -(NumberResult @this, NumberResult that)
        {
            return @this.Value - that.Value;
        }
        public static double operator *(NumberResult @this, NumberResult that)
        {
            return @this.Value * that.Value;
        }
        public static double operator /(NumberResult @this, NumberResult that)
        {
            return @this.Value / that.Value;
        }
        public static implicit operator double(NumberResult meta)
        {
            return meta.Value;
        }
        public override string ToString()
        {
            return BaseInterpreter.NumberMarker + this.Value.ToString();
        }
    }
}
