using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathInterpreter.Tests
{
    [TestClass]
    public class InterpreterTests
    {
        static IMathInterpreter interpreter = TestInterpreter.Create();
        [TestMethod]
        public void PowerOfTwo()
        {
            var exp = "(4)pow(2)"; // 4th power of two
            var result = interpreter.Parse(exp);
            Assert.AreEqual(Math.Pow(2, 4), result);
        }

        [TestMethod]
        public void PowerViaOperator()
        {
            var exp = "125^(1/3)"; // 
            var result = interpreter.Parse(exp);
            double power = (1.0 / 3.0);
            var actual = Math.Pow(125, power);
            Assert.AreEqual(actual, result);
        }
        [TestMethod]
        public void tangent()
        {
            var degree = 45.0;
            var exp = "tand("+degree+")";
            var exp1 = "tanr(pi/4)";
            var result1 = interpreter.Parse(exp);
            var result2 = interpreter.Parse(exp1);

            // for some reason, I cannot use tanr via transform for a given degree as such:
            // var asRadiant = degree.AsRadiant();
            // var exp2 = "tanr(" + asRadiant.ToString("G17") + ")";
            // ToString("G17") from ms documents linked below:
            // https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-numeric-format-strings?redirectedfrom=MSDN#FFormatString
            // var result3 = interpreter.Parse(exp2);
            // // result3 = 0.99999999999999922 instead of actual result of 0.99999999999999989
            // so radiant trigonometric functions should be used with only radiants!
            var actual = Math.Tan(Math.PI / 4);
            var actual1 = Math.Tan(degree.AsRadiant());
            Assert.AreEqual(actual, result1);
            Assert.AreEqual(actual, result2);
        }
        [TestMethod]
        public void sinus_via_degree_to_radiant()
        {
            var exp = "sinr(rad(90))";
            var result = interpreter.Parse(exp);
            var actual = Math.Sin(Math.PI / 2);
            Assert.AreEqual(actual, result);
        }
        [TestMethod]
        public void RADIANT_TO_DEGREE_InternalTrigonometricFunctionTest()
        {
            var radiant = Math.PI / 2;//90 degrees;
            var asDegree = radiant.AsDegree();
            Assert.AreEqual(90, asDegree);
        }
        [TestMethod]
        public void DEGREE_TO_RADIANT_InternalTrigonometricFunctionTest()
        {
            var degree = 180.0;// pi
            var asRadiant = degree.AsRadiant();
            Assert.AreEqual(Math.PI, asRadiant);
        }
        [TestMethod]
        public void sinus_via_radiant_to_degree()
        {
            var exp = "sind(deg(pi/2))";// since we predefined pi
            var result = interpreter.Parse(exp);
            Assert.AreEqual(Math.Sin(Math.PI / 2), result);
        }
        [TestMethod]
        public void PriorityOfOperations()
        {
            var exp = "20*(8-5)-(4+5)/10";
            var result = interpreter.Parse(exp);
            var actual = 20 * (8 - 5) - (4 + 5.0) / 10;
            Assert.AreEqual(actual, result);
        }

        [TestMethod]
        public void CustomMultiplier()
        {
            var exp = "5*+9";
            var result = interpreter.Parse(exp);
            var actualResult = 5 * 9 + 9;
            Assert.AreEqual(actualResult, result);
        }
        [TestMethod]
        public void MoreComplex()
        {
            var exp = "(cosd(deg(pi/4)))pow(9999)";
            var result = interpreter.Parse(exp);
            var asRadiant = (Math.PI / 4);
            var cos = Math.Cos(asRadiant);
            var actualResult = Math.Pow(9999, cos);
            Assert.AreEqual(actualResult, result);
        }

        [TestMethod]
        public void CosinusInRadiantsWorks()
        {

            var exp = "cosr(pi*5/2)";
            var result = interpreter.Parse(exp);
            var actual = Math.Cos(Math.PI * 5 / 2);
            Assert.AreEqual(actual, result);

        }
        [TestMethod]
        public void _90_DegreesWorksOnBothSinAndCos()
        {
            var exp1 = "cosd(90)";
            var exp2 = "sind(90)";
            var result1 = interpreter.Parse(exp1);
            var result2 = interpreter.Parse(exp2);
            Assert.AreEqual(Math.Cos(Math.PI / 2), result1);
            Assert.AreEqual(Math.Sin(Math.PI / 2), result2);
        }

        [TestMethod]
        public void SquareRootOfTangent_45()
        {
            var exp = "sqrt(tanr(pi/4))";
            var result = interpreter.Parse(exp);
            var actual = Math.Sqrt(Math.Tan(Math.PI / 4));
            Assert.AreEqual(actual, result);
        }

        [TestMethod]
        public void PluginOperatorTest()
        {
            var exp = "5custom";
            var result = interpreter.Parse(exp);
            var actual = Math.Sqrt(Math.Pow(5, 5)); ;
            Assert.AreEqual(actual, result);
        }
        [TestMethod]
        public void PluginVariableAndFunctionTest()
        {
            var exp = "(interest)(deposit)dinterest(amountofdays)";
            var result = interpreter.Parse(exp);
            var actual = 6800;
            Assert.AreEqual(actual, result);
        }
    }
}
