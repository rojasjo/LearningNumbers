namespace LearningNumbers.Services
{
    public interface IDigitService
    {
        int? RemoveLastDigit(int? number);

        int? AppendDigits(int? number, string digits);
    }
}