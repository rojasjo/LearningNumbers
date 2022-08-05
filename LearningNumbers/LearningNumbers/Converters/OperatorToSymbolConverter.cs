using System;
using System.Globalization;
using LearningNumbers.Models;
using Xamarin.Forms;

namespace LearningNumbers.Converters
{
    public class OperatorToSymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Operator @operator))
            {
                return "";
            }

            switch (@operator)
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
                    return "";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}