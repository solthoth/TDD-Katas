using System;

namespace Katas.Exceptions
{
    public class NegativeNumberException : ArgumentException
    {
        public NegativeNumberException(int[] numbers) : base($"Negatives not allowed: [{numbers.toString()}]")
        {
            NegativeNumbers = numbers;
        }

        public int[] NegativeNumbers { get; private set; }
    }

    public static class NegativeNumberExceptionsHelper
    {
        public static string toString(this int[] self)
        {
            string result = "";
            for (var i = 0; i < self.Length; i++)
            {
                if (i < self.Length - 1)
                    result += self[i].ToString() + ",";
                else
                    result += self[i].ToString();
            }
            return result;
        }
    }
}
