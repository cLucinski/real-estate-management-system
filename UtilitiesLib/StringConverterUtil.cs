using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesLib
{
    /// <summary>
    /// A class with methods that convert a string to different numeric values.
    /// </summary>
    public class StringConverterUtil
    {
        //Constructor
        public StringConverterUtil()
        {

        }

        /// <summary>
        /// Convert a string to an integer.
        /// </summary>
        /// <param name="input">String of an integer.</param>
        /// <returns>An integer: the number if input was valid, 
        /// or -1 if there was an error with the input.</returns>
        public static int ConvertStringToInt(string input)
        {
            input = input.Trim();

            if (int.TryParse(input, out int toReturn))
            {
                return toReturn;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Convert a string to an integer and make sure it's between lowLimit and highLimit.
        /// </summary>
        /// <param name="input">String of an integer.</param>
        /// <param name="lowLimit"></param>
        /// <param name="highLimit"></param>
        /// <returns>An integer: the number if input was valid and between the limits, 
        /// or -1 if there was an error with the input.</returns>
        public static int ConvertStringToInt(string input, int lowLimit, int highLimit)
        {
            input = input.Trim();

            if (int.TryParse(input, out int toReturn))
            {
                if (toReturn > lowLimit && toReturn < highLimit)
                {
                    return toReturn;
                }
            }

            return -1;
        }

        /// <summary>
        /// Convert a string to a decimal number.
        /// </summary>
        /// <param name="input">String of a decimal number.</param>
        /// <returns>A decimal number: the number if input was valid, 
        /// or -1 if there was an error with the input.</returns>
        public static decimal ConvertStringToDecimal(string input)
        {
            input = input.Trim();

            if (decimal.TryParse(input, out decimal toReturn))
            {
                return toReturn;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Convert a string to a decimal number and make sure it's between lowLimit and highLimit.
        /// </summary>
        /// <param name="input">String of a decimal number.</param>
        /// <param name="lowLimit"></param>
        /// <param name="highLimit"></param>
        /// <returns>String of a decimal number.</param>
        /// <returns>A decimal number: the number if input was valid, 
        /// or -1 if there was an error with the input.</returns>
        public static decimal ConvertStringToDecimal(string input, decimal lowLimit, decimal highLimit)
        {
            input = input.Trim();

            if (decimal.TryParse(input, out decimal toReturn))
            {
                if (toReturn > lowLimit && toReturn < highLimit)
                {
                    return toReturn;
                }
            }

            return -1;
        }

    }
}
