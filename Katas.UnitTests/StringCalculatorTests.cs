using NUnit.Framework;
using Katas.Exceptions;

namespace Katas.Tests
{
    [TestFixture()]
    [Parallelizable]
    public class StringCalculatorTests
    {
        private void TestStringCalculator(string input, int result)
        {
            // Setup
            StringCalculator calculator = new StringCalculator();
            var expected = result;

            // Trigger
            var actual = calculator.Add(input);

            // Assert
            Assert.That(actual, Is.EqualTo(expected), "Values match");
        }

        [Test()]
        public void It_Should_Return_0_If_Input_Is_Empty()
        {
            TestStringCalculator("", 0);
        }

        [Test()]
        [TestCase("5", 5)]
        [TestCase("5,10", 15)]
        [TestCase("5,10,15", 30)]
        public void It_Should_Return_The_Sum_of_up_to_3_Input_Values(string input, int result)
        {
            TestStringCalculator(input, result);
        }

        [TestCase("2,2,2,2,2,2,2,2", 16)]
        public void It_Should_Return_The_Sum_of_an_unknown_amount_of_Input_Values(string input, int result)
        {
            TestStringCalculator(input, result);
        }

        [Test]
        [TestCase("1\n2,3", 6)]
        public void It_Should_Allow_For_NewLineFeeds_In_The_Input_String(string input, int result)
        {
            TestStringCalculator(input, result);
        }

        [Test]
        [TestCase("1,\n2,3", 6)]
        public void It_Should_Not_Allow_For_NewLineFeeds_Adjcent_To_Commas_In_The_Input_String(string input, int result)
        {
            Assert.Throws<EmptyNumberException>(() => TestStringCalculator(input, result));
        }

        [Test]
        [TestCase("//;\n1;2", 3)]
        [TestCase("//|\n1|2", 3)]
        public void It_Should_Allow_For_Custom_delimiter_In_The_Input_String(string input, int result)
        {
            TestStringCalculator(input, result);
        }

        [Test]
        [TestCase("5,-2", "Negatives not allowed: [-2]")]
        [TestCase("10,-2,-4", "Negatives not allowed: [-2,-4]")]
        public void It_Should_Not_Allow_For_Negative_Numbers_In_The_Input_String(string input, string errorMessage)
        {
            Assert.Throws<NegativeNumberException>(() => TestStringCalculator(input, 0), errorMessage);
        }
    }
}