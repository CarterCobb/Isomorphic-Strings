using System;

namespace Isomorphic_Strings
{
    public class IO
    {
        /// <summary>
        /// Gets validated string from user input in the console
        /// </summary>
        /// <param name="message">Promt to user</param>
        /// <returns>Validated string value from user input</returns>
        public static string GetConsoleString(string message)
        {
            string result;
            do
            {
                Console.Write(message);
                result = Console.ReadLine();
            } while (result == null);
            return result;
        }
    }
}
