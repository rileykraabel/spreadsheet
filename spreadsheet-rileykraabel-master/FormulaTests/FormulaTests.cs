using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpreadsheetUtilities;

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
///     This class contains tests for the 'Formula' class from Assignment 3. These tests achieve approx. 92% code 
///     coverage for the code written inside of the 'Formula' class. Some code from the given Assignment 1 tests.
/// 
/// </summary>

namespace FormulaTests
{

    /// <summary>
    ///     This is a test class for the 'Formula' class' functionality. This is intended to contain all Formula Unit Tests. 
    /// </summary>
    [TestClass]
    public class FormulaTests
    {
        private Formula f1;
        private Formula f2;

        // The majority of next tests call the constructor and ensure that the parsing rules are set in place. 
        // -- Throw Exception Tests Using the Default Constructor (Formula(string)) --

        /// <summary>
        ///     Calls the default constructor, should trigger parse rule 1: invalid formula.
        ///     - exclamation point (!) is invalid.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidTokenFormulaDefaultConstructor()
        {
            f1 = new("!5");
        }

        /// <summary>
        ///     Calls the default constructor, should trigger parse rule 1: invalid formula. (test 2).
        ///     - modulus (%) is invalid.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]

        public void TestInvalidTokenDefault2()
        {
            f1 = new("(1+1)*5%2");
        }

        /// <summary>
        ///     Calls the default constructor, should trigger parse rule 1: invalid formula. (test 3).
        ///     - exponent (^) is invalid.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidTokenDefault3()
        {
            f1 = new("2^x");
        }

        /// <summary>
        ///     Calls the default constructor, should trigger parse rule 2: needs > 1 token.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestEmptyFormulaDefaultConstructor()
        {
            f1 = new(" ");
        }

        /// <summary>
        ///     Calls the default constructor, should trigger parse rule 3: the number of ')' 
        ///     should never be greater than the number of '('. (extra after the 5).
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExcessRightParenthesesDefault()
        {
            f1 = new("(1+1) * 5)");
        }

        /// <summary>
        ///     Calls the default constructor, should trigger parse rule 3: the number of ')'
        ///     should never be greater than the number of '('. (extra on the end).
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExcessRightParenthesesDefault2()
        {
            f1 = new("(((1))))");
        }

        /// <summary>
        ///     Calls the default constructor, should trigger parse rule 4: the number of '('
        ///     should be equal to the total number of ')'. 
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExcessLeftParenthesesDefault()
        {
            f1 = new("(1*10-(8+2)+((7))");
        }

        /// <summary>
        ///     Calls the default constructor, should trigger parse rule 4: the number of '('
        ///     should be equal to the total number of ')'. 
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExcessLeftParenthesesDefault2()
        {
            f1 = new("((1) * 5");
        }

        /// <summary>
        ///     Calls the default constructor, should trigger parse rule 5: Invalid starting token.
        ///     - '+' is not a number, variable, or '('. 
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidStartTokenDefault()
        {
            f1 = new("+5");
        }

        /// <summary>
        ///     Calls the default constructor, should trigger parse rule 5: Invalid starting token.
        ///     - ')' is not a number, variable, or '('. 
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidStartTokenDefault2()
        {
            f1 = new(") * 17");
        }

        /// <summary>
        ///     Calls the default constructor, should trigger parse rule 6: Invalid end token. 
        ///     - '(' is not a number, variable, or ')'. 
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidEndTokenDefault()
        {
            f1 = new("7 * 6(");
        }

        /// <summary>
        ///     Calls the default constructor, should trigger parse rule 6: Invalid end token.
        ///     - '*' is not a number, variable, or ')'.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidEndTokenDefault2()
        {
            f1 = new("17 *");

        }

        /// <summary>
        ///     Calls the default constructor, should trigger parse rule 7: Invalid token following
        ///     an opening parentheses. Must be a number, variable, or '('. 
        ///     - cannot be a closing parentheses ')'. 
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestParenthesesFollowingRuleDefault()
        {
            f1 = new("()");
        }

        /// <summary>
        ///     Calls the default constructor, should trigger parse rule 7: Invalid token following
        ///     an opening parentheses. Must be a number, variable, or '('.
        ///     - cannot be a '+' operator.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestParenthesesFollowingRuleDefault2()
        {
            f1 = new("(*6");
        }

        /// <summary>
        ///     Calls the default constructor, should trigger parse rule 7: more rParentheses than lParentheses.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestParenthesesFollowingRuleDefault3()
        {
            f1 = new("(1*1))");
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidItemDefault()
        {
            f1 = new("123 ^ 2");
        }

        /// <summary>
        ///     Calls the default constructor, should trigger parse rule 7: Invalid token following an
        ///     operator. Must be a number, variable, or '('. 
        ///     - another operator cannot follow an operator.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestOperatorFollowingRuleDefault()
        {
            f1 = new("5**");
        }

        /// <summary>
        ///     Calls the default constructor, should trigger parse rule 7: Invalid token following an
        ///     operator. Must be a number, variable, or '('. 
        ///     - a closing parentheses ')' cannot follow an operator.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestOperatorFollowingRuleDefault2()
        {
            f1 = new("(5+10*)");
        }

        /// <summary>
        ///     Calls the default constructor, should trigger parse rule 8: Any token following a
        ///     number, variable, or ')' must be either an operator or a closing parentheses. 
        ///     - an opening parentheses '(' cannot follow a ')' parentheses. 
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExtraFollowingRuleDefault()
        {
            f1 = new("(5+1)(");
        }

        /// <summary>
        ///     Calls the default constructor, should trigger parse rule 8: Any token following a
        ///     number, variable, or ')' must be either an operator or a closing parentheses. 
        ///     - a number cannot follow another number.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExtraFollowingRuleDefault2()
        {
            f1 = new("5 5");
        }

        /// <summary>
        ///     Calls the default constructor, should trigger parse rule 8: Any token following a
        ///     number, variable, or ')' must be either an operator or a closing parentheses.
        ///     - a '(' cannot follow a variable.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExtraFollowingRuleDefault3()
        {
            f1 = new("X5(");
        }

        /// <summary>
        ///     Calls the default constructor, should trigger parse rule 8: Any token following a
        ///     number, variable, or ')' must be either an operator or a closing parentheses. 
        ///     - a variable cannot follow an ')' operator.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExtraFollowingRuleDefault4()
        {
            f1 = new("(5 + 0)X5");
        }

        // -- Default Constructor Tests (that should NOT throw) --
        /// <summary>
        ///     Calls the default constructor, should return each item from the formula separated in one list.
        ///     Code From: https://www.techiedelight.com/compare-two-lists-for-equality-csharp/ to compare the contents
        ///     of two lists.
        /// </summary>
        [TestMethod()]
        public void TestGetVariablesScientific()
        {
            f1 = new("5e5*1");

            List<string> expectedTokens = new List<string>() { };
            Assert.IsTrue(expectedTokens.SequenceEqual(f1.GetVariables().ToList()));
        }

        /// <summary>
        ///     Calls the default constructor, from the A1 tests. 
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [TestCategory("11")]
        public void TestParenthesesTimes()
        {
            f1 = new("(2+6)*3");
            Assert.AreEqual(24.0, f1.Evaluate(s => 0));
        }

        /// <summary>
        ///     Calls the default constructor, from the A1 tests. 
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [TestCategory("14")]
        public void TestPlusComplex()
        {
            f1 = new("2+(3+5*9)");
            Assert.AreEqual(50.0, f1.Evaluate(s => 0));
        }

        /// <summary>
        ///     Calls the default constructor, from the A1 tests. 
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [TestCategory("15")]
        public void TestOperatorAfterParens()
        {
            f1 = new("(1*1)-2/2");
            Assert.AreEqual(0.0, f1.Evaluate(s => 0));
        }

        /// <summary>
        ///     Calls the default constructor, from the A1 tests. 
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [TestCategory("27")]
        public void TestComplexNestedParensRight()
        {
            f1 = new("x1+(x2+(x3+(x4+(x5+x6))))");
            Assert.AreEqual(6.0, f1.Evaluate(s => 1));
        }

        /// <summary>
        ///     Calls the default constructor, from the A1 tests. 
        /// </summary>
        [TestMethod()]
        public void TestPlusMinusHasOperatorsCase()
        {
            f1 = new("(5+5-3)");
            Assert.AreEqual(7.0, f1.Evaluate(s => 1));
        }

        /// <summary>
        ///     Calls the default constructor, from the A1 tests. 
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [TestCategory("17")]
        public void TestComplexAndParentheses()
        {
            f1 = new("2+3*5+(3+4*8)*5+2");
            Assert.AreEqual(194.0, f1.Evaluate(s => 0));
        }

        /// <summary>
        ///     Calls the default constructor, from the A1 tests. 
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [TestCategory("16")]
        public void TestComplexTimesParentheses()
        {
            f1 = new("2+3*(3+5)");
            Assert.AreEqual(26.0, f1.Evaluate(s => 0));
        }

        /// <summary>
        ///     Calls the default constructor, from the A1 tests. 
        /// </summary>
        [TestMethod()]
        public void TestSimpleToString()
        {
            f1 = new("X + Y", n => n.ToLower(), v => true);
            Assert.AreEqual("x+y", f1.ToString());
        }

        /// <summary>
        ///     Calls the default constructor, from the A1 tests. 
        /// </summary>
        [TestMethod()]
        public void TestSimpleToString2()
        {
            f1 = new("x * y");
            Assert.AreEqual("x*y", f1.ToString());
        }

        /// <summary>
        ///     Calls the default constructor, should return 25.0
        /// </summary>
        [TestMethod()]
        public void TestComboAddition()
        {
            f1 = new("5+5+5+5+5");
            Assert.AreEqual(25.0, f1.Evaluate(s => 0));
        }

        /// <summary>
        ///     Calls the default constructor, returns -2.0
        /// </summary>
        [TestMethod()]
        public void TestReturnSub()
        {
            f1 = new("5-5");
            Assert.AreEqual(0.0, f1.Evaluate(s => 0));
        }

        /// <summary>
        ///     Calls the default constructor, returns 10.0
        /// </summary>
        [TestMethod()]
        public void TestReturnMultiply()
        {
            f1 = new("(6+4)*(1/1)");
            Assert.AreEqual(10.0, f1.Evaluate(s => 0));
        }

        /// <summary>
        ///     Calls the default constructor, should return each item from the formula separated in one list. 
        ///     Code From: https://www.techiedelight.com/compare-two-lists-for-equality-csharp/ to compare the contents
        ///     of two lists.
        /// </summary>
        [TestMethod()]
        public void TestGetVariablesSimpleDefault()
        {
            f1 = new("A1");

            List<string> expectedTokens = new List<string>() { "A1" };

            // Cited above. 
            Assert.IsTrue(expectedTokens.SequenceEqual(f1.GetVariables().ToList()));
        }

        /// <summary>
        ///     Calls the default constructor, should return each item from the formula separated in one list.
        ///     Code From: https://www.techiedelight.com/compare-two-lists-for-equality-csharp/ to compare the contents
        ///     of two lists.
        /// </summary>
        [TestMethod()]
        public void TestGetVariablesSimpleDefault2()
        {
            f1 = new("5+1");

            List<string> expectedTokens = new List<string>() { };
            Assert.IsTrue(expectedTokens.SequenceEqual(f1.GetVariables().ToList()));
        }

        /// <summary>
        ///     Calls the default constructor, should return each item from the formula separated in one list.
        ///     Code From: https://www.techiedelight.com/compare-two-lists-for-equality-csharp/ to compare the contents
        ///     of two lists.
        /// </summary>
        [TestMethod()]
        public void TestGetVariablesMoreComplicatedDefault()
        {
            f1 = new("(1+1)*2");

            List<string> expectedTokens = new List<string>() { };
            Assert.IsTrue(expectedTokens.SequenceEqual(f1.GetVariables().ToList()));
        }

        /// <summary>
        ///     Calls the default constructor, should return each item from the formula separated in one list.
        ///     Code From: https://www.techiedelight.com/compare-two-lists-for-equality-csharp/ to compare the contents
        ///     of two lists.
        /// </summary>
        [TestMethod]
        public void TestGetVariablesMoreComplicatedDefault2()
        {
            f1 = new("5*5+(10*10)-(30*(5-0))");

            List<string> expectedTokens = new List<string>() { };
            Assert.IsTrue(expectedTokens.SequenceEqual(f1.GetVariables().ToList()));
        }

        /// <summary>
        ///     Calls the default constructor, should return true because the formulas have the same tokens in
        ///     the same order. 
        /// </summary>
        [TestMethod()]
        public void TestGetHashCodeSimpleDefault()
        {
            f1 = new("1*1");
            f2 = new("1*1");

            Assert.IsTrue(f1.GetHashCode() == f2.GetHashCode());
        }

        /// <summary>
        ///     Calls the default constructor, should return false because the formulas do not have the same tokens.
        /// </summary>
        [TestMethod()]
        public void TestGetHashCodeSimpleDefault2()
        {
            f1 = new("1+100");
            f2 = new("100+1");

            Assert.IsFalse(f1.GetHashCode() == (f2.GetHashCode()));
        }

        /// <summary>
        ///     Calls the default constructor, should return true. They have the same formula (same tokens, same order).
        /// </summary>
        [TestMethod()]
        public void TestEqualsSimpleDefault()
        {
            f1 = new("1+1");
            f2 = new("1+1");

            Assert.IsTrue(f1.Equals(f2));
        }

        /// <summary>
        ///     Calls the default constructor, should return true (no matter how many spaces in the function).
        /// </summary>
        [TestMethod()]
        public void TestEqualsSimpleDefault2()
        {
            f1 = new("1*1");
            f2 = new("1    *   1");

            Assert.IsTrue(f1.Equals(f2));
        }

        /// <summary>
        ///     Calls the default constructor, should return false. They are not the same formula. 
        /// </summary>
        [TestMethod()]
        public void TestEqualsFalseDefault()
        {
            f1 = new("1 + 2");
            f2 = new("1 * 2");

            Assert.IsFalse(f1.Equals(f2));
        }

        /// <summary>
        ///     Calls the default constructor, should return false. The two items are not the same Object type.
        /// </summary>
        [TestMethod()]
        public void TestEqualsFalseDefault2()
        {
            Object f3 = new object();
            Object f4 = null;
            f1 = new("1*10");

            Assert.IsFalse(f1.Equals(f2));
            Assert.IsFalse(f1.Equals(f4));
        }

        /// <summary>
        ///     Calls the default constructor, should return false. The two items are different lengths.
        /// </summary>
        [TestMethod()]
        public void TestEqualsFalseDefault3()
        {
            f1 = new("1");
            f2 = new("1*1");

            Assert.IsFalse(f1.Equals(f2));
        }

        /// <summary>
        ///     Calls the default constructor, should return false. The numbers are not the same.
        /// </summary>
        [TestMethod()]
        public void TestEqualsFalseDefault4()
        {
            f1 = new("10");
            f2 = new("17");

            Assert.IsFalse(f1.Equals(f2));
        }

        /// <summary>
        ///     Calls the default constructor, should return true; This is true for 'Equals'.
        /// </summary>
        [TestMethod()]
        public void TestEqualsOperatorDefault()
        {
            f1 = new("10");
            f2 = new("10");

            Assert.IsTrue(f1 == f2);
        }

        /// <summary>
        ///     Calls the default constructor, should return false because this is a true statement. 
        /// </summary>
        [TestMethod()]
        public void TestInequalsOperatorDefault()
        {
            f1 = new("10");
            f2 = new("10");

            Assert.IsFalse(f1 != f2);
        }

        /// <summary>
        ///     Calls the default constructor, should return false because they are not equal.
        /// </summary>
        [TestMethod()]
        public void TestEqualsOperatorDefault2()
        {
            f1 = new("10");
            f2 = new("1");

            Assert.IsFalse(f1 == f2);
        }

        /// <summary>
        ///     Calls the default constructor, should return false because they are not equal. 
        /// </summary>
        [TestMethod()]
        public void TestInequalsOperatorDefault2()
        {
            f1 = new("10");
            f2 = new("100");

            Assert.IsTrue(f1 != f2);
        }

        [TestMethod()]
        public void TestEqualsWithVariablesDefault()
        {
            f1 = new("x1 + y1");
            f2 = new("X1 + Y1");

            Assert.IsFalse(f1.Equals(f2));
        }

        // -- Tests Using the Second Constructor (Formula(formula, normalizer, validator)) --
        /// <summary>
        ///     Calls the second constructor, should trigger parse rule 1: invalid formula.
        ///     - exclamation point (!) is invalid. 
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidTokenComplexConstructor()
        {
            f1 = new("!5", n => n, v => true);
        }

        /// <summary>
        ///     Calls the second constructor, should trigger parse rule 1: invalid formula. (test 2).
        ///     - modulus (%) is invalid.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidTokenComplex2()
        {
            f1 = new("(1+1)*5%2", n => n, v => true);
        }

        /// <summary>
        ///     Calls the complex constructor, should trigger parse rule 1: invalid formula. (test 3).
        ///     - exponent (^) is invalid.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidTokenComplex3()
        {
            f1 = new("2^x", n => n, v => true);
        }

        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidVariableComplex()
        {
            f1 = new("8 + null", n => n, v => true);
        }

        /// <summary>
        ///     Calls the complex constructor, should trigger parse rule 2: needs > 1 token.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestEmptyFormulaComplexConstructor()
        {
            f1 = new(" ", n => n, v => true);
        }

        /// <summary>
        ///     Calls the complex constructor, should trigger parse rule 3: the number of ')' 
        ///     should never be greater than the number of '('. (extra after the 5).
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExcessRightParenthesesComplex()
        {
            f1 = new("(1+1) * 5)", n => n, v => true);
        }

        /// <summary>
        ///     Calls the complex constructor, should trigger parse rule 3: the number of ')'
        ///     should never be greater than the number of '('. (extra on the end).
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExcessRightParenthesesComplex2()
        {
            f1 = new("(((1))))", n => n, v => true);
        }

        /// <summary>
        ///     Calls the complex constructor, should trigger parse rule 4: the number of '('
        ///     should be equal to the total number of ')'. 
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExcessLeftParenthesesComplex()
        {
            f1 = new("(1*10-(8+2)+((7))", n => n, v => true);
        }

        /// <summary>
        ///     Calls the complex constructor, should trigger parse rule 4: the number of '('
        ///     should be equal to the total number of ')'. 
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExcessLeftParenthesesComplex2()
        {
            f1 = new("((1) * 5", n => n, v => true);
        }

        /// <summary>
        ///     Calls the complex constructor, should trigger parse rule 5: Invalid starting token.
        ///     - '+' is not a number, variable, or '('. 
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidStartTokenComplex()
        {
            f1 = new("+5", n => n, v => true);
        }

        /// <summary>
        ///     Calls the complex constructor, should trigger parse rule 5: Invalid starting token.
        ///     - ')' is not a number, variable, or '('. 
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidStartTokenComplex2()
        {
            f1 = new(") * 17", n => n, v => true);
        }

        /// <summary>
        ///     Calls the complex constructor, should trigger parse rule 6: Invalid end token. 
        ///     - '(' is not a number, variable, or ')'. 
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidEndTokenComplex()
        {
            f1 = new("7 * 6(", n => n, v => true);
        }

        /// <summary>
        ///     Calls the complex constructor, should trigger parse rule 6: Invalid end token.
        ///     - '*' is not a number, variable, or ')'.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestInvalidEndTokenComplex2()
        {
            f1 = new("17 *", n => n, v => true);
        }

        /// <summary>
        ///     Calls the complex constructor, should trigger parse rule 7: Invalid token following
        ///     an opening parentheses. Must be a number, variable, or '('. 
        ///     - cannot be a closing parentheses ')'. 
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestParenthesesFollowingRuleComplex()
        {
            f1 = new("()", n => n, v => true);
        }

        /// <summary>
        ///     Calls the complex constructor, should trigger parse rule 7: Invalid token following
        ///     an opening parentheses. Must be a number, variable, or '('.
        ///     - cannot be a '+' operator.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestParenthesesFollowingRuleComplex2()
        {
            f1 = new("(*6", n => n, v => true);
        }

        /// <summary>
        ///     Calls the complex constructor, should trigger parse rule 7: Invalid token following an
        ///     operator. Must be a number, variable, or '('. 
        ///     - another operator cannot follow an operator.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestOperatorFollowingRuleComplex()
        {
            f1 = new("5**", n => n, v => true);
        }

        /// <summary>
        ///     Calls the complex constructor, should trigger parse rule 7: Invalid token following an
        ///     operator. Must be a number, variable, or '('. 
        ///     - a closing parentheses ')' cannot follow an operator.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestOperatorFollowingRuleComplex2()
        {
            f1 = new("(5+10*)", n => n, v => true);
        }

        /// <summary>
        ///     Calls the complex constructor, should trigger parse rule 8: Any token following a
        ///     number, variable, or ')' must be either an operator or a closing parentheses. 
        ///     - an opening parentheses '(' cannot follow a ')' parentheses. 
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExtraFollowingRuleComplex()
        {
            f1 = new("(5+1)(", n => n, v => true);
        }

        /// <summary>
        ///     Calls the complex constructor, should trigger parse rule 8: Any token following a
        ///     number, variable, or ')' must be either an operator or a closing parentheses. 
        ///     - a number cannot follow another number.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExtraFollowingRuleComplex2()
        {
            f1 = new("5 5", n => n, v => true);
        }

        /// <summary>
        ///     Calls the complex constructor, should trigger parse rule 8: Any token following a
        ///     number, variable, or ')' must be either an operator or a closing parentheses.
        ///     - a '(' cannot follow a variable.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExtraFollowingRuleComplex3()
        {
            f1 = new("X5(", n => n, v => true);
        }

        /// <summary>
        ///     Calls the complex constructor, should trigger parse rule 8: Any token following a
        ///     number, variable, or ')' must be either an operator or a closing parentheses. 
        ///     - a variable cannot follow an ')' operator.
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]
        public void TestExtraFollowingRuleComplex4()
        {
            f1 = new("(5+0)X5", n => n, v => true);
        }

        // -- Tests Using the Second Constructor (that should NOT throw) --

        /// <summary>
        ///     Calls the complex constructor, should return 2.0. 
        /// </summary>
        [TestMethod()]
        public void TestSimpleAddition()
        {
            f1 = new("1+1", n => n, v => true);

            Assert.IsTrue((f1.Evaluate(s => 0)).Equals(2.0));
        }

        /// <summary>
        ///     Calls the complex constructor, should return 0.0.
        /// </summary>
        [TestMethod()]
        public void TestSimpleSubtraction()
        {
            f1 = new("1-1", n => n, v => true);

            Assert.IsTrue((f1.Evaluate(s => 0)).Equals(0.0));
        }

        /// <summary>
        ///     Calls the complex constructor, should return 1.0.
        /// </summary>
        [TestMethod()]
        public void TestSimpleMultiplication()
        {
            f1 = new("1*1", n => n, v => true);

            Assert.IsTrue((f1.Evaluate(s => 0)).Equals(1.0));
        }

        /// <summary>
        ///     Calls the complex constructor, should return 1.0.
        /// </summary>
        [TestMethod()]
        public void TestSimpleDivision()
        {
            f1 = new("1/1", n => n, v => true);

            Assert.IsTrue((f1.Evaluate(s => 0)).Equals(1.0));
        }

        /// <summary>
        ///     Calls the complex constructor, should return a FormulaError because it was attempted to divideByZero.
        ///     Test Source: https://utah.instructure.com/courses/834041/external_tools/90790
        /// </summary>
        [TestMethod()]
        public void TestSimpleDivideByZero()
        {
            f1 = new("1/0", n => n, v => true);

            Assert.IsInstanceOfType(f1.Evaluate(s => 0), typeof(FormulaError));
        }

        /// <summary>
        ///     Calls the complex constructor, should return a FormulaError because it was attempted to divideByZero.
        /// </summary>
        [TestMethod()]
        public void TestSimpleDivideByZero2()
        {
            f1 = new("1/A1", n => n, v => true);

            Assert.IsInstanceOfType(f1.Evaluate(s => 0), typeof(FormulaError));
        }

        /// <summary>
        ///     Calls the complex constructor, should return a FormulaError because there is no associated value with 'null'.
        ///     Test Source: https://utah.instructure.com/courses/834041/external_tools/90790
        /// </summary>
        [TestMethod()]
        [ExpectedException(typeof(FormulaFormatException))]

        public void TestSimpleInvalidArgument()
        {
            f1 = new("1 * null", n => n, v => false);
        }

        /// <summary>
        ///     Calls the complex constructor, should return simply the number from the formula.
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [TestCategory("1")]
        public void TestSingleNumber()
        {
            f1 = new("5", n => n, v => true);
            Assert.AreEqual(5.0, f1.Evaluate(s => 0));
        }

        /// <summary>
        ///     Calls the complex constructor, should return 10 (b1 set to 5 * 2)
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [TestCategory("2")]
        public void TestSingleVariable()
        {
            f1 = new("b1 * 2", n => n, v => true);
            Assert.AreEqual(10.0, f1.Evaluate(s => 5));
        }

        /// <summary>
        ///     Calls the complex constructor, should return 6 (2 + X1 (set to 4)).
        /// </summary>
        [TestMethod(), Timeout(5000)]
        [TestCategory("7")]
        public void TestArithmeticWithVariable()
        {
            f1 = new("2+X1", n => n, v => true);
            Assert.AreEqual(6.0, f1.Evaluate(s => 4));
        }

        /// <summary>
        ///     Calls the complex constructor, executes the normalization within the Equals method.
        /// </summary>
        [TestMethod()]
        public void TestNormalizingInEqualsMethod()
        {
            f1 = new("x+y*2", n => n.ToLower(), v => true);
            f2 = new("X+Y*2", n => n.ToLower(), v => true);

            Assert.IsTrue(f1.Equals(f2));
        }

        /// <summary>
        ///     Calls the complex constructor, executes the normalization within the Equals method. Aims to
        ///     get the method to compare "a" and "c" and determine that they are not equal. 
        /// </summary>
        [TestMethod()]
        public void TestNormalizingInEqualsMethodFalse()
        {
            f1 = new("A+B+c", n => n, v => true);
            f2 = new("C+B+A", n => n, v => true);

            Assert.IsFalse(f1.Equals(f2));
        }
    }
}