using System;

namespace MathInterpreter
{
    public static class Extentions
    {
        public static double GetNumber(this IMathMeta meta)
        {
            if (meta.IsNumber())
            {
                return ((NumberResult)meta);
            }
            throw new ArgumentException("unexpected \"number-->object\" cast." +
                " Numbers should parsed as "+typeof(NumberResult).FullName+" type!");
        }
        public static bool IsNumber(this IMathMeta meta)
        {
            return typeof(NumberResult).IsAssignableFrom(meta.GetType());
        }
        public static NumberResult AsNumber(this IMathMeta meta)
        {
            if (meta.IsNumber())
            {
                return meta as NumberResult;
            }
            throw new ArgumentException("meta is not a number!");
        }
        public static double GetValue(this IMathMeta meta)
        {
            var value = meta.AsNumber();
            return value.Value;
        }
        public static bool Is(this IMathMeta meta,Type @interface)
        {
            return @interface.IsAssignableFrom(meta.GetType());
        }
        public static bool IsFunction(this IMathMeta meta)
        {
            return meta.Is(typeof(IMathFunction));
        }
        public static bool IsOperator(this IMathMeta meta)
        {
            return meta.Is(typeof(IMathOperator));
        }
        public static double AsDegree(this double radiant)
        {
            return radiant * 180 / Math.PI;
        }
        public static double AsRadiant(this double angle)
        {
            return angle * Math.PI / 180;
        }
        public static double ApplyTrigonometricFunction(this double angle)
        {
            return angle.AsRadiant();
        }
        
    }
   
}
