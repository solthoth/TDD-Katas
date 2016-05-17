using System;
using System.Collections.Generic;
using System.Linq;
using Katas.Exceptions;

namespace Katas
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            var delimiters = ParseDelimiters(numbers);
            numbers = RemoveOptions(numbers);
            var values = ParseValuesFromString(numbers, delimiters);
            if (values.Where(value=>value<0).Any())
            {
                throw new NegativeNumberException(values.ToArray());
            }
            return values.Where(value=>value<1000).Sum();
        }

        private string RemoveOptions(string numbers)
        {
            bool multiValueDelimiter;
            if (UserDefinedDelimiter(numbers, out multiValueDelimiter))
            {
                if (multiValueDelimiter)
                    numbers = numbers.Remove(0, numbers.IndexOf("]\n") + 2);
                else
                    numbers = numbers.Remove(0, 4);
            }
            return numbers;
        }

        private bool UserDefinedDelimiter(string input, out bool multilength)
        {
            var result = input.StartsWith("//");
            multilength = input.StartsWith("//[");
            return result;
        }

        private List<string> ParseDelimiters(string input)
        {
            var delimiters = new List<string>();
            bool multiValueDelimiter;
            if (UserDefinedDelimiter(input, out multiValueDelimiter))
            {
                input = input.TrimStart('/', '/');
                if (multiValueDelimiter)
                {
                    do
                    {
                        var delimiter = input.TrimStart('[');
                        delimiter = delimiter.Substring(0, input.IndexOf(']') - 1);
                        delimiters.Add(delimiter);
                        input = input.Remove(0, input.IndexOf("]") + 1);
                    } while (input.IndexOf("[") >= 0);
                }
                else
                {
                    delimiters.Add(input.Substring(0, 1));
                }
            }
            else
            {
                delimiters.Add(",");
                delimiters.Add("\n");
            }
            return delimiters;
        }
        private IEnumerable<int> ParseValuesFromString(string numbers, List<string> delimiters = null)
        {
            List<int> results = new List<int>();
            if (!string.IsNullOrEmpty(numbers))
            {
                if (delimiters == null)
                    delimiters = new List<string> { "," , "\n" };
                if (!delimiters.Contains("\n"))
                    delimiters.Add("\n");                    
                var values = numbers.Split(delimiters.ToArray(), StringSplitOptions.None);
                foreach (var value in values)
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        throw new EmptyNumberException();
                    }
                    int integer;
                    int.TryParse(value, out integer);
                    results.Add(integer);
                }
            }
            return results;
        }
    }
}
