using System;
using System.Text.RegularExpressions;

namespace CyberMath.Structures.QuadraticEquation
{
    /// <summary>
    /// Represents a class for building quadratic equation<para/>
    /// <example>1x^2+2.5x+7=0 || 1.5x^2+2x+7.1</example>
    /// </summary>
    public class QuadraticEquation
    {
        private readonly Regex _quadraticEquationPattern = new Regex("([+-]?\\d+|[+-]?\\d+\\.[\\d]{1,})[Xx]\\^2([+-]?\\d+|[+-]?\\d+\\.[\\d]{1,})[Xx]([+-]?\\d+|[+-]?\\d+\\.[\\d]{1,})");

        /// <summary>
        /// Represent a first argument in <see cref="QuadraticEquation"/>
        /// </summary>
        public double A { get; }
        /// <summary>
        /// Represent a second argument in <see cref="QuadraticEquation"/>
        /// </summary>
        public double B { get; }
        /// <summary>
        /// Represent a third argument in <see cref="QuadraticEquation"/>
        /// </summary>
        public double C { get; }
        
        /// <summary>
        /// Determinant of current <see cref="QuadraticEquation"/>
        /// </summary>
        public double Determinant { get; private set; }

        /// <summary>
        /// First calculated root of <see cref="QuadraticEquation"/>
        /// </summary>
        public double? FirstRoot { get; private set; }
        /// <summary>
        /// First calculated root of <see cref="QuadraticEquation"/>
        /// </summary>
        public double? SecondRoot { get; private set; }

        /// <summary>
        /// Creating a new instance of <see cref="QuadraticEquation"/> with 3 <seealso cref="double"/> arguments: <paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>
        /// </summary>
        /// <param name="a">x^2</param>
        /// <param name="b">x</param>
        /// <param name="c"></param>
        public QuadraticEquation(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
            CalculateDeterminant();
        }

        /// <summary>
        /// Creating a new instance of <see cref="QuadraticEquation"/> and parse string for searching a quadratic equation in <paramref name="quadraticEquation"/>
        /// </summary>
        /// <param name="quadraticEquation">A string to parse</param>
        public QuadraticEquation(string quadraticEquation)
        {
            quadraticEquation = quadraticEquation.Replace(" ", string.Empty);
            var match = _quadraticEquationPattern.Match(quadraticEquation);
            if (!match.Success) throw new Exception("Input quadratic equation is not valid");
            A = double.Parse(match.Groups[1].Value);
            B = double.Parse(match.Groups[2].Value);
            C = double.Parse(match.Groups[3].Value);
            CalculateDeterminant();
        }

        /// <summary>
        /// Calculates a <see cref="Determinant"/> and then <see cref="FirstRoot"/> &amp; <see cref="SecondRoot"/>
        /// </summary>
        private void CalculateDeterminant()
        {
            Determinant = Math.Pow(B, 2) - 4 * A * C;
            if (Determinant < 0) return;
            CalculateRoots();
        }

        /// <summary>
        /// Calculates <see cref="FirstRoot"/> and <see cref="SecondRoot"/>
        /// </summary>
        private void CalculateRoots()
        {
            if (Determinant == 0)
            {
                SecondRoot = FirstRoot = -B / (2 * A);
                return;
            }
            FirstRoot = (-B + Math.Sqrt(Determinant)) / (2 * A);
            SecondRoot = (-B - Math.Sqrt(Determinant)) / (2 * A);
        }
    }
}
