/// <summary>
/// Author: Riley Kraabel
/// Partner: -none-
/// Date (of creation): 23-Jan-2023
/// Course: CS 3500, University of Utah, School of Computing
/// Copyright: CS 3500 and Riley Kraabel - This work may not be copied for use in Academic Coursework.
/// 
/// I, Riley Kraabel, certify that I wrote this code from scratch and did not copy it in part or whole from another source. 
/// All references used in the completion of the assignments are cited in my README file.
/// 
/// File Contents:
/// 
///     This file contains a multitude of test cases for the DependencyGraph test class. Edge cases are tested, along with
///     cases that combine on top of each other. There are multiple tests for each of the methods inside of the DependencyGraph
///     class.
/// 
/// </summary>

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NuGet.Frameworks;
using SpreadsheetUtilities;

namespace DevelopmentTests
{
    /// <summary>
    ///This is a test class for DependencyGraphTest and is intended to contain all DependencyGraphTest Unit Tests.
    ///
    ///</summary>
    [TestClass()]
    public class DependencyGraphTests
    {
        /// <summary>
        /// Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void SimpleEmptyTest()
        {
            DependencyGraph t = new DependencyGraph();
            Assert.AreEqual(0, t.Size);
        }

        /// <summary>
        /// Should have a dependent if it was added correctly
        /// </summary>
        [TestMethod()]
        public void SimpleHasDependentsTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            Assert.IsTrue(t.HasDependents("a"));
            Assert.IsFalse(t.HasDependents("b"));
            Assert.AreNotEqual(2, t["b"]);
        }

        /// <summary>
        /// Should have a dependee if it was added correctly
        /// </summary>
        [TestMethod()]
        public void SimpleHasDependeesTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            Assert.IsTrue(t.HasDependees("y"));
            Assert.IsFalse(t.HasDependees("x"));
            Assert.AreEqual(1, t["y"]);
        }

        /// <summary>
        /// Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void SimpleEmptyRemoveTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            Assert.AreEqual(1, t.Size);
            t.RemoveDependency("x", "y");
            Assert.AreEqual(0, t.Size);
        }

        /// <summary>
        /// Empty graph should be able to be added to
        /// </summary>
        [TestMethod()]
        public void SimpleEmptyAddTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("a", "d");
            t.AddDependency("a", "e");
            Assert.AreEqual(4, t.Size);
        }

        /// <summary>
        /// Should not add duplicates, then should return how many dependees are associated with each of the different values
        /// </summary>
        [TestMethod()]
        public void MoreComplexAddTestWithThis()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("a", "d");
            t.AddDependency("a", "e");
            t.AddDependency("a", "e");
            Assert.AreEqual(4, t.Size);
            t.RemoveDependency("a", "b");
            Assert.AreEqual(1, t["c"]);
            Assert.AreEqual(1, t["d"]);
            t.RemoveDependency("a", "c");
            Assert.AreEqual(0, t["c"]);
        }

        /// <summary>
        /// All of the values added, except the last, should be considered t's dependees, and the size of this should go up.
        /// </summary>
        [TestMethod()]
        public void ThoroughThisTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("b", "b");
            t.AddDependency("c", "b");
            t.AddDependency("d", "b");
            t.AddDependency("e", "b");
            t.AddDependency("f", "b");
            Assert.AreEqual(6, t["b"]);
            Assert.IsTrue(t.HasDependents("b"));
            t.AddDependency("b", "a");
            Assert.AreEqual(6, t["b"]);
            Assert.AreEqual(1, t["a"]);
            Assert.IsTrue(t.HasDependees("a"));
        }

        /// <summary>
        /// The mirrored pair should not affect anything because it is mirrored and not identical. 
        /// </summary>
        [TestMethod()]
        public void MirroredPairTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("b", "a");
            Assert.AreEqual(2, t.Size);

            t.AddDependency("a", "b");
            Assert.AreEqual(2, t.Size);
        }

        /// <summary>
        /// Duplicate should not be added, but capital lettered dependency should
        /// </summary>
        [TestMethod()]
        public void SimpleDuplicateTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            Assert.AreEqual(1, t.Size);
            t.AddDependency("x", "y");
            Assert.AreEqual(1, t.Size);
            t.AddDependency("X", "Y");
            Assert.AreEqual(2, t.Size);
        }
        /// <summary>
        /// Ensure that if there are no dependees/dependents, the test will return false
        /// </summary>
        [TestMethod()]
        public void HasDepsFalseTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            Assert.IsTrue(t.HasDependees("y"));
            Assert.IsTrue(t.HasDependents("x"));
            t.RemoveDependency("x", "y");
            Assert.IsFalse(t.HasDependees("y"));
            Assert.IsFalse(t.HasDependents("x"));
        }

        /// <summary>
        /// Ensure that if no dependees are present, moveNext() will return false.
        /// </summary>
        [TestMethod()]
        public void GetNoDependeesTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            Assert.AreEqual(1, t.Size);
            IEnumerator<string> e1 = t.GetDependees("y").GetEnumerator();
            Assert.IsTrue(e1.MoveNext());
            Assert.AreEqual("x", e1.Current);

            t.RemoveDependency("x", "y");
            IEnumerator<string> e2 = t.GetDependees("y").GetEnumerator();
            Assert.IsFalse(e1.MoveNext());

            Assert.AreEqual(0, t["a"]);
        }

        /// <summary>
        /// Ensure that if no dependents are present, moveNext() will return false
        /// </summary>
        [TestMethod()]
        public void GetNoDependentsTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("A", "B");
            Assert.AreEqual(1, t.Size);
            IEnumerator<string> e1 = t.GetDependents("A").GetEnumerator();
            Assert.IsTrue(e1.MoveNext());
            Assert.AreEqual("B", e1.Current);

            t.RemoveDependency("A", "B");
            IEnumerator<string> e2 = t.GetDependents("A").GetEnumerator();
            Assert.IsFalse(e1.MoveNext());

            Assert.AreEqual(0, t["B"]);
        }

        /// <summary>
        /// Empty graph should contain nothing
        ///</summary>
        [TestMethod()]
        public void EmptyEnumeratorTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            IEnumerator<string> e1 = t.GetDependees("y").GetEnumerator();
            Assert.IsTrue(e1.MoveNext());
            Assert.AreEqual("x", e1.Current);
            IEnumerator<string> e2 = t.GetDependents("x").GetEnumerator();
            Assert.IsTrue(e2.MoveNext());
            Assert.AreEqual("y", e2.Current);
            t.RemoveDependency("x", "y");
            Assert.IsFalse(t.GetDependees("y").GetEnumerator().MoveNext());
            Assert.IsFalse(t.GetDependents("x").GetEnumerator().MoveNext());
        }

        /// <summary>
        /// Replace on an empty DG shouldn't fail
        ///</summary>
        [TestMethod()]
        public void SimpleReplaceTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "y");
            Assert.AreEqual(t.Size, 1);
            t.RemoveDependency("x", "y");
            t.ReplaceDependents("x", new HashSet<string>());
            t.ReplaceDependees("y", new HashSet<string>());
        }

        ///<summary>
        /// It should be possible to have more than one DG at a time.
        ///</summary>
        [TestMethod()]
        public void StaticTest()
        {
            DependencyGraph t1 = new DependencyGraph();
            DependencyGraph t2 = new DependencyGraph();
            t1.AddDependency("x", "y");
            Assert.AreEqual(1, t1.Size);
            Assert.AreEqual(0, t2.Size);
        }

        /// <summary>
        /// Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void SizeTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");
            Assert.AreEqual(4, t.Size);
        }

        /// <summary>
        /// Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void EnumeratorTest()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("a", "b");
            t.AddDependency("a", "c");
            t.AddDependency("c", "b");
            t.AddDependency("b", "d");
            IEnumerator<string> e = t.GetDependees("a").GetEnumerator();
            Assert.IsFalse(e.MoveNext());
            e = t.GetDependees("b").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            String s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            String s2 = e.Current;
            Assert.IsFalse(e.MoveNext());
            Assert.IsTrue(((s1 == "a") && (s2 == "c")) || ((s1 == "c") && (s2 == "a")));
            e = t.GetDependees("c").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("a", e.Current);
            Assert.IsFalse(e.MoveNext());
            e = t.GetDependees("d").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("b", e.Current);
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        /// Non-empty graph contains something
        ///</summary>
        [TestMethod()]
        public void ReplaceThenEnumerate()
        {
            DependencyGraph t = new DependencyGraph();
            t.AddDependency("x", "b");
            t.AddDependency("a", "z");
            t.ReplaceDependents("b", new HashSet<string>());
            t.AddDependency("y", "b");
            t.ReplaceDependents("a", new HashSet<string>() { "c" });
            t.AddDependency("w", "d");
            t.ReplaceDependees("b", new HashSet<string>() { "a", "c" });
            t.ReplaceDependees("d", new HashSet<string>() { "b" });
            IEnumerator<string> e = t.GetDependees("a").GetEnumerator();
            Assert.IsFalse(e.MoveNext());
            e = t.GetDependees("b").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            String s1 = e.Current;
            Assert.IsTrue(e.MoveNext());
            String s2 = e.Current;
            Assert.IsFalse(e.MoveNext());
            Assert.IsTrue(((s1 == "a") && (s2 == "c")) || ((s1 == "c") && (s2 == "a")));
            e = t.GetDependees("c").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("a", e.Current);
            Assert.IsFalse(e.MoveNext());
            e = t.GetDependees("d").GetEnumerator();
            Assert.IsTrue(e.MoveNext());
            Assert.AreEqual("b", e.Current);
            Assert.IsFalse(e.MoveNext());
        }

        /// <summary>
        /// Using lots of data
        ///</summary>
        [TestMethod()]
        public void StressTest()
        {
            // Dependency graph
            DependencyGraph t = new DependencyGraph();
            // A bunch of strings to use
            const int SIZE = 200;
            string[] letters = new string[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                letters[i] = ("" + (char)('a' + i));
            }

            // The correct answers
            HashSet<string>[] dents = new HashSet<string>[SIZE];
            HashSet<string>[] dees = new HashSet<string>[SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                dents[i] = new HashSet<string>();
                dees[i] = new HashSet<string>();
            }

            // Add a bunch of dependencies
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 1; j < SIZE; j++)
                {
                    t.AddDependency(letters[i], letters[j]);
                    dents[i].Add(letters[j]);
                    dees[j].Add(letters[i]);
                }
            }

            // Remove a bunch of dependencies
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 4; j < SIZE; j += 4)
                {
                    t.RemoveDependency(letters[i], letters[j]);
                    dents[i].Remove(letters[j]);
                    dees[j].Remove(letters[i]);
                }
            }

            // Add some back
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = i + 1; j < SIZE; j += 2)
                {
                    t.AddDependency(letters[i], letters[j]);
                    dents[i].Add(letters[j]);
                    dees[j].Add(letters[i]);
                }
            }

            // Remove some more
            for (int i = 0; i < SIZE; i += 2)
            {
                for (int j = i + 3; j < SIZE; j += 3)
                {
                    t.RemoveDependency(letters[i], letters[j]);
                    dents[i].Remove(letters[j]);
                    dees[j].Remove(letters[i]);
                }
            }

            // Make sure everything is right
            for (int i = 0; i < SIZE; i++)
            {
                Assert.IsTrue(dents[i].SetEquals(new HashSet<string>(t.GetDependents(letters[i]))));
                Assert.IsTrue(dees[i].SetEquals(new HashSet<string>(t.GetDependees(letters[i]))));
            }

        }
    }
}