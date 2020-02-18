using System;
using System.Collections.Generic;
using System.Text;

namespace Parking.Core.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Check if string is NOT empty, null or white space
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsValidString(this string text) => !string.IsNullOrEmpty(text) && !string.IsNullOrWhiteSpace(text);

    }
}
