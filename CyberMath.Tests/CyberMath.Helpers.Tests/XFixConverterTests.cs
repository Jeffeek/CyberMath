#region Using namespaces

using Microsoft.VisualStudio.TestTools.UnitTesting;

#endregion

namespace CyberMath.Helpers.Tests
{
    [TestClass]
    public class XFixConverterTests
    {
        [TestMethod]
        public void InfixToPostfixTest()
        {
            var infixes = new[]
                          {
                              "x^y/(5*z)+2",
                              "a+b*(c^d-e)^(f+g*h)-i",
                              "(((a*b)+(c/d))-e)",
                              "A+B-C",
                              "A+B*C",
                              "(A+B)/(C-D)",
                              "((A+B)*(C-D)+E)/(F+G)",
                              "A+B*C/D-E",
                              "(A+B*(C-D))/E",
                              "A*(B+C)",
                              "(X*Y+Z)"
                          };

            var postfixes = new[]
                            {
                                "xy^5z*/2+",
                                "abcd^e-fgh*+^*+i-",
                                "ab*cd/+e-",
                                "AB+C-",
                                "ABC*+",
                                "AB+CD-/",
                                "AB+CD-*E+FG+/",
                                "ABC*D/+E-",
                                "ABCD-*+E/",
                                "ABC+*",
                                "XY*Z+"
                            };

            for (var i = 0; i < infixes.Length; i++)
            {
                var expected = postfixes[i];
                var actual = FixExpressionConverter.InfixToPostfix(infixes[i]);
                Assert.IsTrue(actual == expected);
            }
        }

        [TestMethod]
        public void InfixToPrefixTest()
        {
            var infixes = new[]
                          {
                              "x^y/(5*z)+2",
                              "a+b*(c^d-e)^(f+g*h)-i",
                              "(((a*b)+(c/d))-e)",
                              "A+B-C",
                              "A+B*C",
                              "(A+B)/(C-D)",
                              "((A+B)*(C-D)+E)/(F+G)",
                              "A+B*C/D-E",
                              "(A+B*(C-D))/E",
                              "A*(B+C)",
                              "(X*Y+Z)"
                          };

            var prefixes = new[]
                           {
                               "+/^xy*5z2",
                               "+a-*b^-^cde+f*ghi",
                               "-+*ab/cde",
                               "+A-BC",
                               "+A*BC",
                               "/+AB-CD",
                               "/+*+AB-CDE+FG",
                               "+A-*B/CDE",
                               "/+A*B-CDE",
                               "*A+BC",
                               "+*XYZ"
                           };

            for (var i = 0; i < infixes.Length; i++)
            {
                var expected = prefixes[i];
                var actual = FixExpressionConverter.InfixToPrefix(infixes[i]);
                Assert.IsTrue(actual == expected);
            }
        }

        [TestMethod]
        public void PostfixToInfixTest()
        {
            var infixes = new[]
                          {
                              "((a+(b*(((c^d)-e)^(f+(g*h)))))-i)",
                              "(((a*b)+(c/d))-e)",
                              "((A+B)-C)",
                              "(A+(B*C))",
                              "((A+B)/(C-D))",
                              "((((A+B)*(C-D))+E)/(F+G))",
                              "((A+((B*C)/D))-E)",
                              "((A+(B*(C-D)))/E)",
                              "(A*(B+C))",
                              "((X*Y)+Z)"
                          };

            var postfixes = new[]
                            {
                                "abcd^e-fgh*+^*+i-",
                                "ab*cd/+e-",
                                "AB+C-",
                                "ABC*+",
                                "AB+CD-/",
                                "AB+CD-*E+FG+/",
                                "ABC*D/+E-",
                                "ABCD-*+E/",
                                "ABC+*",
                                "XY*Z+"
                            };

            for (var i = 0; i < infixes.Length; i++)
            {
                var expected = infixes[i];
                var actual = FixExpressionConverter.PostfixToInfix(postfixes[i]);
                Assert.IsTrue(actual == expected);
            }
        }

        [TestMethod]
        public void PrefixToInfixTest()
        {
            var infixes = new[]
                          {
                              "(((a*b)+(c/d))-e)",
                              "(A+(B-C))",
                              "(A+(B*C))",
                              "((A+B)/(C-D))",
                              "((((A+B)*(C-D))+E)/(F+G))",
                              "(A+((B*(C/D))-E))",
                              "((A+(B*(C-D)))/E)",
                              "(A*(B+C))",
                              "((X*Y)+Z)"
                          };

            var prefixes = new[]
                           {
                               "-+*ab/cde",
                               "+A-BC",
                               "+A*BC",
                               "/+AB-CD",
                               "/+*+AB-CDE+FG",
                               "+A-*B/CDE",
                               "/+A*B-CDE",
                               "*A+BC",
                               "+*XYZ"
                           };

            for (var i = 0; i < infixes.Length; i++)
            {
                var expected = infixes[i];
                var actual = FixExpressionConverter.PrefixToInfix(prefixes[i]);
                Assert.IsTrue(actual == expected);
            }
        }

        [TestMethod]
        public void PrefixToPostfixTest()
        {
            var prefixes = new[]
                           {
                               "-+*ab/cde",
                               "+A-BC",
                               "+A*BC",
                               "/+AB-CD",
                               "/+*+AB-CDE+FG",
                               "+A-*B/CDE",
                               "/+A*B-CDE",
                               "*A+BC",
                               "+*XYZ"
                           };

            var postfixes = new[]
                            {
                                "ab*cd/+e-",
                                "ABC-+",
                                "ABC*+",
                                "AB+CD-/",
                                "AB+CD-*E+FG+/",
                                "ABCD/*E-+",
                                "ABCD-*+E/",
                                "ABC+*",
                                "XY*Z+"
                            };

            for (var i = 0; i < prefixes.Length; i++)
            {
                var expected = postfixes[i];
                var actual = FixExpressionConverter.PrefixToPostfix(prefixes[i]);
                Assert.IsTrue(actual == expected);
            }
        }

        [TestMethod]
        public void PostfixToPrefixTest()
        {
            var prefixes = new[]
                           {
                               "-+*ab/cde",
                               "+A-BC",
                               "+A*BC",
                               "/+AB-CD",
                               "/+*+AB-CDE+FG",
                               "+A-*B/CDE",
                               "/+A*B-CDE",
                               "*A+BC",
                               "+*XYZ"
                           };

            var postfixes = new[]
                            {
                                "ab*cd/+e-",
                                "ABC-+",
                                "ABC*+",
                                "AB+CD-/",
                                "AB+CD-*E+FG+/",
                                "ABCD/*E-+",
                                "ABCD-*+E/",
                                "ABC+*",
                                "XY*Z+"
                            };

            for (var i = 0; i < prefixes.Length; i++)
            {
                var expected = prefixes[i];
                var actual = FixExpressionConverter.PostfixToPrefix(postfixes[i]);
                Assert.IsTrue(actual == expected);
            }
        }
    }
}