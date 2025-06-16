```
Author: Riley Kraabel
Partner: None
Start Date: 12-Jan-23
Course: CS 3500, University of Utah, School of Computing
GitHub ID: rileykraabel
Repo: https://github.com/uofu-cs3500-spring23/spreadsheet-rileykraabel
Date: 20-Feb-23 11:59am
Solution: Spreadsheet
Copyright: CS 3500 and Riley Kraabel - This work may not be copied for use in Academic Coursework.
```

# Overview of the Spreadsheet functionality

The Spreadsheet program is currently capable of taking in an arithmetic expression using standard infix notation. This program
takes into account "PEMDAS", or the usual precedence rules for arithmetic. The input expression can consist of only "+", "-", "*", "/",
left and right parentheses ("(", ")"), non-negative integers, whitespace (which will be ignored), and variables in the pattern of
one or more letters (can be uppercase or lowercase) followed by one or more numbers (ex: A1, AA1...). The Spreadsheet program
can consistently evaluate and return these expressions.

The Spreadsheet program has been updated (as of 27-Jan-2023) to include the DependencyGraph class and methods testing its functionality.
This class works on the ability of the program to account for dependencies in a formula. An example for this would be if A1 = A2 + A3, 
A2 and A3 must be calculated first in order to find the value of A1. This is called a "dependency", and the DependencyGraph handles sorting
dependees and dependents in a mirrored data-structure. The data structure implemented was a Dictionary(string, HashSet<string>). This
was to ensure that all operations (beside replacing values) are executed at a O(1) runtime.

The Spreadsheet program has been updated (as of 3-Feb-2023) to include a refactored Formula program. This program is similar to the first
assignment 'FormulaEvaluator' program, but has been updated to work with doubles, more delegates (normalize and validate), and more
specified variable names. The 'Formula' program has more functionality than the 'FormulaEvaluator' program. This class allows variables
with underscores and/or no digits, which is different than the 'FormulaEvaluator'. The program has overridden methods for checking equality
as well. 

The Spreadsheet program has been updated (as of 9-Feb-2023) to include a re-vamped Spreadsheet class. This program is an overridden
version of the AbstractSpreadsheet class, which contains methods for setting/getting cell contents and names. This Spreadsheet class
contains functionality from the previous assignments, such as the DependencyGraph and Formula classes. It is important to note
that the methods used come mainly from the Spreadsheet class, but there are a few methods within the AbstractSpreadsheet class that
are implemented, making it an abstract class and not an interface. 

The Spreadsheet program has been updated (as of 20-Feb-2023) to include an even more re-vamped Spreadsheet class. This program uses the
implementation from the previous update (9-Feb-2023), and incorporates more methods from a new AbstractSpreadsheet class. The new
Spreadsheet program has the ability to solve formulas and set them as a Cell's 'value'. This new class includes the functionality from
the DependencyGraph and Formula classes. More methods have been added to the updated version of the AbstractSpreadsheet abstract class. 
These methods include XML file-reading, versioning, and new constructors. 

# Examples of Good Software Practice (GSP)

-- Assignment 4 Examples of GSP --
1. Well-named, Short methods that do a specific job and reduce redundant code. 
	For this assignment, I chose to create a helper method that would execute the code inside of the three setCellContents methods.
	This was my chosen software practice because I usually produce redundant code, and noticed early on that these methods all have
	essentially the same functionality, so they could be reduced down to one simple method call. By doing this for these three
	methods, I was also able to convert the GetCellsToRecalculate method's return to be that of a HashSet<string>, making it usable
	by the SetCellContents methods. Since I converted the return type inside of this method, it also reduced code that would have
	been required to switch the method's return statement into an acceptable type. The GCR method originally returned an
	IEnumerable<string>, which my methods were not happy about.

2. Testing Early and Predicting Outcomes
	For this assignment, I wanted to reduce my time expenditures. I have noticed that throughout my coding experience, I tend
	to fight the compiler and waste countless hours because I do not understand what the API is telling me to do. For this
	week, I chose to lay out my tests early on. I implemented them as I worked through the API, so that I didn't waste extra
	time accidentally creating a test with an incorrect outcome. While I was writing the tests, it really did prove useful
	to be able to outline the methods with just their parameters and return types. It took me a little while to understand
	how the different Classes were incorporated into the tests and the methods, but really seeing what the API wanted
	before coding with no aim was very helpful this week.

3. Good Comments and Variable Names
	In this week's assignment, I made a goal to comment less 'descriptions' and instead, more effectively. When I work through
	an API, I like to write comments to outline what kind of functionality the code should have. While in lecture this week, 
	Prof. Jim mentioned that too much code can be bad, so I aimed to do less of this. Good developers know what code should produce
	what functionality (i.e, a for loop runs x times and through some set), so there is virtually no reason to add extra lines
	when the code and the comment are read in the same way. I used to do this so that my code could be read like English, but
	as we become more advanced programmers, we do not need to do this. Well-designed code should be easy to read, and I think
	that I did well with this this week. I chose to have meaningful variable names and descriptive XML comments, so that in-line
	comments could be avoided. It's crazy how many lines of code you can save when you stop writing redundant comments!


-- Assignment 5 Examples of GSP --
1. Drawing Pictures before Coding
	In this week's assignment, I really wanted to focus on fully understanding the class and its contents. While reading the implementation,
	I made sure to draw the pictures and really feel confident about the differences between A4 and A5. I think this helped understand
	the reasoning behind the implementation, and allowed me to understand without wasting time implementing something wrong. I realized that
	I had the wrong overall idea of how to implement A4, so it ended up giving me a lot of trouble to fix it because I had to re-learn 
	everything I thought I had understood. Drawing pictures beforehand helped to reinforce the similarities and usage of the other classes we made.

2. DRY (code and comments)
	For assignment 5, I really made sure to use methods that had already been made. I think that this went hand in hand with the above software practice,
	drawing pictures before implementing the actual document. By drawing the pictures, I realized the overlap between classes, and could easily see
	how methods were reused and recycled. This allowed me to not repeat myself by making extra helper methods. I think I did a good job using
	DRY comments as well, as I really focused on making sure that the code did not have redundant comments and methods.

3. Testing Strategies
	For this assignment, I would have wanted to use regression testing, but my assignment 4 tests were so poorly written and did not do a good job that
	this became impossible. By using different testing strategies (specifically implementing the assignment 4 grading tests), I forced myself to understand
	the tests and the outcomes of that class before making A5. ** I did not keep any tests from the A4 grading tests in my class ** I started out writing tests
	that I knew should throw exceptions, because it allowed me to structure my methods accordingly. I knew when exceptions were to be thrown, which helped me
	understand what capabilities the code was intended to have. 

# Time Expenditures
1. Assignment One-		Predicted Hours:	15		Actual Hours: 16		Notes: Git Troubles, set-up, etc...
2. Assignment Two-		Predicted Hours:	16		Actual Hours: 15		Notes: trouble visualizing dependent vs. dependee for a while
3. Assignment Three-	Predicted Hours:	12		Actual Hours: 18		Notes: Took me a while to understand the class, I deleted and restarted a few times. 
4. Assignment Four-		Predicted Hours:	12		Actual Hours: 12		Notes: Finally wrote tests first!
5. Assignment Five-		Predicted Hours:	14		Actual Hours: 16		Notes: Took a while to fix A4.