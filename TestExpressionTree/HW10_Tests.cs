// <copyright file="HW10_Tests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

// Tests have documentation above each test set which weren't being detected by stylecop
#pragma warning disable SA1600 // Elements should be documented

namespace HW10.Tests
{
    using System.Globalization;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Xml;
    using ExpressionTree;
    using SpreadsheetEngine;

    [TestFixture]
    public class HW10_Tests
    {
        private Form1 testForm = new Form1();

        // Loading/Saving Tests ----------------------------------------------------------------------------------------------------
        [Test]
        /// <summary>
        /// Test saving text and color to an xml file.
        /// </summary>
        public void TestSaveTextAndColorSpreadSheet()
        {
            this.testForm = new Form1();
            this.testForm.Spreadsheet.GetCell(0, 0).Text = "5";
            this.testForm.Spreadsheet.GetCell(0, 1).Text = "=A1";
            this.testForm.Spreadsheet.GetCell(0, 2).Text = "=B1";
            this.testForm.Spreadsheet.GetCell(0, 3).Text = "10";
            this.testForm.Spreadsheet.GetCell(0, 0).BGColor = 0xFF0000;
            this.testForm.Spreadsheet.GetCell(0, 1).BGColor = 0x00FF00;
            FileStream file = File.Create("test.xml");
            this.testForm.Spreadsheet.SaveSpreadSheet(file);
            Assert.That(File.Exists("test.xml"), Is.True);
        }

        [Test]
        /// <summary>
        /// Test saving & loading text to an xml file.
        /// </summary>
        public void TestLoadTextSpreadSheet()
        {
            this.testForm = new Form1();
            this.testForm.Spreadsheet.GetCell(0, 0).Text = "5";
            this.testForm.Spreadsheet.GetCell(0, 1).Text = "=A1";
            this.testForm.Spreadsheet.GetCell(0, 2).Text = "=B1";
            this.testForm.Spreadsheet.GetCell(0, 3).Text = "10";
            FileStream saveFile = File.Create("test.xml");
            this.testForm.Spreadsheet.SaveSpreadSheet(saveFile);
            saveFile.Close();
            Assert.That(File.Exists("test.xml"), Is.True);
            FileStream loadFile = File.Open("test.xml", FileMode.Open);
            this.testForm.Spreadsheet.LoadSpreadSheet(loadFile);
            loadFile.Close();
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 0).Text, Is.EqualTo("5"));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 1).Text, Is.EqualTo("=A1"));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 2).Text, Is.EqualTo("=B1"));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 3).Text, Is.EqualTo("10"));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 2).Value, Is.EqualTo("5"));
        }

        [Test]
        /// <summary>
        /// Test saving cell colors to an xml file.
        /// </summary>
        public void TestLoadColorSpreadSheet()
        {
            this.testForm = new Form1();
            this.testForm.Spreadsheet.GetCell(0, 0).BGColor = 0xFF0000;
            this.testForm.Spreadsheet.GetCell(0, 1).BGColor = 0x00FF00;
            FileStream saveFile = File.Create("test.xml");
            this.testForm.Spreadsheet.SaveSpreadSheet(saveFile);
            saveFile.Close();
            Assert.That(File.Exists("test.xml"), Is.True);
            FileStream loadFile = File.Open("test.xml", FileMode.Open);
            this.testForm.Spreadsheet.LoadSpreadSheet(loadFile);
            loadFile.Close();
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 0).BGColor, Is.EqualTo(0xFF0000));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 1).BGColor, Is.EqualTo(0x00FF00));
        }

        [Test]
        /// <summary>
        /// Test saving & loading text & color to an xml file.
        /// </summary>
        public void TestLoadTextAndColorSpreadSheet()
        {
            this.testForm = new Form1();
            this.testForm.Spreadsheet.GetCell(0, 0).Text = "5";
            this.testForm.Spreadsheet.GetCell(0, 1).Text = "=A1";
            this.testForm.Spreadsheet.GetCell(0, 2).Text = "=B1";
            this.testForm.Spreadsheet.GetCell(0, 3).Text = "10";
            this.testForm.Spreadsheet.GetCell(0, 0).BGColor = 0xFF0000;
            this.testForm.Spreadsheet.GetCell(0, 1).BGColor = 0x00FF00;
            FileStream saveFile = File.Create("test.xml");
            this.testForm.Spreadsheet.SaveSpreadSheet(saveFile);
            saveFile.Close();
            Assert.That(File.Exists("test.xml"), Is.True);
            FileStream loadFile = File.Open("test.xml", FileMode.Open);
            this.testForm.Spreadsheet.LoadSpreadSheet(loadFile);
            loadFile.Close();
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 0).Text, Is.EqualTo("5"));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 1).Text, Is.EqualTo("=A1"));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 2).Text, Is.EqualTo("=B1"));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 3).Text, Is.EqualTo("10"));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 2).Value, Is.EqualTo("5"));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 0).BGColor, Is.EqualTo(0xFF0000));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 1).BGColor, Is.EqualTo(0x00FF00));
        }

        [Test]
        /// <summary>
        /// Test saving & loading an updated xml file with unrecognized attributes and elements.
        /// </summary>
        public void TestLoadUpdatedXML()
        {
            this.testForm = new Form1();
            FileStream saveFile = File.Create("test.xml");
            var testXMLDoc = new XmlDocument();
            var root = testXMLDoc.CreateElement("Spreadsheet"); // Create root element.
            testXMLDoc.AppendChild(root); // Append root element to xml document.
            var cell = testXMLDoc.CreateElement("Cell"); // Create cell element.
            var index = testXMLDoc.CreateAttribute("Index"); // Create index attribute.
            index.Value = "0,0"; // Set index attribute value.
            var testAttr = testXMLDoc.CreateAttribute("Test"); // Create unused test attribute to act like a future-added attribute.
            testAttr.Value = "Test"; // Set test attribute value.
            var cellValue = testXMLDoc.CreateElement("Text"); // Create text element.
            cellValue.InnerText = "9"; // Set text element value.
            var cellBGC = testXMLDoc.CreateElement("BGColor"); // Create BGColor element.
            uint testColor = 0xFF0000;
            cellBGC.InnerText = testColor.ToString(); // Set BGColor element value.
            var testElement = testXMLDoc.CreateElement("Test"); // Create unused test element to act like a future-added element.

            // setting elements in different order than normal
            testElement.InnerText = "Test"; // Set test element value.
            cell.AppendChild(cellValue); // Append text element to cell element.
            cell.AppendChild(cellBGC); // Append BGColor element to cell element.
            cell.AppendChild(testElement); // Append testing element to cell element.
            cell.Attributes.Append(index); // Append index attribute to cell element.
            cell.Attributes.Append(testAttr); // Append testing attribute to cell element.
            root.AppendChild(cell); // Append cell element to root element.

            cell = testXMLDoc.CreateElement("Cell"); // Create cell element.
            index = testXMLDoc.CreateAttribute("Index"); // Create index attribute.
            index.Value = "0,1"; // Set index attribute value.
            testAttr = testXMLDoc.CreateAttribute("Test"); // Create test attribute.
            testAttr.Value = "Test"; // Set test attribute value.
            cellValue = testXMLDoc.CreateElement("Text"); // Create text element.
            cellValue.InnerText = "=A1/2"; // Set text element value.
            cellBGC = testXMLDoc.CreateElement("BGColor"); // Create BGColor element.
            testColor = 0x00FF00;
            cellBGC.InnerText = testColor.ToString(); // Set BGColor element value.
            testElement = testXMLDoc.CreateElement("Test"); // Create test element.
            testElement.InnerText = "Test"; // Set test element value.
            cell.AppendChild(testElement); // Append test element to cell element.
            cell.AppendChild(cellValue); // Append text element to cell element.
            cell.AppendChild(cellBGC); // Append BGColor element to cell element.
            cell.Attributes.Append(testAttr); // Append test attribute to cell element.
            cell.Attributes.Append(index); // Append index attribute to cell element.
            root.AppendChild(cell); // Append cell element to root element.

            testXMLDoc.Save(saveFile); // Save xml document to file.
            saveFile.Close(); // Close file stream.
            Assert.That(File.Exists("test.xml"), Is.True);
            FileStream loadFile = File.Open("test.xml", FileMode.Open);
            this.testForm.Spreadsheet.LoadSpreadSheet(loadFile);
            loadFile.Close();

            Assert.That(this.testForm.Spreadsheet.GetCell(0, 0).Text, Is.EqualTo("9"));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 1).Text, Is.EqualTo("=A1/2"));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 1).Value, Is.EqualTo("4.5"));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 0).BGColor, Is.EqualTo(0xFF0000));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 1).BGColor, Is.EqualTo(0x00FF00));
        }

        [Test]
        /// <summary>
        /// Test saving & loading an empty xml file.
        /// </summary>
        public void TestSaveLoadEmptySpreadSheet()
        {
            this.testForm = new Form1();
            FileStream saveFile = File.Create("test.xml");
            this.testForm.Spreadsheet.SaveSpreadSheet(saveFile);
            saveFile.Close();
            Assert.That(File.Exists("test.xml"), Is.True);
            this.testForm.Spreadsheet.GetCell(0, 0).Text = "5";
            this.testForm.Spreadsheet.GetCell(0, 1).Text = "=A1";
            this.testForm.Spreadsheet.GetCell(0, 2).Text = "=B1";
            this.testForm.Spreadsheet.GetCell(0, 3).Text = "10";
            this.testForm.Spreadsheet.GetCell(0, 0).BGColor = 0xFF0000;
            this.testForm.Spreadsheet.GetCell(0, 1).BGColor = 0x00FF00;
            this.testForm.Spreadsheet.WipeSpreadSheet();
            FileStream loadFile = File.Open("test.xml", FileMode.Open);
            this.testForm.Spreadsheet.LoadSpreadSheet(loadFile);
            loadFile.Close();
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 0).Text, Is.EqualTo(string.Empty));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 1).Text, Is.EqualTo(string.Empty));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 2).Text, Is.EqualTo(string.Empty));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 3).Text, Is.EqualTo(string.Empty));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 0).BGColor, Is.EqualTo(0xFFFFFFFF));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 1).BGColor, Is.EqualTo(0xFFFFFFFF));
        }

        // Spreadsheet Tests ----------------------------------------------------------------------------------------------------
        [Test]

        /// <summary>
        /// Test spreadsheet display of variable values.
        /// </summary>
        public void TestDisplayFormula()
        {
            this.testForm = new Form1();
            this.testForm.Spreadsheet.GetCell(0, 0).Text = "5";
            this.testForm.Spreadsheet.GetCell(0, 1).Text = "=A1";
            this.testForm.Spreadsheet.GetCell(0, 2).Text = "=B1";
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 2).Value, Is.EqualTo("5"));
        }

        [Test]

        /// <summary>
        /// Test spreadsheet handling of out of bounds cell references.
        /// </summary>
        public void TestOutOfBoundsError()
        {
            this.testForm = new Form1();
            this.testForm.Spreadsheet.GetCell(0, 0).Text = "=A51";
            this.testForm.Spreadsheet.GetCell(0, 1).Text = "=A1";
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 0).Value, Is.EqualTo("RefError"));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 1).Value, Is.EqualTo("RefError"));
            this.testForm.Spreadsheet.GetCell(0, 3).Text = "5";
            this.testForm.Spreadsheet.GetCell(0, 0).Text = "=D1";
            this.testForm.Spreadsheet.GetCell(0, 1).Text = "=A1+5";
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 0).Value, Is.EqualTo("5"));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 1).Value, Is.EqualTo("10"));
        }

        [Test]

        /// <summary>
        /// Test spreadsheet handling of invalid cell references.
        /// </summary>
        public void TestInvalidReferenceError()
        {
            this.testForm = new Form1();
            this.testForm.Spreadsheet.GetCell(0, 0).Text = "=Ba/Zg";
            this.testForm.Spreadsheet.GetCell(0, 1).Text = "=A1";
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 0).Value, Is.EqualTo("RefError"));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 1).Value, Is.EqualTo("RefError"));
            this.testForm.Spreadsheet.GetCell(0, 3).Text = "5";
            this.testForm.Spreadsheet.GetCell(0, 0).Text = "=D1";
            this.testForm.Spreadsheet.GetCell(0, 1).Text = "=A1+5";
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 0).Value, Is.EqualTo("5"));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 1).Value, Is.EqualTo("10"));
        }

        [Test]

        /// <summary>
        /// Test spreadsheet display of plain text values.
        /// </summary>
        public void TestDisplayText()
        {
            this.testForm = new Form1();
            this.testForm.Spreadsheet.GetCell(0, 0).Text = "1";
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 0).Text, Is.EqualTo("1"));
        }

        [Test]
        [TestCase("1", "2", "=A1+B1", ExpectedResult = "3")] // addition expression
        [TestCase("1", "2", "=A1/B1", ExpectedResult = "0.5")] // division expression
        [TestCase("1", "=2+2", "=A1/B1", ExpectedResult = "0.25")] // mixed expression
        [TestCase("1", "=9+A1", "=A1/B1", ExpectedResult = "0.1")] // mixed expression
        [TestCase("0", "1", "=A1/B1", ExpectedResult = "0")] // dividing zero
        [TestCase("1", "=A1", "=B1", ExpectedResult = "1")] // mixed expression
        [TestCase("5", "=A1+5", "=B1/4", ExpectedResult = "2.5")] // mixed expression

        /// <summary>
        /// Test spreadsheet arithmetic & display.
        /// </summary>
        public string TestDisplayArithmetic(string cell1, string cell2, string cell3)
        {
            this.testForm = new Form1();
            this.testForm.Spreadsheet.GetCell(0, 0).Text = cell1;
            this.testForm.Spreadsheet.GetCell(0, 1).Text = cell2;
            this.testForm.Spreadsheet.GetCell(0, 2).Text = cell3;
            return this.testForm.Spreadsheet.GetCell(0, 2).Value;
        }

        [Test]
        /// <summary>
        /// Test empty cell handling.
        /// </summary>
        public void TestEmptyCell()
        {
            this.testForm = new Form1();
            this.testForm.Spreadsheet.GetCell(0, 0).Text = string.Empty;
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 0).Value, Is.EqualTo(string.Empty));
        }

        [Test]
        /// <summary>
        /// Test unsupportedoperator exception handling.
        /// </summary>
        public void TestUnsupportedOperator()
        {
            this.testForm = new Form1();
            this.testForm.Spreadsheet.GetCell(0, 0).Text = "=2^3";
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 0).Value, Is.EqualTo("OpError"));
        }

        [Test]
        /// <summary>
        /// Test empty cell reference exception handling.
        /// </summary>
        public void TestEmptyCellReference()
        {
            this.testForm = new Form1();
            this.testForm.Spreadsheet.GetCell(0, 0).Text = "10";
            this.testForm.Spreadsheet.GetCell(0, 1).Text = "=A1/2";
            this.testForm.Spreadsheet.GetCell(0, 2).Text = "=B1/2";
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 1).Value, Is.EqualTo("5"));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 2).Value, Is.EqualTo("2.5"));
            this.testForm.Spreadsheet.GetCell(0, 0).Text = string.Empty;
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 1).Value, Is.EqualTo("RefError"));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 2).Value, Is.EqualTo("RefError"));
            this.testForm.Spreadsheet.GetCell(0, 0).Text = "20";
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 1).Value, Is.EqualTo("10"));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 2).Value, Is.EqualTo("5"));
        }

        [Test]
        /// <summary>
        /// Test background color change handling.
        /// </summary>
        public void TestColorChange()
        {
            this.testForm = new Form1();
            this.testForm.Spreadsheet.GetCell(0, 0).BGColor = 0xFF0000;
            DataGridView temp = new DataGridView();
            Type dataGridType = temp.GetType();
            PropertyInfo? propertyinfo = dataGridType.GetProperty("Style");
            if (propertyinfo != null)
            {
                var style = propertyinfo.GetValue(temp, null);
                if (style != null)
                {
                    var bgColor = style.GetType().GetProperty("BackColor");
                    if (bgColor != null)
                    {
                        System.Drawing.Color testColor = System.Drawing.Color.FromArgb(0xFF0000);
                        Assert.That(bgColor.GetValue(style, null), Is.EqualTo(testColor));
                    }
                }
            }
        }

        // Undo/redo tesing -------------------------------------------------------------------------------------------------------------------------
        [Test]

        public void TestUndo()
        {
            this.testForm = new Form1();
            var command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 0), string.Empty, "5");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 1), string.Empty, "=A1");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 2), string.Empty, "=B1");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 0), "5", "10");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            this.testForm.Spreadsheet.Undo();
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 2).Value, Is.EqualTo("5"));
        }

        [Test]

        public void TestRedo()
        {
            this.testForm = new Form1();
            var command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 0), string.Empty, "5");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 1), string.Empty, "=A1");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 2), string.Empty, "=B1");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 0), "5", "10");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            this.testForm.Spreadsheet.Undo();
            this.testForm.Spreadsheet.Redo();
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 2).Value, Is.EqualTo("10"));
        }

        [Test]

        public void TestUndoWithEmpty()
        {
            this.testForm = new Form1();
            var command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 0), string.Empty, "5");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 1), string.Empty, "=A1");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 2), string.Empty, "=B1");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 0), "5", string.Empty);
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            this.testForm.Spreadsheet.Undo();
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 2).Value, Is.EqualTo("5"));
        }

        [Test]

        public void TestRedoWithEmpty()
        {
            this.testForm = new Form1();
            var command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 0), string.Empty, "5");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 1), string.Empty, "=A1");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 2), string.Empty, "=B1");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 0), "5", string.Empty);
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            this.testForm.Spreadsheet.Undo();
            this.testForm.Spreadsheet.Redo();
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 2).Value, Is.EqualTo("RefError"));
        }

        [Test]

        public void TestUndoWithError()
        {
            this.testForm = new Form1();
            var command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 0), string.Empty, "=2^3");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 1), string.Empty, "=A1");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 2), string.Empty, "=B1");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 0), this.testForm.Spreadsheet.GetCell(0, 0).Text, "5");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            this.testForm.Spreadsheet.Undo();
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 0).Value, Is.EqualTo("OpError"));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 2).Value, Is.EqualTo("RefError"));
        }

        [Test]

        public void TestRedoWithError()
        {
            this.testForm = new Form1();
            var command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 0), string.Empty, "=2^3");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 1), string.Empty, "=A1");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 2), string.Empty, "=B1");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 0), this.testForm.Spreadsheet.GetCell(0, 0).Text, "5");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            this.testForm.Spreadsheet.Undo();
            this.testForm.Spreadsheet.Redo();
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 2).Value, Is.EqualTo("5"));
        }

        [Test]

        public void TestUndoWithArithmetic()
        {
            this.testForm = new Form1();
            var command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 0), string.Empty, "=(5*3)/2");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 1), string.Empty, "=A1");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 2), string.Empty, "=B1");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 0), this.testForm.Spreadsheet.GetCell(0, 0).Text, "=2*(4+7)");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            this.testForm.Spreadsheet.Undo();
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 2).Value, Is.EqualTo("7.5"));
        }

        [Test]

        public void TestRedoWithArithmetic()
        {
            this.testForm = new Form1();
            var command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 0), string.Empty, "=(5*3)/2");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 1), string.Empty, "=A1");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 2), string.Empty, "=B1");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            command = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 0), this.testForm.Spreadsheet.GetCell(0, 0).Text, "=2*(4+7)");
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            this.testForm.Spreadsheet.Undo();
            this.testForm.Spreadsheet.Redo();
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 2).Value, Is.EqualTo("22"));
        }

        [Test]

        public void TestUndoColorEdit()
        {
            this.testForm = new Form1();
            uint[] oldcolor = new uint[1];
            oldcolor[0] = 0xFFFFFFFF;
            var command = new CellBGCEditCommand([this.testForm.Spreadsheet.GetCell(0, 0)], oldcolor, 0xFFFFFFF1);
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            this.testForm.Spreadsheet.Undo();
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 0).BGColor, Is.EqualTo(0xFFFFFFFF));
        }

        [Test]

        public void TestRedoColorEdit()
        {
            this.testForm = new Form1();
            uint[] oldcolor = new uint[5];
            oldcolor[0] = 0xFFFFFFFF;
            var command = new CellBGCEditCommand([this.testForm.Spreadsheet.GetCell(0, 0)], oldcolor, 0xFFFFFFF1);
            command.Execute();
            this.testForm.Spreadsheet.AddUndo(command);
            this.testForm.Spreadsheet.Undo();
            this.testForm.Spreadsheet.Redo();
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 0).BGColor, Is.EqualTo(0xFFFFFFF1));
        }

        [Test]

        public void TestUndoRedoColorAndTextEdit()
        {
            this.testForm = new Form1();
            uint[] oldcolor = new uint[1];
            oldcolor[0] = 0xFFFFFFFF;
            var clrCommand = new CellBGCEditCommand([this.testForm.Spreadsheet.GetCell(0, 0)], oldcolor, 0xFFFFFFF1);
            clrCommand.Execute();
            this.testForm.Spreadsheet.AddUndo(clrCommand);
            var txtcommand = new CellTextEditCommand(this.testForm.Spreadsheet.GetCell(0, 0), string.Empty, "=(5*3)/2");
            txtcommand.Execute();
            this.testForm.Spreadsheet.AddUndo(txtcommand);
            this.testForm.Spreadsheet.Undo();
            this.testForm.Spreadsheet.Undo();
            this.testForm.Spreadsheet.Redo();
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 0).BGColor, Is.EqualTo(0xFFFFFFF1));
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 0).Value, Is.EqualTo(string.Empty));
            this.testForm.Spreadsheet.Redo();
            Assert.That(this.testForm.Spreadsheet.GetCell(0, 0).Value, Is.EqualTo("7.5"));
        }

        // ExpressionTree Tests ----------------------------------------------------------------------------------------------------
        [Test]
        [TestCase("11+22", ExpectedResult = "11 22 + ")] // addition expression
        [TestCase("22-11", ExpectedResult = "22 11 - ")] // subtraction expression
        [TestCase("3+3-3", ExpectedResult = "3 3 + 3 - ")] // addition and substraction mixed expression
        [TestCase("3+3*3", ExpectedResult = "3 3 3 * + ")] // addition and multiplication mixed expression
        [TestCase("11*22", ExpectedResult = "11 22 * ")] // multiplication expression
        [TestCase("11*22 + 5", ExpectedResult = "11 22 * 5 + ")] // multiplication and addditon mixed expression
        [TestCase("22/11", ExpectedResult = "22 11 / ")] // division expression
        [TestCase("A+1", ExpectedResult = "A 1 + ")] // Variable addition expression
        [TestCase("A+B", ExpectedResult = "A B + ")] // Pure Variable addition expression
        [TestCase("A+B*C", ExpectedResult = "A B C * + ")] // Pure Variable addition and multiplication expression
        [TestCase("A-B+C", ExpectedResult = "A B C + - ")] // Pure Variable subtraction and addition expression
        [TestCase("A*B+C", ExpectedResult = "A B * C + ")] // Pure Variable multiplication and addition expression
        [TestCase("A*(B+C)", ExpectedResult = "A B C + * ")] // Pure Variable multiplication and addition expression with parentheses
        [TestCase("A* (B + C* D) + E", ExpectedResult = "A B C D * + * E + ")] // mixed expression with parentheses

        /// <summary>
        /// Test postfix expression generation.
        /// </summary>
        /// <param name="expression">expression used in test.</param>
        public string TestPostFixExpressions(string expression)
        {
            ExpressionTree exp = new ExpressionTree(expression);
            return exp.GetPostFix();
        }

        [Test]
        [TestCase("11+22", ExpectedResult = 33.0)] // addition expression
        [TestCase("22-11", ExpectedResult = 11.0)] // subtraction expression
        [TestCase("11*22", ExpectedResult = 242.0)] // multiplication expression
        [TestCase("22/11", ExpectedResult = 2.0)] // division expression
        [TestCase("5/2", ExpectedResult = 2.5)] // division expression
        [TestCase("5-22/11", ExpectedResult = 3.0)] // mixed (-,/) expression
        [TestCase("(10-6/2)/2", ExpectedResult = 3.5)] // mixed (-,/) expression with parentheses
        [TestCase("1/0", ExpectedResult = double.PositiveInfinity)] // division by zero

        /// <summary>
        /// Test expressions with normal cases.
        /// </summary>
        /// <param name="expression">expression used in test.</param>
        public double TestEvaluateNormalCases(string expression)
        {
            ExpressionTree exp = new ExpressionTree(expression);
            return exp.Evaluate();
        }

        [Test]
        /// <summary>
        /// Test expressions with values totaling above maxvalue.
        /// </summary>
        /// <param name="expression">expression used in test.</param>
        [TestCase("2%3")]
        [TestCase("2^3")]
        public void TestEvaluateUnsupportedOperator(string expression)
        {
            Assert.That(() => new ExpressionTree(expression), Throws.TypeOf<UnsupportedOperatorException>());
        }

        [Test]
        /// <summary>
        /// Test expressions with values totaling above maxvalue.
        /// </summary>
        /// <param name="expression">expression used in test.</param>
        [TestCase("(()")]
        [TestCase("3+(9+(5+44/(6+2))")]
        [TestCase("5+(3/7))")]
        public void TestUnequalParentheses(string expression)
        {
            Assert.That(() => new ExpressionTree(expression), Throws.TypeOf<InvalidExpressionException>());
        }

        [Test]

        /// <summary>
        /// Test expressions with values totaling above maxvalue.
        /// </summary>
        public void TestInfinity()
        {
            string maxValue = double.MaxValue.ToString("F", CultureInfo.InvariantCulture);
            double result = new ExpressionTree($"{maxValue}+{maxValue}").Evaluate();
            Assert.True(double.IsInfinity(result));
        }

        [Test]
        /// <summary>
        /// Test expression with variable value.
        /// </summary>
        public void TestExpressionWithVariableValue()
        {
            Dictionary<string, double> variables = new Dictionary<string, double>
            { { "B1", 23.0 } };
            ExpressionTree expression = new ExpressionTree("B1+7");
            expression.SetVariables(variables);
            Assert.That(expression.Evaluate(), Is.EqualTo(30.0));
        }

        [Test]
        /// <summary>
        /// Test Addition expression with 2 variable values.
        /// </summary>
        public void TestAdditionWith2VariableValues()
        {
            Dictionary<string, double> variables = new Dictionary<string, double>
            { { "A1", 9 }, { "B2", 1 } };
            ExpressionTree expression = new ExpressionTree("B2+A1+3");
            expression.SetVariables(variables);
            Assert.That(expression.Evaluate(), Is.EqualTo(13));
        }

        [Test]
        /// <summary>
        /// Test Addition expression with 2 variable values.
        /// </summary>
        public void TestMixedWith2VariableValues()
        {
            Dictionary<string, double> variables = new Dictionary<string, double>
            { { "A1", 1 }, { "B2", 2 } };
            ExpressionTree expression = new ExpressionTree("(B2+5)*A1+3");
            expression.SetVariables(variables);
            Assert.That(expression.Evaluate(), Is.EqualTo(10));
        }

        [Test]
        /// <summary>
        /// Test Addition expression with 2 variable values.
        /// </summary>
        public void TestExpressionWith3VariableValues()
        {
            Dictionary<string, double> variables = new Dictionary<string, double>
            { { "A1", 11.5 }, { "B2", 9.5 }, { "C5", 2 } };
            ExpressionTree expression = new ExpressionTree("(A1+B2)/C5");
            expression.SetVariables(variables);
            Assert.That(expression.Evaluate(), Is.EqualTo(10.5));
        }
    }
}
#pragma warning restore SA1600 // Elements should be documented