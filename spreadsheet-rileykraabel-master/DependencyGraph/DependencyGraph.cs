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
///     This file functions as a class to handle the organization of dependencies. The class is implemented using a Dictionary
///     data-structure that holds Strings as keys and HashSet<string> items as values. By implementing the class with this data
///     structure, the methods are ensured to run at O(1) complexity for all methods (other than replacing values). Methods
///     include a constructor, size, and other relevant methods for determining traits of Dependees and Dependents for a given
///     Dependency Graph.
/// 
/// </summary>


// Skeleton implementation written by Joe Zachary for CS 3500, September 2013.
// Version 1.1 (Fixed error in comment for RemoveDependency.)
// Version 1.2 - Daniel Kopta 
//               (Clarified meaning of dependent and dependee.)
//               (Clarified names in solution/project structure.)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
namespace SpreadsheetUtilities
{
    /// <summary>
    /// (s1,t1) is an ordered pair of strings
    /// t1 depends on s1; s1 must be evaluated before t1
    /// 
    /// A DependencyGraph can be modeled as a set of ordered pairs of strings.  Two ordered pairs
    /// (s1,t1) and (s2,t2) are considered equal if and only if s1 equals s2 and t1 equals t2.
    /// Recall that sets never contain duplicates.  If an attempt is made to add an     element to a
    /// set, and the element is already in the set, the set remains unchanged.
    /// 
    /// Given a DependencyGraph DG:
    /// 
    ///    (1) If s is a string, the set of all strings t such that (s,t) is in DG is called dependents(s).
    ///        (The set of things that depend on s)    
    ///        
    ///    (2) If s is a string, the set of all strings t such that (t,s) is in DG is called dependees(s).
    ///        (The set of things that s depends on) 
    //
    // For example, suppose DG = {("a", "b"), ("a", "c"), ("b", "d"), ("d", "d")}
    //     dependents("a") = {"b", "c"}
    //     dependents("b") = {"d"}
    //     dependents("c") = {}
    //     dependents("d") = {"d"}
    //     dependees("a") = {}
    //     dependees("b") = {"a"}
    //     dependees("c") = {"a"}
    //     dependees("d") = {"b", "d"}
    /// </summary>
    public class DependencyGraph
    {
        private Dictionary<string, HashSet<string>> dependents;
        private Dictionary<string, HashSet<string>> dependees;
        private int size;

        /// <summary>
        /// Creates an empty DependencyGraph.
        /// Constructor for creating the DependencyGraph object.
        /// </summary>
        public DependencyGraph()
        {
            dependents = new Dictionary<string, HashSet<string>>();
            dependees = new Dictionary<string, HashSet<string>>();
            size = 0;
        }

        /// <summary>
        /// The number of ordered pairs in the DependencyGraph.
        /// </summary>
        /// <returns> int type, the number of ordered pairs in the DependencyGraph. </returns>
        public int Size
        {
            get { return size; }
        }

        /// <summary>
        /// The size of dependees(s). Invoked like DependencyGraph var ["s"].
        /// </summary>
        /// <param name="s"> string type, the item we want to find the dependees of. </param>
        /// <returns> int type, the size of 'dependees(s)'. </returns>
        public int this[string s]
        {
            get 
            {
                if (dependees.ContainsKey(s))
                    return dependees[s].Count;

                else
                    return 0;
            }
        }

        /// <summary>
        /// Reports whether dependents(s) is non-empty.
        /// </summary>
        /// <param name="s"> string type, the item we want to find the dependents of. </param>
        /// <returns> bool type, True if there are items inside of dependents(s), False if not. </returns>
        public bool HasDependents(string s)
        {
            if (dependents.ContainsKey(s) && dependents[s].Count != 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Reports whether dependees(s) is non-empty.
        /// </summary>
        /// <param name="s"> string type, the item we want to find the dependees of. </param>
        /// <returns> bool type, True if there are items inside of dependees(s), False if not. </returns>
        public bool HasDependees(string s)
        {
            if (dependees.ContainsKey(s) && dependees[s].Count != 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Enumerates dependents(s).
        /// </summary>
        /// <param name="s"> string type, the item we want to find the number of dependents of. </param>
        /// <returns> type IEnumerable<string>, an Enumerable determining how many items are inside of dependents(s). </returns>
        public IEnumerable<string> GetDependents(string s)
        {
            if (dependents.ContainsKey(s))
                return new HashSet<string>(dependents[s]);

            return new HashSet<string>();
        }

        /// <summary>
        /// Enumerates dependees(s).
        /// </summary>
        /// <param name="s"> string type, the item we want to find the number of dependees of. </param>
        /// <returns> type IEnumerable<string>, an Enumerable determining how many items are inside of dependees(s). </returns>
        public IEnumerable<string> GetDependees(string s)
        {
            if (dependees.ContainsKey(s))
                return new HashSet<string>(dependees[s]);

            return new HashSet<string>();
        }

        /// <summary>
        ///     Adds the ordered pair (s,t), if it doesn't already exist.
        /// </summary>
        /// <param name="s"> s must be evaluated first. T depends on S</param>
        /// <param name="t"> t cannot be evaluated until s is </param>
        public void AddDependency(string s, string t)
        {
            if (!dependents.ContainsKey(s) && !dependees.ContainsKey(t))
            {
                dependents.Add(s, new HashSet<string> { t });
                dependees.Add(t, new HashSet<string> { s });

                dependents.Add(t, new HashSet<string> { });
                dependees.Add(s, new HashSet<string> { });
            }

            else if (!dependents.ContainsKey(s) && dependees.ContainsKey(t))
            {                
                dependees[t].Add(s);

                dependents.Add(s, new HashSet<string> { t });
                dependees.Add(s, new HashSet<string> { });
            }

            else if (dependents.ContainsKey(s) && !dependees.ContainsKey(t))
            {
                dependents[s].Add(t);

                dependees.Add(t, new HashSet<string> { s });
                dependents.Add(t, new HashSet<string> { });
            }

            else if (dependents.ContainsKey(s) && dependees.ContainsKey(t))
            {
                if (!dependents[s].Contains(t) && !dependees[t].Contains(s))
                {
                    dependents[s].Add(t);
                    dependees[t].Add(s);
                }

                else
                    return;
            }

            size++;

        }

        /// <summary>
        ///     Removes the ordered pair (s,t), if it exists. 
        /// </summary>
        /// <param name="s"> string type, the x value of the pair we want to remove. </param>
        /// <param name="t"> string type, the y value of the pair we want to remove. </param>
        public void RemoveDependency(string s, string t)
        {
            if (dependents.ContainsKey(s) && dependents[s].Contains(t))
                dependents[s].Remove(t);

            if (dependees.ContainsKey(t) && dependees[t].Contains(s))
                dependees[t].Remove(s);

            size--;

        }

        /// <summary>
        /// Removes all existing ordered pairs of the form (s,r).  Then, for each
        /// t in newDependents, adds the ordered pair (s,t).
        /// </summary>
        /// <param name="s"> string type, the item we want to replace. </param>
        /// <param name="newDependents"> IEnumerable<string> type, the items we want to add into the DG. </param>
        public void ReplaceDependents(string s, IEnumerable<string> newDependents)
        {
            if (dependents.ContainsKey(s))
            {
                foreach (string r in dependents[s])
                    RemoveDependency(s, r);
            }

            foreach (string t in newDependents)
                AddDependency(s, t);
        }

        /// <summary>
        /// Removes all existing ordered pairs of the form (r,s).  Then, for each 
        /// t in newDependees, adds the ordered pair (t,s).
        /// </summary>
        /// <param name="s"> string type, the item we want to replace. </param>
        /// <param name="newDependees"> IEnumerable<string> type, the items we want to add into the DG. </param>
        public void ReplaceDependees(string s, IEnumerable<string> newDependees)
        {
            if (dependees.ContainsKey(s))
            {
                foreach (string r in dependees[s])
                    RemoveDependency(r, s);
            }

            foreach (string t in newDependees)
                AddDependency(t, s);
        }
    }
}