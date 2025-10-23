#region Using namespaces

using System;
using System.Text.RegularExpressions;

#endregion

namespace CyberMath.Structures.Equations
{
    /// <summary>
    ///     Represents a class for building quadratic equation
    ///     <para/>
    ///     <example>1x^2+2.5x+7=0 || 1.5x^2+2x+7.1</example>
    /// </summary>
    public class QuadraticEquation
    {
        private readonly Regex _quadraticEquationPattern =
            new
                Regex(@"^([+-]?\d+(?:\.\d+)?)[Xx]\^2([+-]?\d+(?:\.\d+)?)[Xx]([+-]?\d+(?:\.\d+)?)(?:=0)?$");

        /// <summary>
        ///     Creating a new instance of <see cref="QuadraticEquation"/> with 3 <seealso cref="double"/> arguments:
        ///     <paramref name="a"/>, <paramref name="b"/>, <paramref name="c"/>
        /// </summary>
        /// <param name="a">x^2</param>
        /// <param name="b">x</param>
        /// <param name="c"></param>
        public QuadraticEquation(double a, double b, double c)
        {
            if (a == 0)
                throw new ArgumentException("Coefficient 'a' cannot be zero in a quadratic equation", nameof(a));

            A = a;
            B = b;
            C = c;
            CalculateDeterminant();
        }

        /// <summary>
        ///     Creating a new instance of <see cref="QuadraticEquation"/> and parse string for searching a quadratic equation in
        ///     <paramref name="quadraticEquation"/>
        /// </summary>
        /// <param name="quadraticEquation">A string to parse</param>
        public QuadraticEquation(string quadraticEquation)
        {
            if (string.IsNullOrWhiteSpace(quadraticEquation))
                throw new ArgumentException("Quadratic equation string cannot be null or whitespace", nameof(quadraticEquation));

            var normalized = quadraticEquation.Replace(" ", string.Empty).Replace("\t", string.Empty);
            var match = _quadraticEquationPattern.Match(normalized);

            if (!match.Success)
                throw new ArgumentException(
                    $"Input string '{quadraticEquation}' is not a valid quadratic equation format. " +
                    "Expected format: 'ax^2+bx+c' or 'ax^2+bx+c=0' (e.g., '1x^2+2x+3' or '5x^2-16x+12=0')",
                    nameof(quadraticEquation));

            if (!double.TryParse(match.Groups[1].Value, out var a))
                throw new ArgumentException($"Failed to parse coefficient 'a' from '{match.Groups[1].Value}'", nameof(quadraticEquation));

            if (a == 0)
                throw new ArgumentException("Coefficient 'a' cannot be zero in a quadratic equation", nameof(quadraticEquation));

            if (!double.TryParse(match.Groups[2].Value, out var b))
                throw new ArgumentException($"Failed to parse coefficient 'b' from '{match.Groups[2].Value}'", nameof(quadraticEquation));

            if (!double.TryParse(match.Groups[3].Value, out var c))
                throw new ArgumentException($"Failed to parse coefficient 'c' from '{match.Groups[3].Value}'", nameof(quadraticEquation));

            A = a;
            B = b;
            C = c;

            CalculateDeterminant();
        }

        /// <summary>
        ///     Represent a first argument in <see cref="QuadraticEquation"/>
        /// </summary>
        public double A { get; }

        /// <summary>
        ///     Represent a second argument in <see cref="QuadraticEquation"/>
        /// </summary>
        public double B { get; }

        /// <summary>
        ///     Represent a third argument in <see cref="QuadraticEquation"/>
        /// </summary>
        public double C { get; }

        /// <summary>
        ///     Determinant of current <see cref="QuadraticEquation"/>
        /// </summary>
        public double Determinant { get; private set; }

        /// <summary>
        ///     First calculated root of <see cref="QuadraticEquation"/>
        /// </summary>
        public double? FirstRoot { get; private set; }

        /// <summary>
        ///     First calculated root of <see cref="QuadraticEquation"/>
        /// </summary>
        public double? SecondRoot { get; private set; }

        /// <summary>
        ///     Calculates a <see cref="Determinant"/> and then <see cref="FirstRoot"/> &amp; <see cref="SecondRoot"/>
        /// </summary>
        private void CalculateDeterminant()
        {
            Determinant = Math.Pow(B, 2) - 4 * A * C;

            if (Determinant < 0) return;

            CalculateRoots();
        }

        /// <summary>
        ///     Calculates <see cref="FirstRoot"/> and <see cref="SecondRoot"/>
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