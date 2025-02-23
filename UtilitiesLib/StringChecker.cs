using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesLib
{
    /// <summary>
    /// A helper class to check a string for expected conditions.
    /// </summary>
    public class StringChecker
    {
        public StringChecker()
        {

        }

        /// <summary>
        /// Return whether or not given string is empty or has only numbers.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEmptyOrNumber(string input)
        {
            bool isValid = true;
            input = input.Trim();

            if (!string.IsNullOrEmpty(input))
            {
                isValid = double.TryParse(input, out _);
            }

            return isValid;
        }

        /// <summary>
        /// Return whether or not given string is empty or has only letters.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsEmptyOrLetters(string input)
        {
            bool isValid = true;
            input = input.Trim();

            if (!string.IsNullOrEmpty(input))
            {
                isValid = input.All(Char.IsLetter);
            }

            return isValid;
        }
    }
}
