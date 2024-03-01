//-----------------------------------------------------------------------
// Spreadsheet_Tests.cs
// contains implementation of Spreadsheet testing class.
// <author>Cornelius Peck</author>
//-----------------------------------------------------------------------

namespace HW4.Tests
{
    using SpreadsheetEngine;

    /// <summary>
    /// Spreadsheet testing class.
    /// </summary>
    public class Spreadsheet_Tests
    {
        private Form1 testForm = new Form1();

        [SetUp]
        /// <summary>
        /// setup for testing.
        /// </summary>
#pragma warning disable SA1600 // Elements should be documented
        public void Setup()
#pragma warning restore SA1600 // Elements should be documented
        {
        }

        [Test]
        /// <summary>
        /// Test for cell value update.
        /// </summary>
#pragma warning disable SA1600 // Elements should be documented
        public void TestCellUpdate()
#pragma warning restore SA1600 // Elements should be documented
        {
            this.testForm.spreadsheet.GetCell(0, 0).Text = "5";
            this.testForm.spreadsheet.GetCell(0, 0).Text = "10";
            Assert.That(this.testForm.spreadsheet.GetCell(0, 0).Text == "10");
        }

        [Test]
        /// <summary>
        /// Test for cell update Function.
        /// </summary>
#pragma warning disable SA1600 // Elements should be documented
        public void TestValueUpdate()
#pragma warning restore SA1600 // Elements should be documented
        {
            this.testForm.spreadsheet.GetCell(0, 0).Text = "hello world!";
            this.testForm.spreadsheet.GetCell(0, 1).Text = "=A1";
            Assert.That(this.testForm.spreadsheet.GetCell(0, 1).Value == "hello world!");
        }

        [Test]
        /// <summary>
        /// Test for cell update Function using larger distance between cells.
        /// </summary>
#pragma warning disable SA1600 // Elements should be documented
        public void TestValueUpdateLarger()
#pragma warning restore SA1600 // Elements should be documented
        {
            this.testForm.spreadsheet.GetCell(49, 6).Text = "hello world!";
            this.testForm.spreadsheet.GetCell(0, 1).Text = "=G50";
            Assert.That(this.testForm.spreadsheet.GetCell(0, 1).Value == "hello world!");
        }
    }
}