using System;
using System.Text.RegularExpressions;

namespace CyberMath.Structures.Equations.QuadraticEquation
{
    public class QuadraticEquation
    {
        private readonly Regex _quadraticEquationPattern = new Regex("([+-]?\\d+|[+-]?\\d+\\.[\\d]{1,})[Xx]\\^2([+-]?\\d+|[+-]?\\d+\\.[\\d]{1,})[Xx]([+-]?\\d+|[+-]?\\d+\\.[\\d]{1,})");
        
        public double A { get; }
        public double B { get; }
        public double C { get; }
        public double Determinant { get; private set; }
        
        //TODO: предусмотреть корни с мнимой частью
        public double? FirstRoot { get; private set; }
        public double? SecondRoot { get; private set; }

        public QuadraticEquation(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
            CalculateDeterminant();
        }

        public QuadraticEquation(string quadraticEquation)
        {
            quadraticEquation = quadraticEquation.Replace(" ", String.Empty);
            var match = _quadraticEquationPattern.Match(quadraticEquation);
            if (!match.Success) throw new Exception("Input quadratic equation is not valid");
            A = double.Parse(match.Groups[1].Value);
            B = double.Parse(match.Groups[2].Value);
            C = double.Parse(match.Groups[3].Value);
            CalculateDeterminant();
        }

        private void CalculateDeterminant()
        {
            Determinant = Math.Pow(B, 2) - 4 * A * C;
            if (Determinant < 0) return;
            CalculateRoots();
        }

        private void CalculateRoots()
        {
            FirstRoot = (-B + Math.Sqrt(Determinant)) / (2 * A);
            SecondRoot = (-B - Math.Sqrt(Determinant)) / (2 * A);
        }
    }
}
