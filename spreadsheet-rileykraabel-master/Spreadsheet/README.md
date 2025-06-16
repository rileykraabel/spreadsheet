```
Author:     Riley Kraabel
Partner:    None
Course:     CS 3500, University of Utah, School of Computing
GitHub ID:  rileykraabel
Repo:       https://github.com/uofu-cs3500-spring23/spreadsheet-rileykraabel
Date:       20-Feb-23 (11:59am)
Project:    Abstract Spreadsheet
Copyright:  CS 3500 and Riley Kraabel - This work may not be copied for use in Academic Coursework.
```

# Comments to Evaluators:
    -- (updated 10-Feb-2023 for Assignment 4) --
    This class contains overridden methods from an AbstractSpreadsheet abstract class. There are multiple methods that
    incorporate functionality from the DependencyGraph and Formula classes. I chose to create a nested Cell class as well
    to allocate a holder for cellContents and values. This Cell class came in handy because it was used as a Dictionary value, 
    allowing the Dictionary's value to either be cellContents or (for later implementation purposes) a value. 

    As for the rest of the methods in the class, they all came from the AbstractSpreadsheet class. Most of these methods have
    the same code (specifically the SetCellContents methods), because they have the same functionality, just different parameter
    types. I used private helper methods and created a constructor so that each instance of the Spreadsheet is capable of setting
    dependencies and containers for cell attributes.

    Functionality from the DependencyGraph class is used in the setCellContents(formula) method. I wanted to take the
    variables from the formula and assign them in a dependencyGraph object because variables imply some sort of dependency. 

    -- (updated 12-Feb-2023 for Assignment 5)-- 
    This class contains more overridden methods from the AbstractSpreadsheet abstract class. This implementation includes methods
    and capabilities for evaluating formulas (specifically during content-setting). This means that as a cell's contents are set, its
    value is set at the same time. This reflects the behavior of a well-developed spreadsheet, like Google Sheets or Excel. This 
    implementation includes capabilities for reading/writing XML files, as well. A spreadsheet now has a version and a filename, which
    is helpful for general implementation of a spreadsheet.

# Assignment Specific Topics

Code stands on its own.

# Consulted Peers:
    (first implementation - Assignment4)
    1. Zoe
    2. Toby
    3. Lincoln
    4. Alex

    (second implementation - Assignment5)
    1. Zoe
    2. Lincoln
    3. Class discord server

# References:
    (first implementation - Assignment4)
    1. https://www.w3schools.com/cs/cs_interface.php
    2. https://www.geeksforgeeks.org/nested-classes-in-c-sharp/
    3. https://codeeasy.io/lesson/properties
    4. https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/internal

    (second implementation - Assignment5)
    1. https://www.mikesdotnetting.com/article/46/c-regular-expressions-cheat-sheet
    2. https://learn.microsoft.com/en-us/visualstudio/version-control/git-manage-repository?view=vs-2022
    3. https://learn.microsoft.com/en-us/dotnet/api/system.string.substring?view=net-7.0
    4. https://www.techiedelight.com/conversion-between-list-and-hashset-csharp/
    5. https://utah.instructure.com/courses/834041/external_tools/90790
    6. https://learn.microsoft.com/en-us/dotnet/api/system.io.streamreader?view=net-7.0
    7. https://github.com/dotnet/docs/blob/main/docs/standard/linq/catch-parsing-errors.md
