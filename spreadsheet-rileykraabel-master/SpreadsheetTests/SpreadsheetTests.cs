/// <summary>
/// Author: Riley Kraabel
/// Partner: -none-
/// Date (of creation): 7-Feb-2023
/// Date (updated):     20-Feb-2023 
/// Course: CS 3500, University of Utah, School of Computing
/// Copyright: CS 3500 and Riley Kraabel - This work may not be copied for use in Academic Coursework.
/// 
/// I, Riley Kraabel, certify that I wrote this code from scratch and did not copy it in part or whole from another source. 
/// All references used in the completion of the assignments are cited in my README file.
/// 
/// File Contents:
///     This class contains tests for the Spreadsheet class, which inherits methods from the AbstractSpreadsheet class.
///     These tests aim to cover close to 100% of the code from the Spreadsheet class.
///     
///     (Updated 12-Feb-2023) This class now contains tests for the updated Spreadsheet class, since the assignment
///     specifications have changed. There are more methods in the Spreadsheet class, resulting in more tests in this
///     test Class as well. The tests aim to cover close to 100% of the code from the Spreadsheet class. 
/// 
///     (Updated 20-Feb-2023) This class contains tests that work for the Spreadsheet class. Most of the tests from the
///     Assignment4 implementation were deleted and rewritten because they did not function as intended. I think a lot
///     of my tests incorporated method calls from multiple methods, just because they were used together to see results. 
///     (ie, saving file --> used to find/construct file)
///     
/// </summary>
/// 
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;
using SS;

namespace SpreadsheetTests
{
    /// <summary>
    ///     This class is a test class for the 'Spreadsheet' class. The methods are inherited from the 
    ///     'AbstractSpreadsheet' class, and these tests are to ensure full capabilities of the 'Spreadsheet' class.
    /// </summary>
    [TestClass()]
    public class SpreadsheetTests
    {
        /// <summary>
        ///     This test makes sure that if we try to get an item that is not in the spreadsheet, the spreadsheet returns
        ///     it's value as "".
        /// </summary>
        [TestMethod()]
        public void TestEmptySpreadsheetContents()
        {
            AbstractSpreadsheet sheet1 = new SS.Spreadsheet();
            Assert.AreEqual("", sheet1.GetCellContents("A1"));
        }

        /// <summary>
        ///     This test makes sure that when valid items are added to the spreadsheet, they are considered non-empty cells.
        /// </summary>
        [TestMethod()]
        public void TestGetNonEmpties()
        {
            AbstractSpreadsheet sheet1 = new SS.Spreadsheet();
            sheet1.SetContentsOfCell("A1", "20");
            Assert.IsTrue(sheet1.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());

            sheet1.SetContentsOfCell("Riley2", "A1 + 27");
            Assert.IsTrue(sheet1.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());
        }

        /// <summary>
        ///     This test makes sure that if the variable does not have the correct syntax, it is not considered
        ///     a valid name, and therefore not added to the list of non-empty cells.
        /// </summary>
        //[TestMethod()]
        //public void TestNonEmptiesInvalid()
        //{
        //    AbstractSpreadsheet sheet1 = new SS.Spreadsheet();
        //    sheet1.SetContentsOfCell("banana", "27");

        //    Assert.IsFalse(sheet1.GetNamesOfAllNonemptyCells().GetEnumerator().MoveNext());
        //}

        /// <summary>
        ///     This test ensures that the 'setCellContents(formula)' method is correctly parsing the Formula type
        ///     input into a Formula, and that the items taken from the GetCellContents method are the ones input.
        /// </summary>
        [TestMethod()]
        public void TestGetCellContentsEasy()
        {
            AbstractSpreadsheet sheet1 = new SS.Spreadsheet();
            sheet1.SetContentsOfCell("A1", "=27 + 13");
            sheet1.SetContentsOfCell("B1", "20");

            Assert.AreEqual(new Formula("27 + 13"), (Formula)sheet1.GetCellContents("A1"));
            Assert.AreEqual(20, (double)sheet1.GetCellContents("B1"));
        }

        /// <summary>
        ///     This test makes sure that if we try to getCellContents of an invalid cell name (""), the InvalidNameException
        ///     is thrown.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(InvalidNameException))]
        public void TestGetCellContentsThrow()
        {
            AbstractSpreadsheet sheet1 = new SS.Spreadsheet();
            sheet1.SetContentsOfCell("A1", "15");

            sheet1.GetCellContents("");
        }

        /// <summary>
        ///     This test ensures that if we try to get the contents of a cellName we have not set CellContents to, "" is returned.
        /// </summary>
        [TestMethod()]
        public void TestGetCellContentsNotInDict()
        {
            AbstractSpreadsheet sheet1 = new SS.Spreadsheet();
            sheet1.SetContentsOfCell("banANA10", "cheese");

            Assert.AreEqual("", (string)sheet1.GetCellContents("A1"));
        }

        /// <summary>
        ///     This is a slightly longer test to ensure that if we set a cell's contents and it already exists, the current contents
        ///     are simply overridden. This test is mainly to ensure that double functionality works.
        /// </summary>
        [TestMethod()]
        public void OverrideCellContentsDouble()
        {
            AbstractSpreadsheet sheet1 = new SS.Spreadsheet();
            sheet1.SetContentsOfCell("cheese0", "13");
            Assert.AreEqual(13, (double)sheet1.GetCellContents("cheese0"));

            sheet1.SetContentsOfCell("cheese0", "potato");
            Assert.AreEqual("potato", (string)sheet1.GetCellContents("cheese0"));

            sheet1.SetContentsOfCell("cheese0", "10");
            Assert.AreEqual(10, (double)sheet1.GetCellContents("cheese0"));

            sheet1.SetContentsOfCell("cheese0", "13");
            Assert.AreEqual(13, (double)sheet1.GetCellContents("cheese0"));
        }

        /// <summary>
        ///     This is a slightly longer test to ensure that if we set a cell's contents and it already exists, the current contents
        ///     are simply overridden. This case is specifically to ensure that string functionality works.
        /// </summary>
        [TestMethod()]
        public void OverrideCellContentsString()
        {
            AbstractSpreadsheet sheet1 = new SS.Spreadsheet();
            sheet1.SetContentsOfCell("tomato27", "soup");
            Assert.AreEqual("soup", (string)sheet1.GetCellContents("tomato27"));

            sheet1.SetContentsOfCell("tomato27", "soupy");
            Assert.AreEqual("soupy", (string)sheet1.GetCellContents("tomato27"));

            sheet1.SetContentsOfCell("tomato27", "sopp");
            Assert.AreEqual("sopp", (string)sheet1.GetCellContents("tomato27"));

            sheet1.SetContentsOfCell("tomato27", "soup");
            Assert.AreEqual("soup", (string)sheet1.GetCellContents("tomato27"));
        }

        /// <summary>
        ///     This is a slightly longer test to ensure that if we set a cell's contents and it already exists, the current contents
        ///     are simply overridden. This case is specifically to ensure that the interaction between all types of contents works.
        /// </summary>
        //[TestMethod()]
        //public void OverrideCellContentsAll()
        //{
        //    AbstractSpreadsheet sheet1 = new SS.Spreadsheet();
        //    sheet1.SetContentsOfCell("hamSandwich12", "=10+cheese");
        //    Assert.AreEqual(new Formula("10 + cheese"), (Formula)sheet1.GetCellContents("hamSandwich12"));

        //    sheet1.SetContentsOfCell("hamSandwich12", "29");
        //    Assert.AreEqual(29, (double)sheet1.GetCellContents("hamSandwich12"));

        //    sheet1.SetContentsOfCell("hamSandwich12", "tomato");
        //    Assert.AreEqual("tomato", (string)sheet1.GetCellContents("hamSandwich12"));

        //    sheet1.SetContentsOfCell("hamSandwich12", "=10+cheese1");
        //    Assert.AreEqual(new Formula("10 + cheese1"), (Formula)sheet1.GetCellContents("hamSandwich12"));
        //}

        /// <summary>
        ///     This test makes sure that if we enter an invalid formula, the FormulaFormatException from the 'Formula' class is thrown.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestSetContentsThrowInvalidFormula()
        {
            AbstractSpreadsheet sheet1 = new SS.Spreadsheet();
            sheet1.SetContentsOfCell("salami27", "=27 & 7");
        }

        /// <summary>
        ///     This test makes sure that when establishing a circular dependency using the SetContents method,
        ///     a circular dependency is thrown.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(CircularException))]
        public void TestSetContentsThrowCircular()
        {
            AbstractSpreadsheet sheet1 = new SS.Spreadsheet();
            sheet1.SetContentsOfCell("grapes10", "=banana7");
            sheet1.SetContentsOfCell("banana7", "=grapes10");
        }

        /// <summary>
        ///     This test makes sure that if the item we are setting the cell's contents to is a double, the value returned
        ///     is equal to the cell's contents; there is nothing to evaluate.
        /// </summary>
        [TestMethod()]
        public void TestGetCellValueDouble()
        {
            AbstractSpreadsheet sheet1 = new SS.Spreadsheet();
            sheet1.SetContentsOfCell("A1", "27");

            Assert.AreEqual(27, (double)sheet1.GetCellValue("A1"));
        }

        /// <summary>
        ///     This test makes sure that if we try to get the value of a cell that is not in the spreadsheet, the return value is
        ///     an empty string (empty cell).
        /// </summary>
        [TestMethod()]
        public void TestGetValueNotInSheet()
        {
            AbstractSpreadsheet sheet1 = new SS.Spreadsheet();
            sheet1.SetContentsOfCell("a29", "30");

            Assert.AreEqual("", sheet1.GetCellValue("A1"));
        }

        /// <summary>
        ///     This test makes sure that if the item we are setting the cell's contents to is a string, the value returned
        ///     is equal to the cell's contents; there is nothing to evaluate.
        /// </summary>
        [TestMethod()]
        public void TestGetCellValueString()
        {
            AbstractSpreadsheet sheet1 = new SS.Spreadsheet();
            sheet1.SetContentsOfCell("A1", "banana");

            Assert.AreEqual("banana", (string)sheet1.GetCellValue("A1"));
        }

        /// <summary>
        ///     This test was made to see how the version is used.
        /// </summary>
        [TestMethod()]
        public void TestGetVersion()
        {
            AbstractSpreadsheet sheet2 = new SS.Spreadsheet(s => true, s => s.ToLower(), "default");

            Assert.AreEqual("default", sheet2.Version);
        }

        /// <summary>
        ///     This test was made to ensure that if we set a cell to be the formula of two already
        ///     set cells, the newly-made cell will have the value of the other two cells.
        /// </summary>
        [TestMethod()]
        public void TestGetCellValue()
        {
            AbstractSpreadsheet sheet1 = new SS.Spreadsheet();
            sheet1.SetContentsOfCell("A1", "20");
            sheet1.SetContentsOfCell("A2", "10");

            sheet1.SetContentsOfCell("A3", "=A1 + A2");

            Assert.AreEqual(30, (double)sheet1.GetCellValue("A3"));
        }

        /// <summary>
        ///     This is a more advanced version of the above test, and checks to make sure that if a cell's
        ///     contents are reset, the cell can be recalculated and reassigned a new value.
        /// </summary>
        [TestMethod()]
        public void TestGetCellValueAfterReset()
        {
            AbstractSpreadsheet sheet1 = new SS.Spreadsheet();
            sheet1.SetContentsOfCell("A20", "25");
            sheet1.SetContentsOfCell("A1", "5");

            sheet1.SetContentsOfCell("A2", "=A20 / A1");

            Assert.AreEqual(5, (double)sheet1.GetCellValue("A2"));

            sheet1.SetContentsOfCell("A3", "20");
            sheet1.SetContentsOfCell("A2", "=A3/A1");

            Assert.AreEqual(4, (double)sheet1.GetCellValue("A2"));
        }

        /// <summary>
        ///     This test was made to ensure that if a cell whose value is used in a formula changes, the formula's value
        ///     changes as well.
        /// </summary>
        [TestMethod()]
        public void TestGetCellValueReset2()
        {
            AbstractSpreadsheet sheet1 = new SS.Spreadsheet();
            sheet1.SetContentsOfCell("A1", "25");
            sheet1.SetContentsOfCell("A2", "=A1 *10");

            sheet1.SetContentsOfCell("A1", "2");

            Assert.AreEqual(20, (double)sheet1.GetCellValue("A2"));
        }

        /// <summary>
        ///    This test is made to ensure that if a file is made using the 'save' method, the 
        ///    corresponding XML inside of it is formatted correctly.
        /// </summary>
        [TestMethod()]
        public void TestSimpleSave()
        {
            AbstractSpreadsheet sheet1 = new SS.Spreadsheet();
            sheet1.SetContentsOfCell("A1", "20");
            sheet1.SetContentsOfCell("A2", "20");

            sheet1.SetContentsOfCell("A3", "banana");
            sheet1.SetContentsOfCell("A4", "pineapple pizza");

            sheet1.SetContentsOfCell("A5", "=A1+A2");
            sheet1.SetContentsOfCell("A6", "=20+10");

            sheet1.Save("simple.xml");

            Assert.AreEqual("default", sheet1.GetSavedVersion("simple.xml"));
        }

        /// <summary>
        ///     This test is made to ensure that if a file is constructed with a constructor version
        ///     it is reflected in the associated XML.
        /// </summary>
        [TestMethod()]
        public void TestSimpleSaveWithVersion()
        {
            AbstractSpreadsheet sheet1 = new SS.Spreadsheet(s => true, s => s.ToLower(), "27");
            sheet1.SetContentsOfCell("A1", "potato soup");

            sheet1.Save("versionSave.xml");

            Assert.AreEqual("27", sheet1.GetSavedVersion("versionSave.xml"));
        }

        /// <summary>
        ///     This test makes sure that the SpreadsheetReadWriteException is thrown when the file cannot be found.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestInvalidFile()
        {
            AbstractSpreadsheet sheet3 = new SS.Spreadsheet("toast.xml", v => true, n => n.ToLower(), "toast1");
        }

        /// <summary>
        ///     This test makes sure that if a file is read using the wrong version, it cannot be opened
        ///     and a SpreadsheetReadWriteException is thrown.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(SpreadsheetReadWriteException))]
        public void TestInvalidVersionSheet()
        {
            // this should have a default version
            AbstractSpreadsheet sheet1 = new SS.Spreadsheet();

            sheet1.SetContentsOfCell("A1", "potato");
            sheet1.SetContentsOfCell("a2", "=3*10");
            sheet1.SetContentsOfCell("A3", "10");

            sheet1.Save("simpFile.txt");

            // this wants to use the above spreadsheet file, but an invalid version
            AbstractSpreadsheet sheet3 = new SS.Spreadsheet("simpFile.txt", v => true, n => n.ToUpper(), "version2");
        }

        /// <summary>
        ///     This test ensures that circular exceptions are still thrown; takes inspiration from the AS4 grading tests.
        /// </summary>
        [TestMethod()]
        [TestCategory("16")]
        [ExpectedException(typeof(CircularException))]
        public void TestComplexCircular()
        {
            AbstractSpreadsheet s = new SS.Spreadsheet();
            s.SetContentsOfCell("A20", "=A2+A3");
            s.SetContentsOfCell("A3", "=A4+A5");
            s.SetContentsOfCell("A5", "=A6+A7");
            s.SetContentsOfCell("A7", "=A20+A20");
        }

        /// <summary>
        ///     This test was refactored from the as4 grading tests, but with implementation for as5.
        /// </summary>
        [TestMethod()]
        [TestCategory("35")]
        public void TestStress2()
        {
            AbstractSpreadsheet s = new SS.Spreadsheet();
            ISet<String> cells = new HashSet<string>();
            for (int i = 1; i < 200; i++)
            {
                cells.Add("A" + i);
                Assert.IsTrue(cells.SetEquals(s.SetContentsOfCell("A" + i, "=A" + (i + 1))));
            }
        }
    }
}