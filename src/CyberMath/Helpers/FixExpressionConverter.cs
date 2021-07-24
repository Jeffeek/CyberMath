#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace CyberMath.Helpers
{
    /// <summary>
    ///     Class for converting xFix <see langword="expression"/> into yFix: <br/>
    ///     <example>
    ///         <see langword="Infix -> Postfix"/> => <see langword="A+B*C/(E-F) -> ABC*EF-/+"/><br/>
    ///         <see langword="Infix -> Prefix"/> => <see langword="A+B*C/(E-F) -> +A*B/C-EF"/><br/>
    ///         <see langword="Postfix -> Infix"/> => <see langword="ABC*EF-/+ -> (A+((B*C)/(E-F)))"/><br/>
    ///         <see langword="Postfix -> Prefix"/> => <see langword="ABC/-AK/L-* -> *-A/BC-/AKL"/><br/>
    ///         <see langword="Prefix -> Infix"/> => <see langword="+A*B/C-EF -> (A+(B*(C/(E-F))))"/><br/>
    ///         <see langword="Prefix -> Postfix"/> => <see langword="*-A/BC-/AKL -> ABC/-AK/L-*"/>
    ///     </example>
    /// </summary>
    public static class FixExpressionConverter
    {
        private static readonly char[] _operators =
        {
            '*', '-', '+', '/', '^'
        };

        private static int GetOperatorPriority(char charOperator)
        {
            switch (charOperator)
            {
                case '(':
                    return 5;

                case '^':
                    return 4;

                case '/':
                case '*':
                case '%':
                    return 3;

                case '+':
                case '-':
                    return 2;

                case ')':
                    return 1;

                default:
                    return 0;
            }
        }

        private static bool InfixValidity(string expToValidate)
        {
            var bracesBalance = 0;
            var expStatus = false;

            if (GetOperatorPriority(expToValidate[0]) > 0 && GetOperatorPriority(expToValidate[0]) != 5
                || GetOperatorPriority(expToValidate[expToValidate.Length - 1]) > 0 && GetOperatorPriority(expToValidate[expToValidate.Length - 1]) != 1
                || expToValidate.Length == 0)
                return false;

            for (var i = 0; i < expToValidate.Length; i++)
            {
                switch (expToValidate[i])
                {
                    case '(':
                        expStatus = true;
                        bracesBalance++;

                        break;

                    case ')':
                        bracesBalance--;

                        break;
                }

                if (i <= 0) continue;
                if (GetOperatorPriority(expToValidate[i]) <= 0) continue;
                if (GetOperatorPriority(expToValidate[i - 1]) <= 0) continue;

                if (expToValidate[i] == '(')
                {
                    if (!(GetOperatorPriority(expToValidate[i - 1]) > 0))
                        return false;
                }
                else if (expToValidate[i - 1] == ')')
                {
                    if (!(GetOperatorPriority(expToValidate[i]) > 0))
                        return false;
                }
                else
                {
                    return false;
                }
            }

            if (bracesBalance == 0 && expStatus || bracesBalance == 0 && expStatus == false)
                return true;

            if (bracesBalance != 0)
                return false;

            return false;
        }

        /// <summary>
        ///     Converts an <see langword="INFIX"/> <paramref name="expression"/> into <see langword="POSTFIX"/>
        /// </summary>
        /// <param name="expression">Expression to convert</param>
        /// <returns>Converted <see langword="POSTFIX"/> expression from <paramref name="expression"/></returns>
        /// <exception cref="ArgumentNullException">When <paramref name="expression"/> is null</exception>
        /// <exception cref="ArgumentException">When <paramref name="expression"/> is not right</exception>
        public static string InfixToPostfix(string expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (!InfixValidity(expression)) throw new ArgumentException(nameof(expression));

            var output = new Stack<char>();
            var stack = new Stack<char>();
            var postfixExpression = "";

            foreach (var t in expression)
                if (GetOperatorPriority(t) == 0)
                {
                    output.Push(t);
                }
                else
                {
                    if (!stack.Any())
                    {
                        stack.Push(t);
                    }
                    else if (GetOperatorPriority(t) > GetOperatorPriority(stack.Peek()))
                    {
                        stack.Push(t);
                    }
                    else if (GetOperatorPriority(t) <= GetOperatorPriority(stack.Peek()))
                    {
                        if (t == ')')
                        {
                            while (stack.Any() && stack.Peek() != '(')
                                output.Push(stack.Pop());

                            stack.Pop();
                        }
                        else
                        {
                            while (stack.Any() && GetOperatorPriority(t) <= GetOperatorPriority(stack.Peek()) && stack.Peek() != '(')
                                output.Push(stack.Pop());

                            stack.Push(t);
                        }
                    }
                }

            while (stack.Any()) output.Push(stack.Pop());

            var outputArray = output.ToArray()
                                    .Reverse()
                                    .ToArray();

            for (var i = 0; i < output.Count; i++)
                postfixExpression += outputArray[i];

            return postfixExpression;
        }

        /// <summary>
        ///     Converts an <see langword="INFIX"/> <paramref name="expression"/> into <see langword="PREFIX"/>
        /// </summary>
        /// <param name="expression">Expression to convert</param>
        /// <returns>Converted <see langword="PREFIX"/> expression from <paramref name="expression"/></returns>
        /// <exception cref="ArgumentNullException">When <paramref name="expression"/> is null</exception>
        /// <exception cref="ArgumentException">When <paramref name="expression"/> is not right</exception>
        public static string InfixToPrefix(string expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));
            if (!InfixValidity(expression)) throw new ArgumentException(nameof(expression));

            string reversedInfixExpression = "",
                   prefixExpression = "";

            for (var i = expression.Length; i > 0; i--)
                switch (expression[i - 1])
                {
                    case ')':
                        reversedInfixExpression += '(';

                        break;

                    case '(':
                        reversedInfixExpression += ')';

                        break;

                    default:
                        reversedInfixExpression += expression[i - 1];

                        break;
                }

            var reversedPrefixExpression = InfixToPostfix(reversedInfixExpression);

            for (var i = reversedPrefixExpression.Length; i > 0; i--)
                prefixExpression += reversedPrefixExpression[i - 1];

            return prefixExpression;
        }

        /// <summary>
        ///     Converts an <see langword="POSTFIX"/> <paramref name="expression"/> into <see langword="INFIX"/>
        /// </summary>
        /// <param name="expression">Expression to convert</param>
        /// <returns>Converted <see langword="INFIX"/> expression from <paramref name="expression"/></returns>
        /// <exception cref="ArgumentNullException">When <paramref name="expression"/> is null</exception>
        /// <exception cref="ArgumentException">When <paramref name="expression"/> is not right</exception>
        public static string PostfixToInfix(string expression)
        {
            var s = new Stack<string>();

            foreach (var t in expression)
                if (Char.IsLetter(t))
                {
                    s.Push(t.ToString());
                }
                else
                {
                    var op1 = s.Pop();
                    var op2 = s.Pop();
                    s.Push("(" + op2 + t + op1 + ")");
                }

            return s.Pop();
        }

        /// <summary>
        ///     Converts an <see langword="PREFIX"/> <paramref name="expression"/> into <see langword="INFIX"/>
        /// </summary>
        /// <param name="expression">Expression to convert</param>
        /// <returns>Converted <see langword="INFIX"/> expression from <paramref name="expression"/></returns>
        /// <exception cref="ArgumentNullException">When <paramref name="expression"/> is null</exception>
        /// <exception cref="ArgumentException">When <paramref name="expression"/> is not right</exception>
        public static string PrefixToInfix(string expression)
        {
            var s = new Stack<string>();
            var length = expression.Length;

            for (var i = length - 1; i >= 0; i--)
                if (_operators.Contains(expression[i]))
                {
                    var op1 = s.Pop();
                    var op2 = s.Pop();
                    var temp = "(" + op1 + expression[i] + op2 + ")";
                    s.Push(temp);
                }
                else
                {
                    s.Push(expression[i]
                               .ToString());
                }

            return s.Pop();
        }

        /// <summary>
        ///     Converts an <see langword="POSTFIX"/> <paramref name="expression"/> into <see langword="PREFIX"/>
        /// </summary>
        /// <param name="expression">Expression to convert</param>
        /// <returns>Converted <see langword="PREFIX"/> expression from <paramref name="expression"/></returns>
        /// <exception cref="ArgumentNullException">When <paramref name="expression"/> is null</exception>
        /// <exception cref="ArgumentException">When <paramref name="expression"/> is not right</exception>
        public static string PostfixToPrefix(string expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            var s = new Stack<string>();
            var length = expression.Length;

            for (var i = 0; i < length; i++)
                if (_operators.Contains(expression[i]))
                {
                    var op1 = s.Pop();
                    var op2 = s.Pop();
                    var temp = expression[i] + op2 + op1;
                    s.Push(temp);
                }
                else
                {
                    s.Push(expression[i]
                               .ToString());
                }

            return s.Pop();
        }

        /// <summary>
        ///     Converts an <see langword="PREFIX"/> <paramref name="expression"/> into <see langword="POSTFIX"/>
        /// </summary>
        /// <param name="expression">Expression to convert</param>
        /// <returns>Converted <see langword="POSTFIX"/> expression from <paramref name="expression"/></returns>
        /// <exception cref="ArgumentNullException">When <paramref name="expression"/> is null</exception>
        /// <exception cref="ArgumentException">When <paramref name="expression"/> is not right</exception>
        public static string PrefixToPostfix(string expression)
        {
            if (expression == null) throw new ArgumentNullException(nameof(expression));

            var s = new Stack<string>();
            var length = expression.Length;

            for (var i = length - 1; i >= 0; i--)
                if (_operators.Contains(expression[i]))
                {
                    var op1 = s.Pop();
                    var op2 = s.Pop();
                    var temp = op1 + op2 + expression[i];
                    s.Push(temp);
                }
                else
                {
                    s.Push(expression[i]
                               .ToString());
                }

            return s.Pop();
        }
    }
}