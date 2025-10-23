#region Using namespaces
using Xunit;

#endregion

namespace CyberMath.xUnit.Tests.Structures.QuadraticEquation
{
    public class PrimitiveCalculationsTests
    {
        [Fact]
        public void QuadraticEquation_onNumbers()
        {
            int a = 5, b = -16, c = 12;
            var equation = new CyberMath.Structures.Equations.QuadraticEquation(a, b, c);
            double expectedDeterminant = 16;
            var actualDeterminant = equation.Determinant;
            Assert.Equal(expectedDeterminant, actualDeterminant);
            var expectedFirstRoot = 2d;

            if (equation.FirstRoot != null)
            {
                var actualFirstRoot = equation.FirstRoot.Value;
                Assert.Equal(expectedFirstRoot, actualFirstRoot);
            }

            var expectedSecondRoot = 1.2d;

            if (equation.SecondRoot != null)
            {
                var actualSecondRoot = equation.SecondRoot.Value;
                Assert.Equal(expectedSecondRoot, actualSecondRoot);
            }
        }

        [Fact]
        public void QuadraticEquation_stringParse_w0()
        {
            var equation = new CyberMath.Structures.Equations.QuadraticEquation("5x^2 -16x+12=0");
            double expectedDeterminant = 16;
            var actualDeterminant = equation.Determinant;
            Assert.Equal(expectedDeterminant, actualDeterminant);
            var expectedFirstRoot = 2d;

            if (equation.FirstRoot != null)
            {
                var actualFirstRoot = equation.FirstRoot.Value;
                Assert.Equal(expectedFirstRoot, actualFirstRoot);
            }

            var expectedSecondRoot = 1.2d;

            if (equation.SecondRoot != null)
            {
                var actualSecondRoot = equation.SecondRoot.Value;
                Assert.Equal(expectedSecondRoot, actualSecondRoot);
            }
        }

        [Fact]
        public void QuadraticEquation_stringParse_wo0()
        {
            var equation = new CyberMath.Structures.Equations.QuadraticEquation("5x^2 -16x  +12");
            double expectedDeterminant = 16;
            var actualDeterminant = equation.Determinant;
            Assert.Equal(expectedDeterminant, actualDeterminant);
            var expectedFirstRoot = 2d;

            if (equation.FirstRoot != null)
            {
                var actualFirstRoot = equation.FirstRoot.Value;
                Assert.Equal(expectedFirstRoot, actualFirstRoot);
            }

            var expectedSecondRoot = 1.2d;

            if (equation.SecondRoot != null)
            {
                var actualSecondRoot = equation.SecondRoot.Value;
                Assert.Equal(expectedSecondRoot, actualSecondRoot);
            }
        }
    }
}