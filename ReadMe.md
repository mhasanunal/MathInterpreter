# Simple Math Interpreter


It is a base library for who wants to implement such functionality that, read a specific formula, apply with variable names (maybe from db or sth) and execute result.

# Supported Functions/Operators
  - Standart operators as addition(+), subtruction(-), multiplication(*), division(/),modulus (%) 
  - Basic Trigonometric Functions (sin,cos,tan,cot)
  - Absolute,Exp,Log,Ln,Power,Round...

# With Plugin Adaptability!

  - Just write your own library by implementing "IMathOperator" interface or "IMathFunction" interface, and inherit "MathMetaBase" abstract class and voila! BaseInterpreter loads your operators and/or functions on-the-fly!


You can also:
  - Define a variable, define a constant. In this case, your variables or constants should inherit "KeyNumberPair" class.
  - For further implementation futures, you would consider inheriting "PredefinedConstant" or "KnownVariable" classes.

You need to end your library name with "....plugin.dll"
# Sample usage:
First create a simple interpreter from base class.
```csharp
 public class TestInterpreter:BaseInterpreter
    {
        public static IMathInterpreter Create()
        {
            return new TestInterpreter();
        }
    }
```
then call for parse method by creating a math expression in string.
```csharp
var exp = "(cosd(deg(pi/4)))pow(9999)";
double result = TestInterpreter.Create().Parse(exp);
// result = 673.59121423765407
```
As of the [Origin of Project] implements interpreter as:
   - ....) (argument_2) (argument_1) function (argument_0)

You should write your custom multi-argument function's expressions such.

As you can see, I simply show how to write a plug-in, consider
"f_CalculateDepositInterest" class:
```csharp
string exp = "(interest)(deposit)dinterest(amountofdays)"; // function is dinterest
double result = interpreter.Parse(exp);
// since we created interest,deposit and amountofdays "KnownVariable"s we can use them in expression. 
// or we can write our own values as:
string exp1 = "(8.25)(125000)dinterest(365)";
double result1 = interpreter.Parse(exp1);
// remember, amountofdays is first argument, deposit is second and interest is third!
```

you would want to take a look at [Origin of Project]

License
----
GNU General Public License


**Free Software, Hell Yeah!**

[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)


   [Origin of Project]: https://github.com/kirnbas/MathParserTK
