using System.Collections.Generic;
using System.Linq;
using Katas.Exceptions;

namespace Katas
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            var values = ParseIndividualValuesFromString(numbers);
            var negatives = values.Where(number => number < 0).ToList();
            if (negatives.Count > 0)
            {
                throw new NegativeNumberException(negatives.ToArray());
            }
            return values.Sum();
        }

        private IEnumerable<int> ParseIndividualValuesFromString(string numbers)
        {
            List<int> results = new List<int>();
            if (!string.IsNullOrEmpty(numbers))
            {
                var delimiter = ',';
                if (numbers.IndexOf("//")==0)
                {
                    delimiter = numbers.Substring(2, 1)[0];
                    //copy only the values after parsing out custom delimiter
                    numbers = numbers.Substring(4);
                }
                var filteredValues = numbers.Replace('\n', delimiter);
                var values = filteredValues.Split(delimiter);
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
