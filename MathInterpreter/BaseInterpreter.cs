using MathInterpreter.Operators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MathInterpreter
{

    #region LICENCE

    // ORIGINAL PROJECT:        https://github.com/kirnbas/MathParserTK
    // ORIGINAL PROJECT AUTHOR: Yerzhan Kalzhani, kirnbas@gmail.com
    // Copyright (c) 2020 @ Hasan UNAL, mhasanunal@gmail.com

    // This program is free software: you can redistribute it and/or modify
    // it under the terms of the GNU General Public License as published by
    // the Free Software Foundation, either version 3 of the License, or
    // (at your option) any later version.

    // This program is distributed in the hope that it will be useful,
    // but WITHOUT ANY WARRANTY; without even the implied warranty of
    // MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    // GNU General Public License for more details.

    // You should have received a copy of the GNU General Public License
    // along with this program.  If not, see <http://www.gnu.org/licenses/> 

    #endregion
    public abstract class BaseInterpreter : IMathInterpreter
    {
        public BaseInterpreter()
        {
            var operators = Load<IMathOperator>().ToList();
            var functions = Load<IMathFunction>().ToList();
            SupportedFunctions = functions;
            SupportedOperators = operators;
            LoadContstants();

        }

        #region PUBLIC_MEMBERS

        public List<KeyNumberPair> SupportedConstants = new List<KeyNumberPair>();
        #endregion

        #region INTERNAL_MEMBERS
        internal const char OperatorMarker = '$';
        internal const char NumberMarker = '#';
        internal const char FunctionMarker = '@';
        internal static string DecimalSeperatorAccordingToCurrentCulture = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        internal List<IMathOperator> SupportedOperators = new List<IMathOperator>();
        internal List<IMathFunction> SupportedFunctions = new List<IMathFunction>();
        #endregion

        #region PUBLIC_METHODS
        public double Parse(string expression)
        {
            return Calculate(ConvertToReversePolishNotation(Format(expression)));

        }
        public StringBuilder SyntaxAnalysisInfixNotation(IMathMeta token, StringBuilder outputString, Stack<MathMetaBase> block)
        {
            if (token.IsNumber())
            {
                outputString.Append(token.ToString());
            }
            else if (token.IsFunction()
                ||
                token.GetType().IsAssignableFrom(typeof(LeftParenthesis))
                )
            {
                block.Push(token as MathMetaBase);
            }
            else if (token.GetType().IsAssignableFrom(typeof(RightParenthesis)))
            {
                string elem;
                while ((elem = block.Pop().ToString()) != new LeftParenthesis().ToString())
                {
                    outputString.Append(elem);
                }

                if (block.Count > 0 &&
                    block.Peek().IsFunction())
                {
                    outputString.Append(block.Pop());
                }
            }
            else
            {
                while (block.Count > 0
                    &&
                    (token as MathMetaBase) < block.Peek())
                {
                    outputString.Append(block.Pop().ToString());
                }
                block.Push(token as MathMetaBase);
            }
            return outputString;
        }
        public IMathMeta LexicalAnalysisInFixNotation(string expression, ref int position)
        {
            // Receive first char
            StringBuilder token = new StringBuilder();
            token.Append(expression[position]);

            IMathOperator @operator;
            if (CHECK_IF_OPERATOR(expression, ref position, out @operator))
                return @operator;

            if (Char.IsLetter(expression[position]))
                while (++position < expression.Length && Char.IsLetter(expression[position]))
                    token.Append(expression[position]);

            if (char.IsLetter(token[0])// if it is a function
                && SupportedFunctions.Any(c => c.Keyword.StartsWith(token.ToString()))
                )
            {
                if (SupportedFunctions.Any(c => c.Keyword == token.ToString()))
                {
                    return SupportedFunctions.FirstOrDefault(c => c.Keyword == token.ToString());
                }
            }

            if (char.IsLetter(token[0])
                && SupportedConstants.Any(c => c.Keyword.StartsWith(token.ToString())))
            // if it is a constant
            {
                return SupportedConstants.FirstOrDefault(c => c.Keyword == token.ToString());
            }

            if (Char.IsDigit(token[0]) || token[0].ToString() == DecimalSeperatorAccordingToCurrentCulture)
            {
                // Read number
                // Read the whole part of number
                if (Char.IsDigit(token[0]))
                {
                    while (++position < expression.Length
                    && Char.IsDigit(expression[position]))
                    {
                        token.Append(expression[position]);
                    }
                }
                else
                {
                    // Because system decimal separator
                    // will be added below
                    token.Clear();
                }

                // Read the fractional part of number
                if (position < expression.Length
                    && expression[position].ToString() == DecimalSeperatorAccordingToCurrentCulture)
                {
                    // Add current system specific decimal separator
                    token.Append(DecimalSeperatorAccordingToCurrentCulture);

                    while (++position < expression.Length
                    && Char.IsDigit(expression[position]))
                    {
                        token.Append(expression[position]);
                    }
                }

                // Read scientific notation (suffix)
                if (position + 1 < expression.Length && expression[position] == 'e'
                    && (Char.IsDigit(expression[position + 1])
                        || (position + 2 < expression.Length
                            && (expression[position + 1] == '+'
                                || expression[position + 1] == '-')
                            && Char.IsDigit(expression[position + 2]))))
                {
                    token.Append(expression[position++]); // e

                    if (expression[position] == '+' || expression[position] == '-')
                        token.Append(expression[position++]); // sign

                    while (position < expression.Length
                        && Char.IsDigit(expression[position]))
                    {
                        token.Append(expression[position++]); // power
                    }

                    // Convert number from scientific notation to decimal notation
                }

                return NumberResult.Create(token.ToString());

            }
            else
            {
                throw new ArgumentException("Unknown token in expression");
            }
        }
        #endregion

        #region PRIVATE_METHODS
        private Stack<double> SyntaxAnalysisRPN(Stack<double> stack, IMathMeta token)
        {
            // if it's operand then just push it to stack
            if (token.IsNumber())
            {
                stack.Push(token.GetNumber());
            }
            else
            {
                double rst = 0;

                Stack<double> arguments = new Stack<double>();
                for (int i = 0; i < token.NumberOfArguments; i++)
                    arguments.Push(stack.Pop());

                token.Operate(ref rst, arguments.ToArray());

                stack.Push(rst);
            }

            return stack;
        }
        private bool CHECK_IF_OPERATOR(string expression, ref int position, out IMathOperator @operator)
        {
            @operator = null;
            var currentChar = expression[position];
            var list = SupportedOperators.Where(c => c.Keyword.StartsWith(currentChar.ToString()));
            if (list.Count() > 0)
            {
                var start = position;
                var end = start;
                string keyword = expression[start].ToString();
                if (keyword != "("
                    &&
                    keyword!=")")
                {
                    while (++end < expression.Length 
                        && (!char.IsDigit(expression[end])&&expression[end]!='('&& expression[end]!=')'))
                    {
                        keyword += expression[end];
                    }
                }
                var supportedOperator = (IMathOperator)SupportedOperators.FirstOrDefault(o => o.Keyword.StartsWith(keyword));

                @operator = supportedOperator;

                return supportedOperator != null ? (position = start+@operator.Keyword.Length) > -1 : false;
            }
            return false;
        }
        private string Format(string expression)
        {
            if (string.IsNullOrEmpty(expression))
            {
                throw new ArgumentNullException("Expression is null or empty");
            }

            StringBuilder formattedString = new StringBuilder();
            int balanceOfParenth = 0; // Check number of parenthesis

            // Format string in one iteration and check number of parenthesis
            // (this function do 2 tasks because performance priority)
            for (int i = 0; i < expression.Length; i++)
            {
                char ch = expression[i];

                if (ch == '(')
                {
                    balanceOfParenth++;
                }
                else if (ch == ')')
                {
                    balanceOfParenth--;
                }

                if (Char.IsWhiteSpace(ch))
                {
                    continue;
                }
                else if (Char.IsUpper(ch))
                {
                    formattedString.Append(Char.ToLower(ch));
                }
                else
                {
                    formattedString.Append(ch);
                }
            }

            if (balanceOfParenth != 0)
            {
                throw new FormatException("Number of left and right parenthesis is not equal");
            }

            return formattedString.ToString();
        }
        #endregion

        #region INTERNAL_METHODS
        internal double Calculate(string reversePolishNotaionExpression)
        {
            int pos = 0; // Current position of lexical analysis
            var stack = new Stack<double>(); // Contains operands

            // Analyse entire expression
            while (pos < reversePolishNotaionExpression.Length)
            {
                var token = LexicalAnalysisRPN(reversePolishNotaionExpression, ref pos);

                stack = SyntaxAnalysisRPN(stack, token);
            }

            // At end of analysis in stack should be only one operand (result)
            if (stack.Count > 1)
            {
                throw new ArgumentException("Excess operand");
            }

            return stack.Pop();
        }
        internal string ConvertToReversePolishNotation(string expression)
        {
            int pos = 0; // Current position of lexical analysis
            StringBuilder outputString = new StringBuilder();
            Stack<MathMetaBase> stack = new Stack<MathMetaBase>();

            // While there is unhandled char in expression
            while (pos < expression.Length)
            {
                IMathMeta token = LexicalAnalysisInFixNotation(expression, ref pos);

                outputString = SyntaxAnalysisInfixNotation(token, outputString, stack);
            }

            // Pop all elements from stack to output string            
            while (stack.Count > 0)
            {
                // There should be only operators
                if (stack.Peek().IsOperator())
                {
                    outputString.Append(stack.Pop());
                }
                else
                {
                    throw new FormatException("Format exception,"
                    + " there is function without parenthesis");
                }
            }

            return outputString.ToString();
        }
        internal IMathMeta LexicalAnalysisRPN(string expression, ref int pos)
        {

            StringBuilder keyword = new StringBuilder();
            // Read token from marker to next marker

            keyword.Append(expression[pos++]);

            while (pos < expression.Length && expression[pos] != NumberMarker
                && expression[pos] != OperatorMarker

                && expression[pos] != FunctionMarker)
            {
                keyword.Append(expression[pos++]);
            }
            return GET_INSTANCE_FOR(keyword.ToString());
        }
        internal IMathMeta GET_INSTANCE_FOR(string keyword)
        {
            var hasOperatorforKeyword = SupportedOperators?.FirstOrDefault(c => c.ToString() == keyword);
            if (hasOperatorforKeyword != null)
            {
                return hasOperatorforKeyword;
            }
            var hasFunction = SupportedFunctions?.FirstOrDefault(c => c.ToString() == keyword);
            if (hasFunction != null)
            {
                return hasFunction;
            }
            var hasConstant = SupportedConstants?.FirstOrDefault(c => c.ToString() == keyword);
            if (hasConstant != null)
            {
                return hasConstant;
            }
            if (keyword[0] != BaseInterpreter.NumberMarker ||
                (
                !char.IsDigit(keyword[0])
                && keyword[0] != BaseInterpreter.NumberMarker))
            {
                throw new Exception("Argument is not understood! Define " + keyword + "!");
            }
            return NumberResult.Create(keyword);
        }
        internal virtual void LoadContstants()
        {
            var constants = Load<KeyNumberPair>().ToList();
            SupportedConstants = constants;
        }
        internal virtual List<T> Load<T>()
            where T : class
        {
            var type = typeof(T);
            List<T> list = new List<T>();
            var currentContext = AppDomain.CurrentDomain.GetAssemblies().SelectMany(c => c.GetTypes()).Where(p => !p.IsInterface && type.IsAssignableFrom(p));
            List<Type> plugins = new List<Type>();
            foreach (var item in Directory.EnumerateFiles(AppDomain.CurrentDomain.BaseDirectory, "*plugin.dll", SearchOption.AllDirectories))
            {
                var plug = Assembly.LoadFile(item).GetTypes().Where(p =>
                !p.IsInterface && type.IsAssignableFrom(p));
                plugins.AddRange(plug);
            }
            currentContext = currentContext.Union(plugins);

            foreach (var item in currentContext)
            {
                var instance = (T)Activator.CreateInstance(item);
                list.Add(instance);
            }
            return list;
        }
        #endregion

    }
}
