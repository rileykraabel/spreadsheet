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
///     This class holds tests to determine if the function of the Evaluator class' Evaluate method has been implemented correctly. 
///     There are methods for each conditional statement from the Evaluate method. 
/// 
/// </summary>

using FormulaEvaluator;
using Microsoft.VisualBasic;

/// <summary>
///     This method is used to determine if the Evaluator class' Evaluate method can parse integers sufficiently.
///     The console prints a line depending if the String number has been correctly parsed to an integer type.
///     
/// </summary>
/// <param name="number"> type string, the number we want to parse to an integer. </param>
void testNumber(string number)
{
    if (Evaluator.Evaluate(number, null) == int.Parse(number))
        Console.WriteLine("the string " + number + " has correctly been parsed to an integer. Output: " + Evaluator.Evaluate(number, null));
    else
        Console.WriteLine("the string could not be parsed to an integer.");
}


/// <summary>
///     This method is used to determine if the Evaluator class' Evaluate method can add numbers sufficiently.
///     The console prints a line depending if the int numbers were added together correctly or not.
///     
/// </summary>
/// <param name="number1"> int type, the first number we want to add. </param>
/// <param name="number2"> int type, the second number we want to add to the first number. </param>
void testAddition(int number1, int number2)
{
    string expression = number1 + "+" + number2;
    if (Evaluator.Evaluate(expression, null) == number1 + number2)
        Console.WriteLine("added " + number1 + " and " + number2 + " correctly. Output: " + Evaluator.Evaluate(expression, null));
    else
        Console.WriteLine("the expression has output the wrong answer. Output: " + Evaluator.Evaluate(expression, null));
}

/// <summary> 
///     This method is used to determine if the Evaluator class' Evaluate method can subtract numbers sufficiently. 
///     The console prints a line depending if the int numbers were subtracted correctly or not. 
///     (The second number is subtracted from the first). 
///     
/// </summary>
/// <param name="number1"> int type, the first number we want to subtract from. </param>
/// <param name="number2"> int type, the second number we want to use in the subtraction (the one being subtracted from the first). </param>
void testSubtraction(int number1, int number2)
{
    string expression = number1 + "-" + number2;
    if (Evaluator.Evaluate(expression, null) == number1 - number2)
        Console.WriteLine("subtracted " + number1 + " and " + number2 + " correctly. Output: " + Evaluator.Evaluate(expression, null));
    else
        Console.WriteLine("the expression has output the wrong answer. Output: " + Evaluator.Evaluate(expression, null));
}

/// <summary> 
///     This method is used to determine if the Evaluator class' Evaluate method can multiply numbers sufficiently. 
///     The console prints a line depending if the int numbers were multiplied correctly or not.
///     
/// </summary>
/// <param name="number1"> int type, the first number we want to multiply. </param>
/// <param name="number2"> int type, the second number we want to multiply. </param>
void testMultiplication(int number1, int number2)
{
    string expression = number1 + "*" + number2;
    if (Evaluator.Evaluate(expression, null) == number1 * number2)
        Console.WriteLine("multiplied " + number1 + " and " + number2 + " correctly. Output: " + Evaluator.Evaluate(expression, null));
    else
        Console.WriteLine("the expression has output the wrong answer. Output: " + Evaluator.Evaluate(expression, null));
}

/// <summary>
///     This method is used to determine if the Evaluator class' Evaluate method can divide numbers sufficiently.
///     The console prints a line depending if the int numbers were divided correctly or not. 
///     (The first number is divided by the second).
///     
/// </summary>
/// <param name="number1"> int type, the first number. Will be divided by the second number. </param>
/// <param name="number2"> int type, the second number. Divides the first number. </param>
void testDivision(int number1, int number2)
{
    string expression = number1 + "/" + number2;
    if (Evaluator.Evaluate(expression, null) == number1 / number2)
        Console.WriteLine("divided " + number1 + " by " + number2 + " correctly. Output: " + Evaluator.Evaluate(expression, null));
    else
        Console.WriteLine("the expression has output the wrong answer. Output: " + Evaluator.Evaluate(expression, null));
}

/// <summary>
///     This method is used to determine if the Evaluator class' Evaluate method can use general expressions sufficiently. 
///     The console prints a line depending if the string expression was executed correctly. 
///     This method is useful for determining if expressions with parentheses compile. 
///     
/// </summary>
/// <param name="expression"> type string, holds the expression we want to solve. </param>
void testExpression(string expression)
{
    Console.WriteLine(expression);
    Console.WriteLine(Evaluator.Evaluate(expression, null));
}

/// <summary> 
///     This method is used to determine if the Evaluator class' Evaluate method can use variables correctly.
///     The console prints a line depending if the string expression was executed correctly, specifically using variables.
///     
/// </summary>
/// <param name="expression"> type string, holds the expression we want to solve. </param>
void testVariable(string expression)
{
    if (Evaluator.Evaluate(expression, Evaluator.variableEvaluator) != 0)
        Console.WriteLine("the expression has output the wrong answer. Output: " + Evaluator.Evaluate(expression, Evaluator.variableEvaluator));
    else
        Console.WriteLine("correct answer. output: " + Evaluator.Evaluate(expression, Evaluator.variableEvaluator));

}

/// <summary>
///     This method is used to determine if the Evaluator class' Evaluate method throws ArgumentExceptions in the correct locations.
///     The console prints a line if the argument was thrown correctly. 
///     
/// </summary>
/// <param name="expression"> type string, the expression we want to throw an error. </param>
void testInvalid(string expression)
{
    try
    {
        Evaluator.Evaluate(expression, null);
    }
    catch (ArgumentException)
    {
        Console.WriteLine("argument has been handled correctly.");
    }
}

/// <summary> 
///     This method is used to determine if the Evaluator class' Evaluate method throws ArgumentExceptions in the correct locations. 
///     This method is very similar to the 'testInvalid' method, but specifically determines if certain variables are incorrect or hold
///     no associated value.
///     
/// </summary>
/// <param name="expression"> type string, the expression we want to throw an error .</param>
void testInvalidVar(string expression)
{
    try
    {
        Evaluator.Evaluate(expression, Evaluator.variableEvaluator);
    }
    catch (ArgumentException)
    {
        Console.WriteLine("variable argument has been handled correctly.");
    }
}

// the test statements are shown below.

//testNumber("5");
//testNumber("10000");
//testNumber("   20020202 ");
//testNumber("                        0                    ");

//testAddition(5, 5);
//testAddition(100, 1000000);
//testAddition(5, 20);
//testAddition(3278593, 870258689);

//testSubtraction(5, 5);
//testSubtraction(5, 10);
//testSubtraction(10, 3);
//testSubtraction(100, 101);
//testSubtraction(29292, 01010);

//testMultiplication(5, 5);
//testMultiplication(5, 0);
//testMultiplication(100, 5);
//testMultiplication(1000, 102993);
//testMultiplication(0, 0);

//testDivision(10, 5);
//testDivision(100, 10);
//testDivision(10, 10);
//testDivision(5, 10);
//testDivision(32, 6);

//testExpression("(1+1)");
//testExpression("(((0)))");
//testExpression("(1+1)*6");
//testExpression("((1+1)+(2*2))/2");
//testExpression("(45*2)+(1000*2)");
//testExpression("(2 + 3) * 5 + 2");
//testExpression("((5000 * (5+100)) / 5)");
//testExpression("5 + 5 * 2");

//testInvalid("(-2) + 6");
//testInvalid("20++");
//testInvalid("(2 +  6) *   (-9)");
//testInvalid("(2 +  6) *   -9)");
//testInvalid("");
//testInvalid("              ");
//testInvalid("(5+ 1) / 0");
//testInvalid("5--2");
//testInvalid("5**2");
//testInvalid("10//2");
//testInvalid("4++3");
//testInvalid("(5+6))");
//testInvalid("((5+6)");
//testInvalid("1 * () + 17- 4");
//testInvalid("null");

//testVariable("1 * A1");
//testVariable("2 * A1");
//testVariable("5 + ccccC4");
//testVariable("1000 * AbCDeFgHIjk0 + 1");
//testVariable("((0 * A1) + 12345)");

//testInvalidVar("!");
//testInvalidVar("12Abc");
//testInvalidVar("A 1");
//testInvalidVar("aaaaaa1aaa");
//testInvalidVar("variable_name1");
//testInvalidVar("         CCCCC5");
//testInvalidVar("    %%%");
//testInvalidVar(" 555abc");

//testExpression("2*(3+5)");

//testExpression("2+5+");

//testExpression("2+3*(3+5)");

testExpression("2+3*5+(3+4*8)*5+2");

Console.Read();