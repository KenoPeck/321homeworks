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
            HW2Form hW2Form = new HW2Form();
        }

        [Test]
        public void TestMethod1()
        {
            Assert.That(HashMethod.Method1(HW2Form.numList), Is.EqualTo(HW2Form.numList.Distinct<int>().Count()));
        }
    }
}