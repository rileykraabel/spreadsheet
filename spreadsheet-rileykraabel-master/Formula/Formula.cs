/// <summary>
/// Author: Riley Kraabel
/// Partner: -none-
/// Date (of creation): 26-Jan-2023
/// Course: CS 3500, University of Utah, School of Computing
/// Copyright: CS 3500 and Riley Kraabel - This work may not be copied for use in Academic Coursework.
/// 
/// I, Riley Kraabel, certify that I wrote this code from scratch and did not copy it in part or whole from another source. 
/// All references used in the completion of the assignments are cited in my README file.
/// 
/// File Contents:
/// 
///     This class contains a more generalized version of the previous 'FormulaEvaluator' class. 
/// 
/// </summary>

// Skeleton written by Joe Zachary for CS 3500, September 2013
// Read the entire skeleton carefully and completely before you
// do anything else!

// Version 1.1 (9/22/13 11:45 a.m.)

// Change log:
//  (Version 1.1) Repaired mistake in GetTokens
//  (Version 1.1) Changed specification of second constructor to
//                clarify description of how validation works

// (Daniel Kopta) 
// Version 1.2 (9/10/17) 

// Change log:
//  (Version 1.2) Changed the definition of equality with regards
//                to numeric tokens

using System.Text;
using System.Text.RegularExpressions;

namespace SpreadsheetUtilities
{
    public delegate double Lookup(String variable_name);
    public delegate string normalizeDelegate(String variable_name);
    public delegate bool isValidDelegate(String variable_name);

    /// <summary>
    /// Represents formulas written in standard infix notation using standard precedence
    /// rules.  The allowed symbols are non-negative numbers written using double-precision
    /// floating-point syntax (without unary preceeding '-' or '+'); 
    /// variables that consist of a letter or underscore followed by 
    /// zero or more letters, underscores, or digits; parentheses; and the four operator
    /// symbols +, -, *, and /.  
    /// 
    /// Spaces are significant only insofar that they delimit tokens.  For example, "xy" is
    /// a single variable, "x y" consists of two variables "x" and y; "x23" is a single variable;
    /// and "x 23" consists of a variable "x" and a number "23".
    /// 
    /// Associated with every formula are two delegates:  a normalizer and a validator.The
    /// normalizer is used to convert variables into a canonical form, and the validator is used
    /// to add extra restrictions on the validity of a variable (beyond the standard requirement
    /// that it consist of a letter or underscore followed by zero or more letters, underscores,
    /// or digits.)  Their use is described in detail in the constructor and method comments.
    /// </summary>
    public class Formula
    {
        // -- Initialize Delegate Statements --
        private Lookup lookup;
        private normalizeDelegate normalize;
        private isValidDelegate isValid;

        //  -- Initialize Instance Variables --
        private readonly IEnumerable<String> formulaItems;
        private readonly List<String> enumeratedItems = new List<String>();

        private Stack<double> valuesStack = new Stack<double>();
        private Stack<string> operatorsStack = new Stack<string>();

        private double vsItem1;
        private double vsItem2;
        private string operatorItem;

        private bool isIdentity;

        // -- Private Helper Methods --

        /// <summary>
        ///     This private helper method allows us to easily check if a variable matches the variable restrictions.
        /// </summary>
        /// <param name="item"> String type, the item we want to check. </param>
        /// <returns> bool type, true if the item is a valid variable, false if not. </returns>
        private bool IsVariable(String item)
        {
            String allowedPattern = "^[a-zA-Z_]([a-zA-Z_]|\\d)*$";

            if (Regex.IsMatch(item, allowedPattern) && item != "null")
                return true;
            else
                return false;
        }

        /// <summary>
        ///     This private helper method allows us to easily check if a token is an operator (+, -, *, /).
        /// </summary>
        /// <param name="item"> String type, the item we want to check. </param>
        /// <returns> bool type, true if the item is an operator, false if not. </returns>
        private bool IsOperator(String item)
        {
            if (item == "+" || item == "-" || item == "*" || item == "/")
                return true;
            else
                return false;
        }

        /// <summary>
        ///     This private helper method allows us to easily check if a token is a left parentheses '('.
        /// </summary>
        /// <param name="item"> String type, the item we want to check. </param>
        /// <returns> bool type; true if the item is a '(', false if not. </returns>
        private bool IsLeftParentheses(String item)
        {
            if (item == "(")
                return true;
            else
                return false;
        }

        /// <summary>
        ///     This private helper method allows us to easily check if a token is a right parentheses ')'. 
        /// </summary>
        /// <param name="item"> String type, the item we want to check. </param>
        /// <returns> bool type; true if the item is a ')', false if not. </returns>
        private bool IsRightParentheses(String item)
        {
            if (item == ")")
                return true;
            else
                return false;
        }

        /// <summary>
        ///     This private helper method allows us to easily check if a token is a valid number. 
        /// </summary>
        /// <param name="item"> String type, the item we want to check. </param>
        /// <returns> bool type; true if the item is a valid number, false if not. </returns>
        private bool IsNumber(String item)
        {
            return double.TryParse(item, out double value);
        }

        /// <summary>
        ///     This private helper method handles the Parsing Rules from the Assignment3 Specifications.
        ///     See Source: https://utah.instructure.com/courses/834041/assignments/12129848?module_item_id=20269469
        /// </summary>
        /// <param name="tokens"> IEnumerable<String> type, holds the list of tokens from the formula. </param>
        /// <exception cref="FormulaFormatException"> Is returned if there is an invalid item. </exception>
        private void ParseRules(IEnumerable<String> tokens)
        {
            String prevToken = null;

            int rParenCount = 0;
            int lParenCount = 0;

            // One Token Rule: There must be at least one item.
            if (formulaItems.Count() > 0)
            {
                String firstItem = tokens.First();
                String lastItem = tokens.Last();

                // Starting Token Rule: The first token cannot be an operator or a ')'.
                if (IsOperator(firstItem) || IsRightParentheses(firstItem))
                    throw new FormulaFormatException("Formula syntax is invalid. Reminder: the only valid " +
                        "tokens for the first token of the formula are numbers, variables, or '(' items. " +
                        "Possible Fix: Enter a new formula that includes only the above items as the first token.");

                // Ending Token Rule: The final token cannot be an operator or a '('. 
                if (IsOperator(lastItem) || IsLeftParentheses(lastItem))
                    throw new FormulaFormatException("Formula syntax is invalid. Reminder: the only valid " +
                        "tokens for the last token of the formula are numbers, variables or ')' items. " +
                        "Possible Fix: Enter a new formula that includes only the above items as the last token.");
            }

            else if (formulaItems.Count() == 0)
                throw new FormulaFormatException("Formula syntax is invalid. Reminder: There must be at least " +
                    "one item in the formula. Possible Fix: Enter a new formula that includes the valid tokens.");


            // Now that we know there are enough tokens, iterate through them to determine if they are valid.
            foreach (String token in tokens)
            {
                // Check if the token is one of the parentheses; Count++ accordingly. 
                if (IsLeftParentheses(token))
                    lParenCount++;

                if (IsRightParentheses(token))
                    rParenCount++;

                // Keep track of the previous token; if it is an operator or '(', the next token must be a number, 
                // a variable, or an opening parentheses.
                if (prevToken != null && (IsOperator(prevToken) || IsLeftParentheses(prevToken)))
                {
                    // Parentheses/Operator Following Rule: Must be a number, variable, or '('.
                    if (!IsNumber(token) && !IsVariable(token) && !IsLeftParentheses(token))
                        throw new FormulaFormatException("Formula syntax is invalid. Reminder: Any token that " +
                            "immediately follows an opening parentheses or an operator must be a number, a variable, " +
                            "or an '('. Possible Fix: Enter a new formula and ensure that the tokens are in valid spots.");
                }

                // Keep track of the previous token; if it is a number, a variable, or a ')', the next token must be 
                // an operator or a closing parentheses.
                if (prevToken != null && (IsNumber(prevToken) || IsVariable(prevToken) || IsRightParentheses(prevToken)))
                {
                    // Extra Following Rule: Must be an operator or ')'. 
                    if (!IsOperator(token) && !IsRightParentheses(token))
                        throw new FormulaFormatException("Formula syntax is invalid. Reminder: Any token that " +
                            "immediately follows a number, variable, or a ')' must be an operator or a ')'. " +
                            "Possible Fix: Enter a new formula and ensure that the tokens are in valid spots.");
                }

                // Right Parentheses Rule: There cannot be more rParentheses than lParentheses
                if (rParenCount > lParenCount)
                    throw new FormulaFormatException("Formula syntax is invalid. Reminder: There can not be more " +
                        "closing ')' parentheses than opening '(' parentheses at any point in the formula. " +
                        "Possible Fix: Enter a new formula, and ensure that for each '(' parentheses, there is " +
                        "a closing ')' parentheses to match it.");

                // If the token is not an operator or '(' ')'; check if it is a number or a variable. 
                else if (!IsOperator(token) && !IsLeftParentheses(token) && !IsRightParentheses(token))
                {
                    // If it is not a number or a variable, throw 'Specific Token Rule: did not pass as a valid token'.
                    if (!IsNumber(token) && !IsVariable(token))
                        throw new FormulaFormatException("Formula syntax is invalid. Reminder: The only valid " +
                            "tokens are operators (+, -, *, /), parentheses, variables, and decimal real numbers. " +
                            "Possible Fix: Enter a new formula that includes only the above items.");
                }

                prevToken = token;
            }

            // Balanced Parentheses Rule: The total # of rParentheses must equal the total # of lParentheses
            if (lParenCount != rParenCount)
                throw new FormulaFormatException("Formula syntax is invalid. Reminder: The total number of " +
                    "parentheses must equal the total number of closing parentheses. Possible Fix: Enter a new formula " +
                    "and ensure that for each opening '(' parentheses, there is a ')' to match it. ");
        }

        // -- API Methods to Complete --

        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically invalid,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer is the identity function, and the associated validator
        /// maps every string to true.  
        /// </summary>
        public Formula(String formula) : this(formula, s => s, s => true)
        {
            isIdentity = true;
            normalize = s => s;
            isValid = s => true;

            formulaItems = GetTokens(formula);
            ParseRules(formulaItems);
        }


        /// <summary>
        /// Creates a Formula from a string that consists of an infix expression written as
        /// described in the class comment.  If the expression is syntactically incorrect,
        /// throws a FormulaFormatException with an explanatory Message.
        /// 
        /// The associated normalizer and validator are the second and third parameters,
        /// respectively.  
        /// 
        /// If the formula contains a variable v such that normalize(v) is not a legal variable, 
        /// throws a FormulaFormatException with an explanatory message. 
        /// 
        /// If the formula contains a variable v such that isValid(normalize(v)) is false,
        /// throws a FormulaFormatException with an explanatory message.
        /// 
        /// Suppose that N is a method that converts all the letters in a string to upper case, and
        /// that V is a method that returns true only if a string consists of one letter followed
        /// by one digit.  Then:
        /// 
        /// new Formula("x2+y3", N, V) should succeed
        /// new Formula("x+y3", N, V) should throw an exception, since V(N("x")) is false
        /// new Formula("2x+y3", N, V) should throw an exception, since "2x+y3" is syntactically incorrect.
        /// </summary>
        public Formula(String formula, Func<string, string> normalizer, Func<string, bool> validator)
        {
            isIdentity = false;

            if (normalizer != null)
                normalize = new normalizeDelegate(normalizer);
            if (validator != null)
                isValid = new isValidDelegate(validator);

            formulaItems = GetTokens(formula);

            foreach (string item in formulaItems)
            {
                if (IsVariable(item))
                {
                    if (!IsVariable(normalize(item)))
                        throw new FormulaFormatException("The variable entered is not a legal variable. " +
                            "Possible Fix: Enter the equation again, but use a variable that will be legal after " +
                            "normalization.");

                    else if (!isValid(normalize(item)))
                        throw new FormulaFormatException("The variable entered is not a legal variable. " +
                            "Possible Fix: Enter the equation again, but use a variable that will be valid after " +
                            "normalization.");
                }
            }

            ParseRules(formulaItems);
        }

        /// <summary>
        ///     This private helper method handles the case where the item being looked at is a number. The Stacks are popped
        ///     and the top items are multiplied by the item that was put as the parameter during the method call.
        /// </summary>
        /// <param name= number"> type double, holds the value of the token. </param>
        private void NumberCase(double number)
        {
            // ensure that there is some item in the OpStack, and the top item is either "*" or "/"
            if (operatorsStack.Count > 0 && (operatorsStack.Peek() == "*" || operatorsStack.Peek() == "/"))
            {
                vsItem1 = valuesStack.Pop();
                operatorItem = operatorsStack.Pop();

                if (operatorItem == "*")
                    valuesStack.Push(vsItem1 * number);

                // Catch DivideByZero Exception
                if (operatorItem == "/" && number == 0)
                    throw new ArgumentException();

                else if (operatorItem == "/" && number != 0)
                    valuesStack.Push(vsItem1 / number);
            }

            else
                valuesStack.Push(number);
        }

        /// <summary>
        ///     This is a local method to handle the condition where the token is a "+" or "-" operand. The method was made as a local
        ///     method to allow items from the Stacks to be accessible, since they are not instance variables.
        ///     
        /// </summary>
        /// <param name="token"> type string, holds some operand value. </param>
        private void PlusMinusOperandCase(string token)
        {
            PopStacks();

            if (operatorItem == "+") valuesStack.Push(vsItem1 + vsItem2);
            if (operatorItem == "-") valuesStack.Push(vsItem1 - vsItem2);
        }

        /// <summary>
        ///     This is a private helper method to eliminate redundant code for popping elements off of the stacks. 
        /// </summary>
        private void PopStacks()
        {
            vsItem2 = valuesStack.Pop();
            vsItem1 = valuesStack.Pop();
            operatorItem = operatorsStack.Pop();
        }

        /// <summary>
        /// Evaluates this Formula, using the lookup delegate to determine the values of
        /// variables.  When a variable symbol v needs to be determined, it should be looked up
        /// via lookup(normalize(v)). (Here, normalize is the normalizer that was passed to
        /// the constructor.)
        /// 
        /// For example, if L("x") is 2, L("X") is 4, and N is a method that converts all the letters
        /// in a string to upper case:
        /// 
        /// new Formula("x+7", N, s => true).Evaluate(L) is 11
        /// new Formula("x+7").Evaluate(L) is 9
        /// 
        /// Given a variable symbol as its parameter, lookup returns the variable's value
        /// (if it has one) or throws an ArgumentException (otherwise).
        /// 
        /// If no undefined variables or divisions by zero are encountered when evaluating
        /// this Formula, the value is returned.  Otherwise, a FormulaError is returned.
        /// The Reason property of the FormulaError should have a meaningful explanation.
        ///
        /// This method should never throw an exception.
        /// </summary>
        public object Evaluate(Func<string, double> lookup)
        {
            foreach (String item in formulaItems)
            {
                // 1. Try to parse the token to a double
                if (double.TryParse(item, out double value))
                    try
                    {
                        NumberCase(value);
                    }

                    catch (ArgumentException)
                    {
                        return new FormulaError("Invalid argument; there has been a DivisionByZero exception. Possible " +
                            "Fix: Ensure that anything you want divided does not have a denominator of zero.");
                    }

                // 2. Check if the token is a variable
                else if (IsVariable(item))
                {
                    try
                    {
                        try
                        {
                            double varValue = lookup(normalize(item));
                            NumberCase(varValue);
                        }

                        catch (ArgumentException)
                        {
                            return new FormulaError("Invalid argument; there has been a DivisionByZero exception. Possible " +
                                "Fix: Ensure that anything you want divided does not have a denominator of zero.");
                        }
                    }

                    catch (ArgumentException)
                    {
                        return new FormulaError("Invalid argument; there is no value associated with the input variable. Potential " +
                            "Fix: Ensure that the variables being used in your formula have associated values.");
                    }
                }

                // 3. Check if the token is a "+" or "-"
                else if (item == "+" || item == "-")
                {
                    if ((operatorsStack.Count > 0 && valuesStack.Count >= 2) && (operatorsStack.Peek() == "+" || operatorsStack.Peek() == "-"))
                    {
                        PlusMinusOperandCase(item);
                        operatorsStack.Push(item);
                    }

                    else
                        operatorsStack.Push(item);
                }

                // 4. Check if the token is an "*" or "/"
                else if (item == "*" || item == "/")
                    operatorsStack.Push(item);

                // 5. Check if the token is a "("
                else if (item == "(")
                    operatorsStack.Push(item);

                // 6. Check if the token is a ")"
                else if (item == ")")
                {
                    // Step 1: Check the top of OpStack for "+" or "-"
                    if ((operatorsStack.Count > 0 && valuesStack.Count >= 2) && (operatorsStack.Peek() == "+" || operatorsStack.Peek() == "-"))
                        PlusMinusOperandCase(item);

                    // Step 2: Check the top of OpStack for "("
                    if (operatorsStack.Count > 0 && operatorsStack.Peek() == "(")
                        operatorsStack.Pop();

                    // Step 3: Check the top of OpStack for "*" or "/"
                    if ((operatorsStack.Count > 0 && valuesStack.Count >= 2) && (operatorsStack.Peek() == "*" || operatorsStack.Peek() == "/"))
                    {
                        PopStacks();

                        if (operatorItem == "*")
                            valuesStack.Push(vsItem1 * vsItem2);

                        // Catch DivideByZero error
                        else if (operatorItem == "/" && vsItem2 == 0)
                            return new FormulaError("Invalid argument; there has been a DivideByZero exception. Possible " +
                                "Fix: Ensure that anything you want divided does not have a denominator of zero.");

                        else
                            valuesStack.Push(vsItem1 / vsItem2);
                    }
                }
            }

            // Last of the Tokens ... Case 1: Operator Stack is empty.
            if (operatorsStack.Count == 0)
            {
                if (valuesStack.Count == 1)
                {
                    vsItem1 = valuesStack.Pop();
                    return vsItem1;
                }
            }

            // Case 2: Operator Stack is not empty.
            else
            {
                if (operatorsStack.Count == 1 && valuesStack.Count == 2)
                {
                    PopStacks();

                    if (operatorItem == "+") return vsItem1 + vsItem2;
                    if (operatorItem == "-") return vsItem1 - vsItem2;
                    if (operatorItem == "*") return vsItem1 * vsItem2;
                    if (operatorItem == "/") return vsItem2 / vsItem1;
                }

                else if (operatorsStack.Count > 1 && valuesStack.Count > 2)
                {
                    PopStacks();

                    if (operatorItem == "+") valuesStack.Push(vsItem1 + vsItem2);
                    if (operatorItem == "-") valuesStack.Push(vsItem1 - vsItem2);
                    if (operatorItem == "*") valuesStack.Push(vsItem1 * vsItem2);
                    if (operatorItem == "/") valuesStack.Push(vsItem2 / vsItem1);
                }
            }

            return new FormulaError("Invalid argument: there has been an invalid argument. Possible Fix: Ensure " +
                "that the formula you have entered contains only valid arguments.");
        }

        /// <summary>
        /// Enumerates the normalized versions of all of the variables that occur in this 
        /// formula.  No normalization may appear more than once in the enumeration, even
        /// if it appears more than once in this Formula.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x+y*z", N, s => true).GetVariables() should enumerate "X", "Y", and "Z"
        /// new Formula("x+X*z", N, s => true).GetVariables() should enumerate "X" and "Z".
        /// new Formula("x+X*z").GetVariables() should enumerate "x", "X", and "z".
        /// </summary>
        public IEnumerable<String> GetVariables()
        {
            foreach (String item in formulaItems)
            {
                if (IsVariable(item) && !enumeratedItems.Contains(normalize(item)))
                    enumeratedItems.Add(normalize(item));
            }

            return enumeratedItems;
        }

        /// <summary>
        /// Returns a string containing no spaces which, if passed to the Formula
        /// constructor, will produce a Formula f such that this.Equals(f).  All of the
        /// variables in the string should be normalized.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        /// 
        /// new Formula("x + y", N, s => true).ToString() should return "X+Y"
        /// new Formula("x + Y").ToString() should return "x+Y"
        /// </summary>
        public override string ToString()
        {
            StringBuilder finalFormula = new StringBuilder();

            foreach (string item in formulaItems)
            {
                if (IsVariable(item))
                {
                    string normalizedItem = normalize(item);
                    finalFormula.Append(normalizedItem);
                }

                else
                    finalFormula.Append(item);
            }

            return finalFormula.ToString();
        }

        /// <summary>
        ///  <change> make object nullable </change>
        ///
        /// If obj is null or obj is not a Formula, returns false.  Otherwise, reports
        /// whether or not this Formula and obj are equal.
        /// 
        /// Two Formulae are considered equal if they consist of the same tokens in the
        /// same order.  To determine token equality, all tokens are compared as strings 
        /// except for numeric tokens and variable tokens.
        /// Numeric tokens are considered equal if they are equal after being "normalized" 
        /// by C#'s standard conversion from string to double, then back to string. This 
        /// eliminates any inconsistencies due to limited floating point precision.
        /// Variable tokens are considered equal if their normalized forms are equal, as 
        /// defined by the provided normalizer.
        /// 
        /// For example, if N is a method that converts all the letters in a string to upper case:
        ///  
        /// new Formula("x1+y2", N, s => true).Equals(new Formula("X1  +  Y2")) is true
        /// new Formula("x1+y2").Equals(new Formula("X1+Y2")) is false
        /// new Formula("x1+y2").Equals(new Formula("y2+x1")) is false
        /// new Formula("2.0 + x7").Equals(new Formula("2.000 + x7")) is true
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != typeof(Formula))
                return false;

            var formula = (Formula)obj;

            if (this.formulaItems.Count() != formula.formulaItems.Count())
                return false;

            for (int i = 0; i < this.formulaItems.Count(); i++)
            {
                string token1 = formulaItems.ElementAt(i).ToString();
                string token2 = formula.formulaItems.ElementAt(i).ToString();

                // If they are numbers, we have to convert from string --> double --> string. 
                if (double.TryParse(token1, out double double1) && double.TryParse(token2, out double double2))
                {
                    string normalized1 = double1.ToString();
                    string normalized2 = double2.ToString();

                    if (normalized1 != normalized2)
                        return false;
                }

                // If they are variables, we have to normalize them.
                else if (IsVariable(token1) && IsVariable(token2) && !isIdentity)
                {
                    string normalized1 = normalize(token1);
                    string normalized2 = normalize(token2);

                    if (normalized1 != normalized2)
                        return false;
                }

                // If it is something else, check if it is equal (they will be strings, we can use == or !=).
                else if (token1 != token2)
                    return false;
            }

            return true;
        }

        /// <summary>
        ///   <change> We are now using Non-Nullable objects.  Thus neither f1 nor f2 can be null!</change>
        /// Reports whether f1 == f2, using the notion of equality from the Equals method.
        /// 
        /// </summary>
        public static bool operator ==(Formula f1, Formula f2)
        {
            return f1.Equals(f2);
        }

        /// <summary>
        ///   <change> We are now using Non-Nullable objects.  Thus neither f1 nor f2 can be null!</change>
        ///   <change> Note: != should almost always be not ==, if you get my meaning </change>
        ///   Reports whether f1 != f2, using the notion of equality from the Equals method.
        /// </summary>
        public static bool operator !=(Formula f1, Formula f2)
        {
            return !f1.Equals(f2);
        }

        /// <summary>
        /// Returns a hash code for this Formula.  If f1.Equals(f2), then it must be the
        /// case that f1.GetHashCode() == f2.GetHashCode().  Ideally, the probability that two
        /// randomly-generated unequal Formulae have the same hash code should be extremely small.
        /// </summary>
        public override int GetHashCode()
        {
            int returnedHashCode = 1;

            for (int i = 0; i < this.formulaItems.Count(); i++)
            {
                if (IsNumber(formulaItems.ElementAt(i)))
                {
                    double.TryParse(formulaItems.ElementAt(i), out double value);
                    string normalized = value.ToString();
                    returnedHashCode *= normalized.GetHashCode();
                }

                else if (!IsNumber(formulaItems.ElementAt(i)))
                    returnedHashCode += formulaItems.ElementAt(i).GetHashCode();
            }

            return returnedHashCode;
        }

        /// <summary>
        /// Given an expression, enumerates the tokens that compose it.  Tokens are left paren;
        /// right paren; one of the four operator symbols; a string consisting of a letter or underscore
        /// followed by zero or more letters, digits, or underscores; a double literal;and anything that doesn't
        /// match one of those patterns.  There are no empty tokens, and no token contains white space.
        /// </summary>
        private static IEnumerable<string> GetTokens(String formula)
        {
            // Patterns for individual tokens
            String lpPattern = @"\(";
            String rpPattern = @"\)";
            String opPattern = @"[\+\-*/]";
            String varPattern = @"[a-zA-Z_](?: [a-zA-Z_]|\d)*";
            String doublePattern = @"(?: \d+\.\d* | \d*\.\d+ | \d+ ) (?: [eE][\+-]?\d+)?";
            String spacePattern = @"\s+";
            // Overall pattern
            String pattern = String.Format("({0}) | ({1}) | ({2}) | ({3}) | ({4}) | ({5})",
                                      lpPattern, rpPattern, opPattern, varPattern, doublePattern, spacePattern);
            // Enumerate matching tokens that don't consist solely of white space.
            foreach (String s in Regex.Split(formula, pattern, RegexOptions.IgnorePatternWhitespace))
            {
                if (!Regex.IsMatch(s, @"^\s*$", RegexOptions.Singleline))
                {
                    yield return s;
                }
            }
        }
    }

    /// <summary>
    /// Used to report syntactic errors in the argument to the Formula constructor.
    /// </summary>
    public class FormulaFormatException : Exception
    {
        /// <summary>
        /// Constructs a FormulaFormatException containing the explanatory message.
        /// </summary>
        public FormulaFormatException(String message) : base(message)
        {
        }
    }

    /// <summary>
    /// Used as a possible return value of the Formula.Evaluate method.
    /// </summary>
    public struct FormulaError
    {
        /// <summary>
        /// Constructs a FormulaError containing the explanatory reason.
        /// </summary>
        /// <param name="reason"></param>
        public FormulaError(String reason) : this()
        {
            Reason = reason;
        }
        /// <summary>
        ///  The reason why this FormulaError was created.
        /// </summary>
        public string Reason { get; private set; }
    }
}

// <change>
//   If you are using Extension methods to deal with common stack operations (e.g., checking for
//   an empty stack before peeking) you will find that the Non-Nullable checking is"biting" you.
//
//   To fix this, you have to use a little special syntax like the following:
//
//       public static bool OnTop<T>(this Stack<T> stack, T element1, T element2) where T : notnull
//
//   Notice that the "where T : notnull" tells the compiler that the Stack can contain any object
//   as long as it doesn't allow nulls!
// </change>
