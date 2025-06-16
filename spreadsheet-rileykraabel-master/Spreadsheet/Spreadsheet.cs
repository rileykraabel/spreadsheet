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
///     This class contains the implementation of the Spreadsheet class. This class implements methods from the
///     AbstractSpreadsheet abstract class, along with a nested 'Cell' class. This class contains a constructor and
///     three getter/setter methods for a Cell's name, cellContents, and value. These three components make up a 
///     Cell object, and therefore are essential to the Spreadsheet class' functionality as well. 
///     
///     The Spreadsheet class contains methods that work using the previous DependencyGraph and Formula assignments.
/// </summary>

using SpreadsheetUtilities;
using System.Text.RegularExpressions;
using System.Xml;

namespace SS
{
    /// <summary>
    ///     This class functions as an implementation of the methods from 'AbstractSpreadsheet'. This class
    ///     overrides the methods from that class, and uses them for functionality within a Spreadsheet. This
    ///     class incorporates function from the DependencyGraph class and the Formula class; both of which
    ///     are prior assignments from the University of Utah School of Computing CS3500 course.
    /// </summary>
    public class Spreadsheet : AbstractSpreadsheet
    {
        /// <summary>
        ///     This nested Cell class functions to allow access to a Cell item. The cell item has three
        ///     components: 1- a name, 2- cellContents, and 3- a value. 
        /// </summary>
        public class Cell
        {
            private object cellContents;
            private object value;

            /// <summary>
            ///     This is a constructor for the nested 'Cell' class. A cell has three notable features:
            ///     1. A name
            ///     2. Some collection of the Cell's contents.
            ///     3. The Cell's output value.
            /// </summary>
            /// 
            /// <param name="cellContents">
            ///     Object type, holds the Cell's contents.
            /// </param>
            public Cell(object cellContents)
            {
                this.cellContents = cellContents;
                this.value = cellContents;
            }

            public object GetCC() => this.cellContents;

            public void SetCC(object item) { cellContents = item; }

            public object GetValue() => this.value;

            public void SetValue(object item) { value = item; }

        }

        // -- Initialize Private Variables --
        private readonly DependencyGraph depGraph;
        private readonly List<string> NonEmptyCells;
        private readonly Dictionary<string, Cell> cellPairs;

        // -- Initialize Private Helper Methods --
        /// <summary>
        ///     This is the lookup delegate method used for functionality within the 'Evaluate'
        ///     method inside of the SetCellContents(formula) method.
        /// </summary>
        /// 
        /// <param name="variable_name">
        ///     string type, holds the name of the variable we want to find.
        /// </param>
        /// 
        /// <returns>
        ///     double type, the mapped value of the variable. 
        /// </returns>
        private double Lookup(string variable_name)
        {
            return GCVWrapper(variable_name);
        }

        /// <summary>
        ///     This private helper method is used to determine if a variable is a valid Spreadsheet cell name.
        /// </summary>
        /// 
        /// <remarks>
        ///     A Spreadsheet variable is valid if it is structured as "one or more letters followed by one or more numbers".
        /// </remarks>
        /// 
        /// <param name="name"> 
        ///     The name of the variable we are looking at. 
        /// </param>
        /// 
        /// <returns>
        ///     Returns a boolean; true if the variable is valid, false if not.
        /// </returns>
        private bool IsValidName(string name)
        {
            name = Normalize(name);
            String allowedPattern = @"^[a-zA-Z]+\d+$";

            if (Regex.IsMatch(name, allowedPattern))
                return IsValid(name);
            else
                return false;
        }

        /// <summary>
        ///     This private helper method is used in each of the methods to determine if a Cell's name is "" (empty string).
        ///     If true, the method throws an InvalidNameException. Otherwise, the method returns false/
        /// </summary>
        /// 
        /// <param name="name">
        ///     String type, holds the name of the Cell we are looking at. 
        /// </param>
        /// 
        /// <returns>
        ///     Returns a boolean; true if the name is "" (empty string), false if not.
        /// </returns>
        /// 
        /// <exception cref="InvalidNameException">
        ///     Thrown if the name is determined to be invalid. 
        /// </exception>
        private bool CheckInvalidName(string name)
        {
            name = Normalize(name);
            if (name == "" || !IsValidName(name))
                throw new SS.InvalidNameException();
            else
                return false;
        }

        /// <summary>
        ///     This private helper method handles the code inside of the setCellContents methods, because the same
        ///     code is used in each method. It effectively creates a new HashSet<string> item to hold the dependencies
        ///     of the 'name' Cell whose value we are replacing.
        /// </summary>
        /// 
        /// <param name="name">
        ///     String type, holds the name of the Cell whose cellContents we want to change.
        /// </param>
        /// 
        /// <param name="item">
        ///     Object type, holds the cellContents we are replacing the original contents with.
        /// </param>
        /// 
        /// <returns>
        ///     Returns an object of HashSet<string> type; calls GetCellsToRecalculate() and retrieves all of the
        ///     'name' Cell's direct and indirect dependencies.
        /// </returns>
        private IList<string> ReplaceContents(string name, object item)
        {
            if (cellPairs.ContainsKey(name))
            {
                cellPairs[name].SetCC(item);
                cellPairs[name].SetValue(item);
            }

            else if (!cellPairs.ContainsKey(name))
            {
                cellPairs.Add(name, new Cell(item));
                cellPairs[name].SetValue(item);
            }

            return new List<string>(GetCellsToRecalculate(name));
        }

        /// <summary>
        ///     This method is used by the Lookup delegate. It finds out if there is a value associated
        ///     with some 'name' item, and returns it if it exists. If not, it throws an ArgumentException.
        /// </summary>
        /// 
        /// <param name="name">
        ///     string type, the item we want to find the value of.
        /// </param>
        /// 
        /// <returns>
        ///     double type, the value of the 'name' item we are looking at.
        /// </returns>
        /// 
        /// <exception cref="ArgumentException">
        ///     Thrown if the cell does not have a valid double value equivalent.
        /// </exception>
        private double GCVWrapper(string name)
        {
            if (GetCellValue(name) is double)
                return (double)GetCellValue(name);

            else
                throw new ArgumentException();
        }

        /// <summary>
        ///     This is a constructor for the 'Spreadsheet' class. It initializes a 
        ///     constructor that has no added validity constraints, no extra normalization
        ///     rule, and the version is "default". 
        /// </summary>
        public Spreadsheet() : base(valid => true, s => s, "default")
        {
            depGraph = new DependencyGraph();
            NonEmptyCells = new List<string>();
            cellPairs = new Dictionary<string, Cell>();

            Changed = false;
        }

        /// <summary>
        ///     This is a constructor method for the 'Spreadsheet' class. It initializes a 
        ///     constructor that has added validity constraints, extra normalization
        ///     rules, and a pre-determined version. 
        /// </summary>
        /// 
        /// <param name="isValid">
        ///     Func type, takes a String and returns Boolean. This determines the extra 
        ///     validity constraints within the spreadsheet. 
        /// </param>
        /// 
        /// <param name="normalize">
        ///     Func type, takes a String and returns a String. This determines the 
        ///     normalization rule within the spreadsheet. 
        /// </param>
        /// 
        /// <param name="version">\
        ///     string type. Holds the version name of the spreadsheet. 
        /// </param>
        public Spreadsheet(Func<string, bool> isValid, Func<string, string> normalize, string version) : base(isValid, normalize, version)
        {
            depGraph = new DependencyGraph();
            NonEmptyCells = new List<string>();
            cellPairs = new Dictionary<string, Cell>();

            Changed = false;

            if (version == "")
                Version = "default";

            else
                Version = version;
        }

        /// <summary>
        ///     This is a constructor method for the 'Spreadsheet' class. It initializes a 
        ///     constructor that has a given string filepath, added validity constraints, 
        ///     extra normalization rules, and a pre-determined version.
        /// </summary>
        ///
        /// <param name="filepath">
        ///     The given filepath to read. 
        /// </param>
        /// 
        /// <param name="isValid">
        ///     Func type, takes a string and records a boolean. This determines the extra
        ///     validity constraints within the spreadsheet.
        /// </param>
        /// 
        /// <param name="normalize">
        ///     Func type, takes a string and returns a string. This determines the 
        ///     normalization rule within the spreadsheet.
        /// </param>
        /// 
        /// <param name="version">
        ///     string type. Holds the version name of the spreadsheet.
        /// </param>
        public Spreadsheet(string filepath, Func<string, bool> isValid, Func<string, string> normalize, string version) : base(isValid, normalize, version)
        {
            depGraph = new DependencyGraph();
            NonEmptyCells = new List<string>();
            cellPairs = new Dictionary<string, Cell>();
            Changed = false;

            List<string> cellNames = new List<string>();
            List<string> cellContents = new List<string>();

            try
            {
                using (XmlReader reader = XmlReader.Create(filepath))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name)
                            {
                                case "spreadsheet":
                                    if (reader["version"] != Version)
                                        throw new SpreadsheetReadWriteException("There was an error reading this version of the file.");
                                    reader.Read();
                                    break;

                                case "cell":
                                    reader.Read();
                                    break;

                                case "name":
                                    reader.Read();
                                    cellNames.Add(reader.Value);
                                    reader.Read();
                                    break;

                                case "contents":
                                    reader.Read();
                                    cellContents.Add(reader.Value);
                                    reader.Read();
                                    break;
                            }
                        }

                        else
                        {
                            if (reader.Name == "cell")
                                reader.Read();

                            if (reader.Name == "spreadsheet")
                                reader.Read();
                        }
                    }
                }
            }

            catch (FileNotFoundException)
            {
                throw new SpreadsheetReadWriteException("An error has occurred while trying to read the file.");
            }

            catch (IOException)
            {
                throw new SpreadsheetReadWriteException("An error has occurred while trying to read the file.");
            }

            catch (XmlException)
            {
                throw new SpreadsheetReadWriteException("An error has occurred while trying to read the file.");
            }

            catch (CircularException)
            {
                throw new CircularException();
            }

            catch (FormulaFormatException)
            {
                throw new FormulaFormatException("The formula is invalid, enter a new formula.");
            }

            catch (Exception)
            {
                throw new SpreadsheetReadWriteException("Other processing error.");
            }


            for (int i = 0; i < cellNames.Count; i++)
            {
                cellPairs.Add(cellNames[i], new Cell(cellNames[i]));
                SetContentsOfCell(cellNames[i], cellContents[i]);
            }
        }

        // -- Initialize Overridden API Methods --
        /// <summary>
        /// True if this spreadsheet has been modified since it was created or saved                  
        /// (whichever happened most recently); false otherwise.
        /// </summary>
        public override bool Changed { get; protected set; }

        /// <summary>
        ///     This method returns the names of all non-empty cells in the Spreadsheet. If 
        ///     all cells are empty, an empty enumerable is returned.
        /// </summary>
        /// 
        /// <returns>
        ///     Returns an object of IEnumerable<string> type. This holds the non-empty cell names.
        /// </returns>
        public override IEnumerable<string> GetNamesOfAllNonemptyCells()
        {
            foreach (string cellName in cellPairs.Keys)
            {
                if (IsValidName(cellName) && GetCellContents(cellName) != "" && !NonEmptyCells.Contains(cellName))
                    NonEmptyCells.Add(cellName);
            }

            return NonEmptyCells;
        }

        /// <summary>
        ///     This method returns the contents (NOT the value) of the named cell.
        /// </summary>
        /// 
        /// <param name="name">
        ///     String type. The name of the Spreadsheet cell we want to find the contents of.
        /// </param>
        /// 
        /// <returns>
        ///     Returns either a double, a string, or a Formula.
        /// </returns>
        /// 
        /// <exception cref="InvalidNameException">
        ///     Thrown if the parameter cell name invalid (blank, empty "").
        /// </exception>
        public override object GetCellContents(string name)
        {
            name = Normalize(name);
            CheckInvalidName(name);

            if (cellPairs.ContainsKey(name))
                return cellPairs[name].GetCC();

            return "";
        }

        /// <summary>
        ///     This method sets the contents of the named cell to the input number.
        /// </summary>
        /// 
        /// <param name="name">
        ///     String type. The name of the Spreadsheet cell.
        /// </param>
        /// 
        /// <param name="number">
        ///     Double type. The number we are setting the cell's contents to.
        /// </param>
        /// 
        /// <returns>
        ///     Returns an IList<string> type, which holds the name of the cell, along with the names
        ///     of all other cells whose value depends (directly or indirectly) on the cell.
        /// </returns>
        /// 
        /// <exception cref="InvalidNameException">
        ///     Thrown if the parameter cell name is invalid (blank, empty "").
        /// </exception>
        protected override IList<string> SetCellContents(string name, double number)
        {
            name = Normalize(name);
            CheckInvalidName(name);

            List<string> cells = new List<string>(ReplaceContents(name, number));

            foreach (string dependent in cells)
            {
                object content = GetCellContents(dependent);

                if (content is Formula)
                    SetContentsOfCell(dependent, "=" + content);
            }

            return cells;
        }

        /// <summary>
        ///     This method sets the contents of the named cell to the input text. 
        /// </summary>
        /// 
        /// <param name="name">
        ///     String type. The first parameter; the name of the Spreadsheet cell.
        /// </param>
        /// 
        /// <param name="text">
        ///     String type. The second parameter; the text we are setting the cell's contents to.
        /// </param>
        /// 
        /// <returns>
        ///     Returns an IList<string> type, which holds the name of the cell, along with the names
        ///     of all other cells whose value depends (directly or indirectly) on the cell.
        /// </returns>
        /// 
        /// <exception cref="InvalidNameException">
        ///     Thrown if the parameter cell name is null or invalid.
        /// </exception>
        protected override IList<string> SetCellContents(string name, string text)
        {
            name = Normalize(name);
            CheckInvalidName(name);

            return ReplaceContents(name, text);
        }

        /// <summary>
        ///     This method sets the contents of the named cell to the input Formula.
        /// </summary>
        /// 
        /// <param name="name">
        ///     String type. The first parameter; the name of the Spreadsheet cell.
        /// </param>
        /// 
        /// <param name="formula">
        ///     Formula type. The second parameter; the Formula we are setting the cell's contents to.
        /// </param>
        /// 
        /// <returns>
        ///     Returns an IList<string> type, which holds the name of the cell, along with the names
        ///     of all other cells whose value depends (directly or indirectly) on the cell.
        /// </returns>
        /// 
        /// <exception cref="InvalidNameException">
        ///     Thrown if the parameter cell name is null or invalid.
        /// </exception>
        /// 
        /// <exception cref="ArgumentNullException"> 
        ///     Thrown if formula parameter is null. 
        /// </exception>
        /// 
        /// <exception cref="CircularException">
        ///     Thrown if changing the contents of the named cell to the Formula would cause a circular dependency.
        ///     NOTE: No change is made to the spreadsheet if this is the case.
        /// </exception>
        protected override IList<string> SetCellContents(string name, Formula formula)
        {
            name = Normalize(name);
            CheckInvalidName(name);

            List<string> storeDependees = new List<string>(depGraph.GetDependees(name));

            try
            {
                depGraph.ReplaceDependees(name, formula.GetVariables());
                return ReplaceContents(name, formula);
            }

            catch (CircularException)
            {
                depGraph.ReplaceDependees(name, storeDependees);
                ReplaceContents(name, storeDependees);

                throw new CircularException();
            }
        }

        /// <summary>
        ///   <para>Sets the contents of the named cell to the appropriate value. </para>
        ///   <para>
        ///       First, if the content parses as a double, the contents of the named
        ///       cell becomes that double.
        ///   </para>
        ///
        ///   <para>
        ///       Otherwise, if content begins with the character '=', an attempt is made
        ///       to parse the remainder of content into a Formula.  
        ///       There are then three possible outcomes:
        ///   </para>
        ///
        ///   <list type="number">
        ///       <item>
        ///           If the remainder of content cannot be parsed into a Formula, a 
        ///           SpreadsheetUtilities.FormulaFormatException is thrown.
        ///       </item>
        /// 
        ///       <item>
        ///           If changing the contents of the named cell to be f
        ///           would cause a circular dependency, a CircularException is thrown,
        ///           and no change is made to the spreadsheet.
        ///       </item>
        ///
        ///       <item>
        ///           Otherwise, the contents of the named cell becomes f.
        ///       </item>
        ///   </list>
        ///
        ///   <para>
        ///       Finally, if the content is a string that is not a double and does not
        ///       begin with an "=" (equal sign), save the content as a string.
        ///   </para>
        /// </summary>
        ///
        /// <exception cref="InvalidNameException"> 
        ///   If the name parameter is null or invalid, throw an InvalidNameException
        /// </exception>
        /// 
        /// <exception cref="SpreadsheetUtilities.FormulaFormatException"> 
        ///   If the content is "=XYZ" where XYZ is an invalid formula, throw a FormulaFormatException.
        /// </exception>
        /// 
        /// <exception cref="CircularException"> 
        ///   If changing the contents of the named cell to be the formula would 
        ///   cause a circular dependency, throw a CircularException.  
        ///   (NOTE: No change is made to the spreadsheet.)
        /// </exception>
        /// 
        /// <param name="name"> The cell name that is being changed </param>
        /// <param name="content"> The new content of the cell </param>
        /// 
        /// <returns>
        ///       <para>
        ///           This method returns a list consisting of the passed in cell name,
        ///           followed by the names of all other cells whose value depends, directly
        ///           or indirectly, on the named cell. The order of the list MUST BE any
        ///           order such that if cells are re-evaluated in that order, their dependencies 
        ///           are satisfied by the time they are evaluated.
        ///       </para>
        ///
        ///       <para>
        ///           For example, if name is A1, B1 contains A1*2, and C1 contains B1+A1, the
        ///           list {A1, B1, C1} is returned.  If the cells are then evaluate din the order:
        ///           A1, then B1, then C1, the integrity of the Spreadsheet is maintained.
        ///       </para>
        /// </returns>
        public override IList<string> SetContentsOfCell(string name, string content)
        {
            name = Normalize(name);
            List<string> contents;
            CheckInvalidName(name);

            if (double.TryParse(content, out double result))
            {
                contents = new List<string>(SetCellContents(name, result));
            }

            else if (content.StartsWith("="))
            {
                try
                {
                    Formula formula = new Formula(content.Substring(1));

                    contents = new List<string>(SetCellContents(name, formula));

                    foreach (string item in contents)
                    {
                        Formula itemFormula = new Formula(cellPairs[item].GetCC().ToString());
                        cellPairs[item].SetValue(itemFormula.Evaluate(Lookup));
                    }
                }

                catch (CircularException)
                {
                    throw new CircularException();
                }

                catch (Exception)
                {
                    throw new FormulaFormatException("The formula cannot be parsed into a valid formula.");
                }
            }

            else
                contents = new List<string>(SetCellContents(name, content));

            Changed = true;
            return contents;
        }

        /// <summary>
        ///     This method retrieves the names of all cells whose values depend directly on the value
        ///     of the named cell, and returns them as an enumeration. Duplicates are not included.
        /// </summary>
        /// 
        /// <param name="name">
        ///     String type. The name of the cell we want to look at. 
        /// </param>
        /// 
        /// <returns>
        ///     Returns an IEnumerable<string> type, which holds the names of all cells that contain formulas
        ///     containing 'name' (i.e., direct dependents of 'name'). The 'GetDependents' method from the 
        ///     DependencyGraph class returns a HashSet<string> type, which implicitly does not allow duplicates.
        /// </returns>
        /// 
        /// <exception cref="InvalidNameException"> 
        ///     Thrown if the name is invalid.
        /// </exception>
        protected override IEnumerable<string> GetDirectDependents(string name)
        {
            name = Normalize(name);
            CheckInvalidName(name);

            return depGraph.GetDependents(name);
        }

        /// <summary>
        ///     This method returns the Value of the named cell. 
        /// </summary>
        /// 
        /// <param name="name">
        ///     string type. The cell name we want to find the value of. The normalized version is used.
        /// </param>
        /// 
        /// <returns>
        ///     object type, the value of the cell.
        /// </returns>
        /// 
        /// <exception cref="NotImplementedException">
        ///     Thrown if the name of the cell is invalid.
        /// </exception>
        public override object GetCellValue(string name)
        {
            name = Normalize(name);
            CheckInvalidName(name);

            if (cellPairs.ContainsKey(name))
                return cellPairs[name].GetValue();

            return "";
        }

        /// <summary>
        ///   Look up the version information in the given file. If there are any problems opening, reading, 
        ///   or closing the file, the method should throw a SpreadsheetReadWriteException with an explanatory message.
        /// </summary>
        /// 
        /// <exception cref="SpreadsheetReadWriteException"> 
        ///   Thrown if any problem occurs while reading the file or looking up the version information.
        /// </exception>
        /// 
        /// <param name="filename"> The name of the file (including path, if necessary)</param>
        /// <returns>Returns the version information of the spreadsheet saved in the named file.</returns>
        public override string GetSavedVersion(string filename)
        {
            string version = "";

            try
            {
                using (XmlReader reader = XmlReader.Create(filename))
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement())
                        {
                            switch (reader.Name)
                            {
                                case "spreadsheet":
                                    version = reader.GetAttribute(0);
                                    break;
                            }
                        }
                    }
                }
            }

            catch (FileNotFoundException)
            {
                throw new SpreadsheetReadWriteException("An error has occurred while trying to read the file for the version info.");
            }

            catch (Exception)
            {
                throw new SpreadsheetReadWriteException("An error has occurred.");
            }


            return version;
        }

        /// <summary>
        ///     Writes the contents of this spreadsheet to the named file using an XML format.
        ///     Example of the XML format can be found in the AbstractSpreadsheet class. 
        /// </summary>
        /// 
        /// <param name="filename">
        ///     The name of the file we want to convert to an XML file
        /// </param>
        public override void Save(string filename)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "  ";

            try
            {
                using (XmlWriter writer = XmlWriter.Create(filename, settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("spreadsheet");

                    writer.WriteAttributeString("version", Version);

                    foreach (string cellName in GetNamesOfAllNonemptyCells())
                    {
                        writer.WriteStartElement("cell");
                        writer.WriteElementString("name", cellName);

                        object contents = GetCellContents(cellName);

                        if (contents is double) writer.WriteElementString("contents", contents.ToString());
                        if (contents is string) writer.WriteElementString("contents", contents.ToString());
                        if (contents is Formula) writer.WriteElementString("contents", "=" + contents.ToString());

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }

            catch (Exception)
            {
                throw new SpreadsheetReadWriteException("There was a problem writing the file.");
            }

            Changed = false;
        }
    }
}