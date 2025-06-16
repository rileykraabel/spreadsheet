```
Author: Riley Kraabel
Partner: None
Course: CS 3500, University of Utah, School of Computing
Github ID: rileykraabel
Repo: https://github.com/uofu-cs3500-spring23/spreadsheet-rileykraabel
Date: 23-Jan-2023
Project: Formula Evaluator
Copyright: CS 3500 and Riley Kraabel - This work may not be copied for use in Academic Coursework.
```

# Comments to Evaluators:

Local helper methods were added to improve functionality and reduce redundant code. Local helper methods were chosen since the Stack
variables were not made instance variables, and were used inside of the local methods. This allowed access to their elements within both
the Evaluate and the respective helper methods. 

The algorithm was designed using if statements so that if none applied, the ArgumentException could easily be thrown afterwards. 

Regular Expressions were used to determine if the variable had correct syntax and to determine if there was whitespace. This was to eliminate
any errors that may slip through a loop or another implementation.

# Assignment Specific Topics

N/A.

# Consulted Peers
	1. Lingbai
	2. Kyle
	3. Zoe
	4. Lincoln
	5. Alex
	6. Matt

# References:
	1. https://www.mikesdotnetting.com/article/46/c-regular-expressions-cheat-sheet