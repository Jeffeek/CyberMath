using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CyberMath.Helpers.Tests
{
    [TestClass]
    public class DeepCopyTests
    {
	    [TestMethod]
	    public void IntDeepCopy()
	    {
		    var test = 5;
		    var actual = test.SerializableDeepCopy();
			Assert.AreEqual(actual, test);
	    }

	    [TestMethod]
	    public void StringDeepCopy()
	    {
		    var test = "test";
		    var actual = test.SerializableDeepCopy();
		    test = "haha";
		    Assert.AreNotEqual(actual, test);
	    }
	}
}
