namespace HW2.Tests
{
    using NUnit.Framework;
    using HW2.Forms;
    using HW2.Methods;

    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            HW2Form hW2Form = new HW2Form(); // instantiating HW2Form
        }

        [Test]

        
        
        /// confirms that hash method returns correct number of distinct integers
        public void TestHashMethod()
        {
            Assert.That(HashMethod.Method1(HW2Form.numList), Is.EqualTo(HW2Form.numList.Distinct<int>().Count()));
        }

        [Test]
        
        /// confirms that fixed storage method returns correct number of distinct integers
        public void TestFixedStorageMethod()
        {
            Assert.That(FixedStorageMethod.Method2(HW2Form.numList), Is.EqualTo(HW2Form.numList.Distinct<int>().Count()));
        }

        [Test]

        /// confirms that Sorted method returns correct number of distinct integers
        public void TestSortedMethod()
        {
            Assert.That(SortedMethod.Method3(HW2Form.numList), Is.EqualTo(HW2Form.numList.Distinct<int>().Count()));
        }
    }
}