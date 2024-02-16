namespace HW3Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SaveToFileTest()
        {
            Assert.That(File.Exists("test.txt"));
        }
    }
}