using MathInterpreter;
using MathInterpreter.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math_1plugin
{

    public class CustomMultiplier : MathMetaBase,IMathOperator {

        public CustomMultiplier()
        {
            this.Keyword = "*+";
            this.NumberOfArguments = 2;
            this.Priority = OrderOfOperators.LEVEL_1;//like addition and subtraction
        }
        public override void Operate(ref double result, params double[] args)
        {
            result = args[0]*args[1]+args[1];
        }
    }
    public class MyCustomOperator : MathMetaBase, IMathOperator
    {
        public MyCustomOperator()
        {
            this.Keyword = "custom";
            this.NumberOfArguments = 1;
            this.Priority = OrderOfOperators.LEVEL_1;//like addition and subtraction
        }
        public override void Operate(ref double result, params double[] args)
        {
            result = Math.Sqrt(Math.Pow(args[0], args[0]));
        }
    }
    public class Variable_Interest : KnownVariable
    {
        public Variable_Interest()
        {
            this.Value = 8;
            SetKeyword("interest");
        }

    }
    public class Variable_Deposit:KnownVariable
    {
        public Variable_Deposit()
        {
            this.SetKeyword("deposit");
            Value = 100000;
        }
    }
    public class Variable_AmountOfDays : KnownVariable
    {
        public Variable_AmountOfDays()
        {
            this.SetKeyword("amountofdays");
            Value = 365;// a whole year
        }
    }
    public class f_CalculateDepositInterest : MathMetaBase, IMathFunction
    {
        public f_CalculateDepositInterest()
        {
            this.Keyword = "dinterest";
            this.NumberOfArguments = 3;
            this.Priority = OrderOfOperators.LEVEL_5;

        }
        public override void Operate(ref double result, params double[] args)
        {
            var days = args[0];
            var depositAmount = args[1];
            var interest = args[2];
            result = ((interest/100)*depositAmount/365)
                *days
                *.85;// income tax
        }
    }
}
