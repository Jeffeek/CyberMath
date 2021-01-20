using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CyberMath.QuadraticEquation.Tests
{
    [TestClass]
    public class PrimitiveCalculationsTests
    {
        [TestMethod]
        public void QuadraticEquation_onNumbers()
        {
            int a = 5, b = -16, c = 12;
            var equation = new ÑyberMath.Structures.Equations.QuadraticEquation(a, b, c);
            double expectedDeterminant = 16;
            var actualDeterminant = equation.Determinant;
            Assert.AreEqual(expectedDeterminant, actualDeterminant);
            var expectedFirstRoot = 2d;
            if (equation.FirstRoot != null)
            {
                var actualFirstRoot = equation.FirstRoot.Value;
                Assert.AreEqual(expectedFirstRoot, actualFirstRoot);
            }

            var expectedSecondRoot = 1.2d;
            if (equation.SecondRoot != null)
            {
                var actualSecondRoot = equation.SecondRoot.Value;
                Assert.AreEqual(expectedSecondRoot, actualSecondRoot);
            }
        }

        [TestMethod]
        public void QuadraticEquation_stringParse_w0()
        {
            var equation = new ÑyberMath.Structures.Equations.QuadraticEquation("5x^2 -16x+12=0");
            double expectedDeterminant = 16;
            var actualDeterminant = equation.Determinant;
            Assert.AreEqual(expectedDeterminant, actualDeterminant);
            var expectedFirstRoot = 2d;
            if (equation.FirstRoot != null)
            {
                var actualFirstRoot = equation.FirstRoot.Value;
                Assert.AreEqual(expectedFirstRoot, actualFirstRoot);
            }

            var expectedSecondRoot = 1.2d;
            if (equation.SecondRoot != null)
            {
                var actualSecondRoot = equation.SecondRoot.Value;
                Assert.AreEqual(expectedSecondRoot, actualSecondRoot);
            }
        }

        [TestMethod]
        public void QuadraticEquation_stringParse_wo0()
        {
            var equation = new ÑyberMath.Structures.Equations.QuadraticEquation("5x^2 -16x  +12");
            double expectedDeterminant = 16;
            var actualDeterminant = equation.Determinant;
            Assert.AreEqual(expectedDeterminant, actualDeterminant);
            var expectedFirstRoot = 2d;
            if (equation.FirstRoot != null)
            {
                var actualFirstRoot = equation.FirstRoot.Value;
                Assert.AreEqual(expectedFirstRoot, actualFirstRoot);
            }

            var expectedSecondRoot = 1.2d;
            if (equation.SecondRoot != null)
            {
                var actualSecondRoot = equation.SecondRoot.Value;
                Assert.AreEqual(expectedSecondRoot, actualSecondRoot);
            }
        }
    }
}
