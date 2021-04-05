using System;
using System.Collections.Generic;
using System.Linq;

namespace Isomorphic_Strings
{
    class Isomorph
    {
        /// <summary>
        /// Generates the exact isomorph of a string.
        /// </summary>
        /// <param name="str">string to generate off of</param>
        /// <returns>
        ///     Tuple of string,string,int[] as: 
        ///         Item1=[the string isomorph array description]
        ///         Item2=[the original string]
        ///         Item3=[array of isomorph integers]
        /// </returns>
        public static Tuple<string, string, int[]> FindExact(string str)
        {
            var characters = new List<char>(str.ToCharArray());
            Dictionary<char, int> dict = characters
                .Distinct()
                .Select((e, i) => new { elem = e, index = i })
                .ToDictionary(x => x.elem, x => x.index);
            var isomorph = "";
            var arr = new List<int>();
            characters.ForEach(c => { isomorph += $" {dict[c]}"; arr.Add(dict[c]); });
            isomorph = isomorph.TrimStart();
            return new Tuple<string, string, int[]>(isomorph, str, arr.ToArray());
        }

        /// <summary>
        /// Generates the loose isomorph of a string 
        /// </summary>
        /// <param name="str">string to generate off of</param>
        /// <returns>
        ///     Tuple of string,string,int[] as: 
        ///         Item1=[the string isomorph array description]
        ///         Item2=[the original string]
        ///         Item3=[array of isomorph integers]
        /// </returns>
        public static Tuple<string, string, int[]> FindLoose(string str)
        {
            var loose_iso = new List<int>(FindExact(str).Item3)
                .GroupBy(x => x)
                .Select(x => x.Count())
                .OrderBy(x => x)
                .ToList();
            var isomorph = "";
            loose_iso.ForEach(x => isomorph += $" {x}");
            isomorph = isomorph.TrimStart();
            return new Tuple<string, string, int[]>(isomorph, str, loose_iso.ToArray());
        }
    }
}
