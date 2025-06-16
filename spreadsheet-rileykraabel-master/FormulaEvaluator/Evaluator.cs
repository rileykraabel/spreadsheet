/// <summary>
/// Author: Riley Kraabel
/// Partner: -none-
/// Date (of creation): 12-Jan-2023
/// Course: CS 3500, University of Utah, School of Computing
/// Copyright: CS 3500 and Riley Kraabel - This work may not be copied for use in Academic Coursework.
/// 
/// I, Riley Kraabel, certify that I wrote this code from scratch and did not copy it in part or whole from another source. 
/// All references used in the completion of the assignments are cited in my README file.
/// 
/// File Contents:
/// 
///     This class is able to solve arithmetic equations using the Evaluate method. The class contains a 'Lookup' method, which
///     checks some data structure for an associated value under the variable name. The class also contains an 'Evaluate' method,
///     which does the actual mathematic evaluation. If the equation input is unable to be assessed, the 'Evaluate' method
///     returns an ArgumentException.
/// 
/// </summary>

using System.Collections;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace FormulaEvaluator
{
    /// <summary>
    ///     This delegate is used within the Evaluate method below. It allows us to pass another method into the Evaluate
    ///     method's parameters. 
    /// 
    /// </summary>
    /// <param name="variable_name"> the name of the variable we are passing into the Evaluate function. </param>
    /// <returns> type int; comes from the method call within the Evaluate function. </returns>
    public delegate int Lookup(String variable_name);

    public static class Evaluator
    {

        /// <summary>
        ///     This method determines if some variable has a value associated with it. If there is no associated value, the method
        ///     throws an ArgumentException. 
        ///     
        /// </summary>
        /// <param name="variable_name"> type string, the variable we are searching for. </param>
        /// <returns> type int; the associated value with the parameter 'variable_name'. </returns>
        /// <exception cref="ArgumentException"> is thrown if there is no value associated with the parameter variable. </exception>
        public static int variableEvaluator(String variable_name)
        {
            if (variable_name == "A1")
                return 100;

            else
                throw new ArgumentException("This variable does not have an associated value.");
        }


        /// <summary>
        ///     This function takes in a String representing some equation and Lookup representing a call to the Lookup delegate.
        ///     The Lookup delegate parameter allows us to pass in the 'variableEvaluator' variable. The 'Evaluate' method
        ///     checks multiple conditions (integer, variable, operand, etc.), and determines what items will be added/removed from
        ///     the stack accordingly.
        /// 
        ///     Valid arguments include: +, -, *, /, (, ), non-negative numbers, and variables beginning with one
        ///     or more letters and ending with one or more digits (ex: A1, AA1, ABDJFKdjdhd0292).
        ///     
        /// Code From:
        ///     1. https://www.mikesdotnetting.com/article/46/c-regular-expressions-cheat-sheet
        ///     
        /// </summary>
        /// <param name="expression"> type String, holds the given expression to be evaluated. </param>
        /// <param name="variableEvaluator"> the delegate parameter that allows us to call another method inside of the function. </param>
        /// <returns> Type int, the value after evaluating from the 'expression' variable. </returns>
        /// <exception cref="ArgumentException"> thrown if an argument is not valid. </exception>
        public static int Evaluate(String expression, Lookup variableEvaluator)
        {
            Stack<int> valuesStack = new Stack<int>();
            Stack<string> operatorsStack = new Stack<string>();

            // holds the top two items from the ValuesStack and the top item from the OperatorsStack
            int vsItem1;
            int vsItem2;
            string operatorItem;

            // code cited above.
            // https://www.mikesdotnetting.com/article/46/c-regular-expressions-cheat-sheet
            String allowedPattern = @"[A-Za-z]+\d+";
            String spacePattern = @"\s+";


            /// <summary>
            ///     This is a local method to handle the condition where the token is an Integer. The method was made as a local method 
            ///     to allow items from the Stacks to be accessible, since they are not instance variables. This method is used in the
            ///     condition of a variable or a parse-able integer within the parameter expression from 'Evaluate'. 
            /// 
            /// </summary>
            /// <param name= number"> type int, holds the value of the token. </param>
            void IntegerCase(int number)
            {
                // ensure that there is some item in the OpStack, and the top item is either "*" or "/"
                if (operatorsStack.Count > 0 && (operatorsStack.Peek() == "*" || operatorsStack.Peek() == "/"))
                {
                    vsItem1 = valuesStack.Pop();
                    operatorItem = operatorsStack.Pop();

                    if (operatorItem == "*")
                        valuesStack.Push(vsItem1 * number);

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
            ///// <param name="token"> type string, holds some operand value. </param>
            void PlusMinusOperandCase(string token)
            {
                vsItem1 = valuesStack.Pop();
                vsItem2 = valuesStack.Pop();
                operatorItem = operatorsStack.Pop();

                if (operatorItem == "+") valuesStack.Push(vsItem1 + vsItem2);
                if (operatorItem == "-") valuesStack.Push(vsItem2 - vsItem1);
            }


            string[] substrings = Regex.Split(expression, "(\\()|(\\))|(-)|(\\+)|(\\*)|(/)");

            foreach (String substring in substrings)
            {
                String trimmedSub = substring.Trim();

                // Check if the token is whitespace, if so, skip it
                if (trimmedSub == "" || Regex.IsMatch(trimmedSub, spacePattern))
                    continue;

                // 1. Try to parse the substring to an integer
                else if (int.TryParse(trimmedSub, out int value) == true)
                    IntegerCase(value);

                // 2. Check if the token is a variable
                else if (Regex.IsMatch(trimmedSub, allowedPattern))
                {
                    int varValue = variableEvaluator(trimmedSub);
                    IntegerCase(varValue);
                }

                // 3. Check if the substring is a "+" or "-"
                else if (trimmedSub == "+" || trimmedSub == "-")
                {
                    if ((operatorsStack.Count > 0 && valuesStack.Count >= 2) && (operatorsStack.Peek() == "+" || operatorsStack.Peek() == "-"))
                    {
                        PlusMinusOperandCase(trimmedSub);
                        operatorsStack.Push(trimmedSub);
                    }

                    else
                        operatorsStack.Push(trimmedSub);
                }

                // 4. Check if the substring is an "*" or "/"
                else if (trimmedSub == "*" || trimmedSub == "/")
                    operatorsStack.Push(trimmedSub);

                // 5. Check if the substring is a "("
                else if (trimmedSub == "(")
                    operatorsStack.Push(trimmedSub);

                // 6. Check if the substring is a ")"
                else if (trimmedSub == ")")
                {
                    // Step 1: Check the top of OpStack for "+" or "-"
                    if ((operatorsStack.Count > 0 && valuesStack.Count >= 2) && (operatorsStack.Peek() == "+" || operatorsStack.Peek() == "-"))
                        PlusMinusOperandCase(trimmedSub);

                    // Step 2: Check the top of OpStack for "("
                    if (operatorsStack.Count > 0 && operatorsStack.Peek() == "(")
                        operatorsStack.Pop();

                    else
                        throw new ArgumentException("Invalid argument.");

                    // Step 3: Check the top of OpStack for "*" or "/"
                    if ((operatorsStack.Count > 0 && valuesStack.Count >= 2) && (operatorsStack.Peek() == "*" || operatorsStack.Peek() == "/"))
                    {
                        vsItem1 = valuesStack.Pop();
                        vsItem2 = valuesStack.Pop();
                        operatorItem = operatorsStack.Pop();

                        if (operatorItem == "*")
                            valuesStack.Push(vsItem1 * vsItem2);

                        // we need a case to catch is vsItem1 is zero, because if it is, DivideByZeroException will occur.
                        else if (operatorItem == "/" && vsItem1 != 0)
                            valuesStack.Push(vsItem2 / vsItem1);

                        else
                            throw new ArgumentException("Invalid argument.");
                    }



                }

                else
                    throw new ArgumentException("Invalid argument.");
            }

            // Last of the Tokens ... Case 1: Operator Stack is empty.
            if (operatorsStack.Count == 0)
            {
                if (valuesStack.Count == 1)
                {
                    vsItem1 = valuesStack.Pop();
                    return vsItem1;
                }

                else
                    throw new ArgumentException("There are values leftover on the value stack.");
            }

            // Case 2: Operator Stack is not empty.
            else
            {
                if (operatorsStack.Count > 1 && valuesStack.Count > 2)
                {
                    vsItem2 = valuesStack.Pop();
                    vsItem1 = valuesStack.Pop();
                    operatorItem = operatorsStack.Pop();

                    if (operatorItem == "+") valuesStack.Push(vsItem1 + vsItem2);
                    if (operatorItem == "-") valuesStack.Push(vsItem1 - vsItem2);
                    if (operatorItem == "*") valuesStack.Push(vsItem1 * vsItem2);
                    if (operatorItem == "/") valuesStack.Push(vsItem2 / vsItem1);
                }

                if (operatorsStack.Count == 1 && valuesStack.Count == 2)
                {
                    vsItem2 = valuesStack.Pop();
                    vsItem1 = valuesStack.Pop();

                    if (operatorsStack.Peek() == "+") return vsItem1 + vsItem2;
                    if (operatorsStack.Peek() == "-") return vsItem1 - vsItem2;
                    if (operatorsStack.Peek() == "*") return vsItem1 * vsItem2;
                    if (operatorsStack.Peek() == "/") return vsItem2 / vsItem1;
                }

                else
                    throw new ArgumentException("Invalid argument.");
            }

            throw new ArgumentException("Invalid argument.");
        }

    }

}
