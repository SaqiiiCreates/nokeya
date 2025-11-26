using System;
using System.Text;
using System.Collections.Generic;

namespace Nokeya.Helpers
{
    /// <summary>
    /// Provides functionality for decoding classic multi-tap keypad input
    /// (Nokia-style text input) into alphabetical output.
    /// </summary>
    public static class PhoneHelper
    {
        /// <summary>
        /// Mapping of keypad digits to their corresponding characters.
        /// Repeated presses on the same digit cycle through this sequence.
        /// </summary>
        private static readonly Dictionary<char, string> PhoneButtonMapping = new()
        {
            { '0', " " },
            { '1', "&'(" },
            { '2', "ABC" },
            { '3', "DEF" },
            { '4', "GHI" },
            { '5', "JKL" },
            { '6', "MNO" },
            { '7', "PQRS" },
            { '8', "TUV" },
            { '9', "WXYZ" }
        };

        /// <summary>
        /// Decodes a string representing button presses on an old multi-tap phone keypad.
        /// Supports:
        /// - Multi-press character cycling (e.g., "44" → 'H')
        /// - Space as a pause (e.g., "222 2 22" → "CAB")
        /// - '*' as a backspace
        /// - '#' as a required terminating character
        /// </summary>
        /// <param name="input">The raw input string ending with '#'.</param>
        /// <returns>The decoded output string.</returns>
        public static string OldPhonePad(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            var output = new StringBuilder();
            var current = new StringBuilder();

            foreach (char ch in input)
            {
                switch (ch)
                {
                    case '#':
                        CommitCurrent(output, current);
                        return output.ToString();

                    case '*':
                        CommitCurrent(output, current);
                        if (output.Length > 0)
                            output.Remove(output.Length - 1, 1);
                        break;

                    case ' ':
                        CommitCurrent(output, current);
                        break;

                    default:
                        if (char.IsDigit(ch))
                        {
                            if (current.Length > 0 && current[0] != ch)
                                CommitCurrent(output, current);

                            current.Append(ch);
                        }
                        break;
                }
            }

            return output.ToString();
        }

        /// <summary>
        /// Converts the currently collected sequence of repeated digits
        /// into a single character and appends it to the output.
        /// </summary>
        private static void CommitCurrent(StringBuilder output, StringBuilder current)
        {
            if (current.Length == 0)
                return;

            char key = current[0];

            if (PhoneButtonMapping.TryGetValue(key, out string? letters))
            {
                if (key == '0')
                {
                    output.Append(' ', current.Length);
                }
                else
                {
                    int index = (current.Length - 1) % letters.Length;
                    output.Append(letters[index]);
                }
            }

            current.Clear();
        }
    }
}