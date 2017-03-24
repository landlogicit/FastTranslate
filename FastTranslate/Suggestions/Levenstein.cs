using System;

namespace FastTranslate.Suggestions
{
    #region Copyright

    /*
 * The original .NET implementation of the SimMetrics library is taken from the Java
 * source and converted to NET using the Microsoft Java converter.
 * It is notclear who made the initial convertion to .NET.
 * 
 * This updated version has started with the 1.0 .NET release of SimMetrics and used
 * FxCop (http://www.gotdotnet.com/team/fxcop/) to highlight areas where changes needed 
 * to be made to the converted code.
 * 
 * this version with updates Copyright (c) 2006 Chris Parkinson.
 * 
 * For any queries on the .NET version please contact me through the 
 * sourceforge web address.
 * 
 * SimMetrics - SimMetrics is a java library of Similarity or Distance
 * Metrics, e.g. Levenshtein Distance, that provide float based similarity
 * measures between string Data. All metrics return consistant measures
 * rather than unbounded similarity scores.
 *
 * Copyright (C) 2005 Sam Chapman - Open Source Release v1.1
 *
 * Please Feel free to contact me about this library, I would appreciate
 * knowing quickly what you wish to use it for and any criticisms/comments
 * upon the SimMetric library.
 *
 * email:       s.chapman@dcs.shef.ac.uk
 * www:         http://www.dcs.shef.ac.uk/~sam/
 * www:         http://www.dcs.shef.ac.uk/~sam/stringmetrics.html
 *
 * address:     Sam Chapman,
 *              Department of Computer Science,
 *              University of Sheffield,
 *              Sheffield,
 *              S. Yorks,
 *              S1 4DP
 *              United Kingdom,
 *
 * This program is free software; you can redistribute it and/or modify it
 * under the terms of the GNU General Public License as published by the
 * Free Software Foundation; either version 2 of the License, or (at your
 * option) any later version.
 *
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License
 * for more details.
 *
 * You should have received a copy of the GNU General Public License along
 * with this program; if not, write to the Free Software Foundation, Inc.,
 * 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
 */

    #endregion

    /// <summary>
    /// Compares how similar two strings are using Levenstein algorithm.
    /// Complexity: O(n*m), where n and m is the length of the input strings.
    /// </summary>
    [Serializable]
    public sealed class Levenstein
    {
        private const double DefaultPerfectMatchScore = 1.0;
        private const double DefaultMismatchScore = 0.0;

        private static readonly SubCostRange0To1 DCostFunction = new SubCostRange0To1();

        /// <summary>
        /// Gets the similarity of the two strings using levenstein distance,
        /// returning a value between 0-1 of the similarity.
        /// </summary>
        public double GetSimilarity(string firstWord, string secondWord)
        {
            if (firstWord == null || secondWord == null)
                return DefaultMismatchScore;
            double levensteinDistance = GetUnnormalisedSimilarity(firstWord, secondWord);
            int maxLen = Math.Max(firstWord.Length, secondWord.Length);
            if (maxLen == 0)
                return DefaultPerfectMatchScore;
            return DefaultPerfectMatchScore - levensteinDistance / maxLen;
        }

        /// <summary>
        /// Gets the un-normalised similarity measure of the metric for the given strings, in number of mismatching characters.
        /// </summary>
        /// <remarks>
        /// <p />
        /// Copy character from string1 over to string2 (cost 0)
        /// Delete a character in string1 (cost 1)
        /// Insert a character in string2 (cost 1)
        /// Substitute one character for another (cost 1)
        /// <p />
        /// D(i-1,j-1) + d(si,tj) //subst/copy
        /// D(i,j) = min D(i-1,j)+1 //insert
        /// D(i,j-1)+1 //delete
        /// <p />
        /// d(i,j) is a function whereby d(c,d)=0 if c=d, 1 else.
        /// </remarks>
        public double GetUnnormalisedSimilarity(string firstWord, string secondWord)
        {
            if (firstWord == null || secondWord == null)
                return DefaultMismatchScore;
            // Step 1
            int n = firstWord.Length;
            int m = secondWord.Length;
            if (n == 0)
                return m;
            if (m == 0)
                return n;

            var d = new double[n + 1][];
            for (int i = 0; i < n + 1; i++)
            {
                d[i] = new double[m + 1];
            }

            // Step 2
            for (int i = 0; i <= n; i++)
            {
                d[i][0] = i;
            }
            for (int j = 0; j <= m; j++)
            {
                d[0][j] = j;
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                // Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    double cost = DCostFunction.GetCost(firstWord, i - 1, secondWord, j - 1);
                    // Step 6
                    d[i][j] = MinOf3(d[i - 1][j] + 1.0, d[i][j - 1] + 1.0, d[i - 1][j - 1] + cost);
                }
            }

            // Step 7
            return d[n][m];
        }

        private static double MinOf3(double firstNumber, double secondNumber, double thirdNumber)
        {
            return Math.Min(firstNumber, Math.Min(secondNumber, thirdNumber));
        }
    }

    /// <summary>
    /// implements a substitution cost function where d(i,j) = 1 if idoes not equal j, 0 if i equals j.
    /// </summary>
    [Serializable]
    internal sealed class SubCostRange0To1
    {
        private const int CharExactMatchScore = 1;
        private const int CharMismatchMatchScore = 0;

        /// <summary>
        /// get cost between characters where d(i,j) = 1 if i does not equals j, 0 if i equals j.
        /// </summary>
        /// <param name="firstWord">the string1 to evaluate the cost</param>
        /// <param name="firstWordIndex">the index within the string1 to test</param>
        /// <param name="secondWord">the string2 to evaluate the cost</param>
        /// <param name="secondWordIndex">the index within the string2 to test</param>
        /// <returns>the cost of a given subsitution d(i,j) where d(i,j) = 1 if i!=j, 0 if i==j</returns>
        public double GetCost(string firstWord, int firstWordIndex, string secondWord, int secondWordIndex)
        {
            if (firstWord == null || secondWord == null)
                return 0.0;

            return firstWord[firstWordIndex] != secondWord[secondWordIndex]
                ? CharExactMatchScore
                : CharMismatchMatchScore;
        }
    }
}