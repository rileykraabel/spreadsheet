```
Author:     Riley Kraabel
Partner:    None
Course:     CS 3500, University of Utah, School of Computing
GitHub ID:  rileykraabel
Repo:       https://github.com/uofu-cs3500-spring23/spreadsheet-rileykraabel
Date:       27-Jan-2023 (11:59am)
Project:    Dependency Graph
Copyright:  CS 3500 and Riley Kraabel - This work may not be copied for use in Academic Coursework.
```

# Comments to Evaluators:

My implementation of the DependencyGraph class was done using a Dictionary that holds types (string, hashSet<string>). This was to
ensure that any adding, removing, or searching values was done at O(1) time. This data structure allows easy and efficient access
to elements because they are stored in "buckets" corresponding to keys. The only method that was executed using O(N) time was the
replace method, because it had to re-add 'N' items from a list of newValues. 

# Assignment Specific Topics

Any additional feedback or write up required by the assignment.
Leave a space for a new paragraph.

# Consulted Peers:
    1. Zoe
    2. Lincoln
    3. Chris

# References:
    1. https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/indexers/
    2. https://utah.instructure.com/courses/834041/assignments/11982609?module_item_id=19989988