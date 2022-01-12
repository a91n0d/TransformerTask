using System;
using System.Collections.Generic;
using System.Globalization;

namespace TransformerToWords
{
    /// <summary>
    /// Implements transformer class.
    /// </summary>
    public class Transformer
    {
        private readonly Dictionary<double, string> transformEx = new Dictionary<double, string>()
            {
                { double.NaN, "Not a Number" }, { double.NegativeInfinity, "Negative Infinity" },
                { double.PositiveInfinity, "Positive Infinity" }, { double.Epsilon, "Double Epsilon" },
            };

        private readonly Dictionary<char, string> transformToWords = new Dictionary<char, string>()
            {
                { '-', "minus " }, { '0', "zero " },
                { '1', "one " }, { '2', "two " },
                { '3', "three " }, { '4', "four " },
                { '5', "five " }, { '6', "six " },
                { '7', "seven " }, { '8', "eight " },
                { '9', "nine " }, { 'E', "E " },
                { '.', "point " }, { '+', "plus " },
            };

        /// <summary>
        /// Converts number's digital representation into words.
        /// </summary>
        /// <param name="number">Number to convert.</param>
        /// <returns>Words representation.</returns>
        public string TransformToWords(double number)
        {
            if (this.transformEx.ContainsKey(number))
            {
                return this.transformEx[number];
            }

            string numberToStr = number.ToString(CultureInfo.InvariantCulture);
            string words = string.Empty;
            foreach (char ch in numberToStr)
            {
                if (this.transformToWords.ContainsKey(ch))
                {
                    words = string.Concat(words, this.transformToWords[ch]);
                }
            }

            words = words[..1].ToUpper(CultureInfo.InvariantCulture) + words[1..];
            return words.Trim();
        }

        /// <summary>
        /// Transforms each element of source array into its 'word format'.
        /// </summary>
        /// <param name="source">Source array.</param>
        /// <returns>Array of 'word format' of elements of source array.</returns>
        /// <exception cref="ArgumentNullException">Thrown when array is null.</exception>
        /// <exception cref="ArgumentException">Thrown when array is empty.</exception>
        /// <example>
        /// new[] { 2.345, -0.0d, 0.0d, 0.1d } => { "Two point three four five", "Minus zero", "Zero", "Zero point one" }.
        /// </example>
        public string[] Transform(double[] source)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (source.Length == 0)
            {
                throw new ArgumentException("Array is empty.", nameof(source));
            }

            string[] transform = new string[source.Length];
            for (int i = 0; i < source.Length; i++)
            {
                transform[i] = this.TransformToWords(source[i]);
            }

            return transform;
        }
    }
}
