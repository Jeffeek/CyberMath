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
            var equation = new Structures.Equations.QuadraticEquation.QuadraticEquation(a, b, c);
            double expectedDeterminant = 16;
            double actualDeterminant = equation.Determinant;
            Assert.AreEqual(expectedDeterminant, actualDeterminant);
            double expectedFirstRoot = 2d;
            double actualFirstRoot = equation.FirstRoot.Value;
            Assert.AreEqual(expectedFirstRoot, actualFirstRoot);
            double expectedSecondRoot = 1.2d;
            double actualSecondRoot = equation.SecondRoot.Value;
            Assert.AreEqual(expectedSecondRoot, actualSecondRoot);
        }

        [TestMethod]
        public void QuadraticEquation_stringParse_w0()
        {
            var equation = new Structures.Equations.QuadraticEquation.QuadraticEquation("5x^2 -16x+12=0");
            double expectedDeterminant = 16;
            double actualDeterminant = equation.Determinant;
            Assert.AreEqual(expectedDeterminant, actualDeterminant);
            double expectedFirstRoot = 2d;
            double actualFirstRoot = equation.FirstRoot.Value;
            Assert.AreEqual(expectedFirstRoot, actualFirstRoot);
            double expectedSecondRoot = 1.2d;
            double actualSecondRoot = equation.SecondRoot.Value;
            Assert.AreEqual(expectedSecondRoot, actualSecondRoot);
        }

        [TestMethod]
        public void QuadraticEquation_stringParse_wo0()
        {
            var equation = new Structures.Equations.QuadraticEquation.QuadraticEquation("5x^2 -16x  +12");
            double expectedDeterminant = 16;
            double actualDeterminant = equation.Determinant;
            Assert.AreEqual(expectedDeterminant, actualDeterminant);
            double expectedFirstRoot = 2d;
            double actualFirstRoot = equation.FirstRoot.Value;
            Assert.AreEqual(expectedFirstRoot, actualFirstRoot);
            double expectedSecondRoot = 1.2d;
            double actualSecondRoot = equation.SecondRoot.Value;
            Assert.AreEqual(expectedSecondRoot, actualSecondRoot);
        }
    }
}
