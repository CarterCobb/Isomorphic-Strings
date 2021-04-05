using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Isomorphic_Strings
{
    class FileIO
    {
        /// <summary>
        /// Reads a text file
        /// </summary>
        /// <param name="path">File Path</param>
        /// <returns>List of lines</returns>
        public static List<string> ReadTextFile(string path)
        {
            if (!File.Exists(path)) return null;
            return new List<string>(File.ReadAllLines(path));
        }

        /// <summary>
        /// Writes text to a text file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="text">Text to write</param>
        public static void WriteToTextFile(string path, string text)
        {
            File.WriteAllText(path, text.Replace("\n", Environment.NewLine));
        }
    }
}
