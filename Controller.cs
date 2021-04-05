using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Isomorphic_Strings
{
    class Controller
    {
        Stopwatch watch = new Stopwatch(); // Used to show time of execution
        string PATH = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName
            + "\\Input_Files\\IsomorphInput1.txt";
        string PATH_2 = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName
            + "\\Input_Files\\IsomorphInput2.txt";
        List<string> words = new List<string>();
        Dictionary<string, Tuple<string, string>> isomorphic_variants = new Dictionary<string, Tuple<string, string>>();

        /// <summary>
        /// Runs the program 
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Welcome!");
            var user_path = IO.GetConsoleString(
                $"Enter exact file path (e.g. 'C:\\path\\to\\file.txt'){Environment.NewLine} - 'D' for IsomorphInput1.txt{Environment.NewLine} - 'D2' for IsomorphInput2.txt:{Environment.NewLine}-> ");
            if (!user_path.Equals("D")) PATH = user_path;
            if (user_path.Equals("D2")) PATH = PATH_2;
            watch.Start();
            if (user_path.Equals("D2") || user_path.Equals("D")) Console.WriteLine($"Using path: .{PATH[^31..]}");
            else Console.WriteLine($"Using path: {PATH}");
            words = FileIO.ReadTextFile(PATH);
            if (words == null) Console.WriteLine("File Not Found :(");
            else
            {
                Console.WriteLine($"Words Found: {(words != null ? words.Count : 0)}{Environment.NewLine}Isomorphizing...{Environment.NewLine}-------------OUTPUT--------------");
                words.ForEach(word => isomorphic_variants.Add(word, new Tuple<string, string>(Isomorph.FindExact(word).Item1, Isomorph.FindLoose(word).Item1)));
                PrintData();
            }
        }

        /// <summary>
        /// After gathering the file path and setting the words,
        /// this will format, log, and save the output
        /// </summary>
        public void PrintData()
        {
            var dict = new Dictionary<string, List<Tuple<string, List<string>>>>()
            {
                { "Exact Isomorphs", new List<Tuple<string, List<string>>>() },
                { "Loose Isomorphs", new List<Tuple<string, List<string>>>() },
                { "Non-isomorphs", new List<Tuple<string, List<string>>>() }
            };
            // The term 'pattern' refers to the isomorphic representation of the word - either in 'exact' form or 'loose' form
            foreach (var entry in isomorphic_variants)
            {
                if (dict["Exact Isomorphs"].Select(x => x.Item1).Contains(entry.Value.Item1))
                    dict["Exact Isomorphs"].Find(x => x.Item1 == entry.Value.Item1).Item2.Add(entry.Key); // Add word to list with 'Key' of pattern if exists
                else dict["Exact Isomorphs"].Add(new Tuple<string, List<string>>(entry.Value.Item1, new List<string> { entry.Key })); // else create new Tuple with word keyed by pattern
                if (dict["Loose Isomorphs"].Select(x => x.Item1).Contains(entry.Value.Item2))
                    dict["Loose Isomorphs"].Find(x => x.Item1 == entry.Value.Item2).Item2.Add(entry.Key); // Add word to list with 'Key' of pattern if exists
                else dict["Loose Isomorphs"].Add(new Tuple<string, List<string>>(entry.Value.Item2, new List<string> { entry.Key })); // else create new Tuple with word keyed by pattern
            }
            ExtractNonIsomorphs(ref dict);
            var output = "";
            foreach (var entry in dict)
            {
                output += $"{entry.Key}\n";
                foreach (var item in entry.Value.OrderBy(x => $"0.{x.Item1.Replace(" ", "")}"))
                {
                    output += !entry.Key.Equals("Non-isomorphs") ? $"{item.Item1}: " : ""; // Pattern
                    var line = "";
                    foreach (var word in item.Item2.OrderBy(x => x))
                        line += $" {word}"; // Word for each pattern
                    output += line.TrimStart() + "\n"; // Enter to new Pattern Line
                }
                output += !entry.Key.Equals("Non-isomorphs") ? "\n" : ""; // Enter to new section unless its the last section (Non-isomorphs)
            }
            output = output.TrimEnd('\r', '\n');
            Console.Write(output);
            FileIO.WriteToTextFile(
                Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName+ "\\Output_Files\\output.txt", output);
            watch.Stop();
            Console.WriteLine($"\n--------------END----------------\n** Execution Time: {watch.ElapsedMilliseconds} ms **");
        }

        /// <summary>
        /// Extract and remove the non isomorphic strings from the dictionary by referance
        /// </summary>
        /// <param name="dict">The output dictionary</param>
        public void ExtractNonIsomorphs(ref Dictionary<string, List<Tuple<string, List<string>>>> dict)
        {
            foreach (var entry in dict)
            {
                foreach (var pattern in entry.Value)
                    if (pattern.Item2.Count == 1)
                    {
                        if (dict["Non-isomorphs"].Select(x => x.Item1).Contains("words"))
                            dict["Non-isomorphs"]
                                .Find(x => x.Item1 == "words" && !x.Item2.Contains(pattern.Item2.First()))
                                ?.Item2.Add(pattern.Item2.First()); // Add word to list if does not exist already
                        else dict["Non-isomorphs"]
                                .Add(new Tuple<string, List<string>>("words", new List<string> { pattern.Item2.First() })); // else create new Tuple
                    }
                dict[entry.Key].RemoveAll(x => x.Item2.Count == 1); // Remove entry from other tuple lists
            }
        }

    }
}
