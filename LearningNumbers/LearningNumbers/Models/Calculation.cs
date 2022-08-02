using System;

namespace LearningNumbers.Models
{
    public class Calculation
    {
        public int First { get; set; }
        
        private int second;
        
        public int Second
        {
            get => second;

            set
            {
                if (Operator == Operator.Division && value == 0)
                    throw new DivideByZeroException();
                second = value;
            }
        }

        public Operator Operator { get; private set; }

        public String Symbol => GetSymbol();
        public int GivenResult { get; set; }

        public int Attempts { get; private set; }

        public Calculation(Operator operatore)
        {
            Operator = operatore;
        }

        public int Calculate()
        {
            Attempts++;

            switch (Operator)
            {
                case Operator.Sum:
                    return First + second;
                case Operator.Subtraction:
                    return First - second;
                case Operator.Multiplication:
                    return First * second;
                case Operator.Division:
                    return First / second;
                default:
                    throw new InvalidOperationException("The given operator is not valid");
            }
        }

        private string GetSymbol()
        {
            switch (Operator)
            {
                case Operator.Sum:
                    return "+";
                case Operator.Subtraction:
                    return "-";
                case Operator.Multiplication:
                    return "×";
                case Operator.Division:
                    return "÷";
                default:
                    throw new InvalidOperationException("The given operator is not valid");
            }
        }
    }
}
