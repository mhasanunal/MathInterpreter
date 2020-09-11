﻿namespace MathInterpreter.Operators
{
    public sealed class Subtraction : MathMetaBase, IMathOperator
    {
            public Subtraction()
            {
                this.Keyword = "-";
                this.Priority = OrderOfOperators.LEVEL_1;
                this.RightAssociated = false;
                this.NumberOfArguments = 2;

            }
            public override void Operate(ref double result, params double[] args)
            {
                result = args[0] - args[1];
            }
        }
    }
