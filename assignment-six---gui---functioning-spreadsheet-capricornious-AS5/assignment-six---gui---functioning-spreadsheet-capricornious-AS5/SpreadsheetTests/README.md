```
Author:     Riley Kraabel
Partner:    None
Course:     CS 3500, University of Utah, School of Computing
GitHub ID:  rileykraabel
Repo:       https://github.com/uofu-cs3500-spring23/spreadsheet-rileykraabel
Date:       10-Feb-23 (11:59pm)
Project:    Abstract Spreadsheet
Copyright:  CS 3500 and Riley Kraabel - This work may not be copied for use in Academic Coursework.
```

# Comments to Evaluators:
    (updated 10-Feb-2023 for assignment4)
    I created tests that achieved about 95% code coverage, and had some issues getting it any higher simply because
    of the Formula(null) case that my code did not execute. It was difficult to test this case, simply because when I
    would insert null as the formula, the Formula class determines it to be an invalid variable, and the test ends.

    Other than that test, I think my test cases cover a solid portion of the assignment and have good functionality.

    (updated 12-Feb-2023 for assignment5)
    I created tests that achieved about 95& code coverage, and had some issues getting it higher because of the errors
    that my Constructor throws. I was not entirely sure what errors could be thrown by XML reading/writing, so I included
    tests to cover XMLExceptions and IOExceptions, because the webpages I read said these can be thrown sometimes, so I
    thought it was better to be safe than sorry. I was not sure how to implement throwing these exceptions, so it did
    not get covered by my tests.

    I used testing methods from the Assignment4 Grading tests, but I deleted them after running so they are not pushed
    to my Github. They were re-worked to include functionality from the new Assignment 5 API, but it is not reflected in
    my code. I just thought it would be good to let the staff know that I did use these for error-checking, in
    case there are similarities.

# Assignment Specific Topics

Code stands on its own.

# Consulted Peers:
    (first implementation - assignment4)
    1. Zoe
    2. Toby
    3. Class Discord server

    (second implementation - assignment5)
    1. Zoe
    2. Toby
    3. Alex
    4. Class Discord server
    5. class Piazza

# References:
    (first implementation - assignment4)
    N/A.

    (second implementation - assignment5)
    1. https://utah.instructure.com/courses/834041/assignments/11982612 