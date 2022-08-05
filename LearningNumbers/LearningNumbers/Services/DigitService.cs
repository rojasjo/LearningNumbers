namespace LearningNumbers.Services
{
    public class DigitService : IDigitService
    {
        public int? AppendDigits(int? number, string digits)
        {
            var currentValue = number + digits;

            if (int.TryParse(currentValue, out var parsedValue))
            {
                number = parsedValue;
            }

            return number;
        }

        public int? RemoveLastDigit(int? number)
        {
            if (number == null)
            {
                return null;
            }

            var numberString = number.ToString();
            var lastIndex = numberString.Length - 1;
            numberString = numberString.Remove(lastIndex);
            
            if (int.TryParse(numberString, out var parsedValue))
            {
                number = parsedValue;
            }
            else
            {
                number = null;
            }

            return number;
        }
    }
}