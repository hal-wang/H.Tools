﻿using System.Globalization;
using System.Windows.Data;

namespace H.Tools.Converter.Converters;

internal class MathConverter : IValueConverter
{
    private static readonly char[] _allOperators = ['+', '-', '*', '/', '%', '(', ')'];

    private static readonly List<string> _grouping = ["(", ")"];
    private static readonly List<string> _operators = ["+", "-", "*", "/", "%"];

    #region IValueConverter Members
    public object Convert(object value, Type targetType, object parameter, CultureInfo language)
    {
        // Parse value into equation and remove spaces
        var mathEquation = (parameter as string)!;
        mathEquation = mathEquation.Replace(" ", "");
        mathEquation = mathEquation.Replace("@Value", value.ToString(), StringComparison.InvariantCultureIgnoreCase);

        // Validate values and get list of numbers in equation
        var numbers = new List<double>();

        foreach (string s in mathEquation.Split(_allOperators))
        {
            if (s != string.Empty)
            {
                if (double.TryParse(s, out double tmp))
                {
                    numbers.Add(tmp);
                }
                else
                {
                    // Handle Error - Some non-numeric, operator, or grouping character found in string
                    throw new InvalidCastException();
                }
            }
        }

        // Begin parsing method
        EvaluateMathString(ref mathEquation, ref numbers, 0);

        // After parsing the numbers list should only have one value - the total
        return ConverterHelper.ConvertValue(numbers[0], targetType)!;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
    {
        throw new NotImplementedException();
    }
    #endregion

    // Evaluates a mathematical string and keeps track of the results in a List<double> of numbers
    private static void EvaluateMathString(ref string mathEquation, ref List<double> numbers, int index)
    {
        // Loop through each mathemtaical token in the equation
        string token = GetNextToken(mathEquation);

        while (token != string.Empty)
        {
            // Remove token from mathEquation
            mathEquation = mathEquation.Remove(0, token.Length);

            // If token is a grouping character, it affects program flow
            if (_grouping.Contains(token))
            {
                switch (token)
                {
                    case "(":
                        EvaluateMathString(ref mathEquation, ref numbers, index);
                        break;

                    case ")":
                        return;
                }
            }

            // If token is an operator, do requested operation
            if (_operators.Contains(token))
            {
                // If next token after operator is a parenthesis, call method recursively
                string nextToken = GetNextToken(mathEquation);
                if (nextToken == "(")
                {
                    EvaluateMathString(ref mathEquation, ref numbers, index + 1);
                }

                // Verify that enough numbers exist in the List<double> to complete the operation
                // and that the next token is either the number expected, or it was a ( meaning
                // that this was called recursively and that the number changed
                if (numbers.Count > (index + 1) &&
                    (double.Parse(nextToken) == numbers[index + 1] || nextToken == "("))
                {
                    switch (token)
                    {
                        case "+":
                            numbers[index] = numbers[index] + numbers[index + 1];
                            break;
                        case "-":
                            numbers[index] = numbers[index] - numbers[index + 1];
                            break;
                        case "*":
                            numbers[index] = numbers[index] * numbers[index + 1];
                            break;
                        case "/":
                            numbers[index] = numbers[index] / numbers[index + 1];
                            break;
                        case "%":
                            numbers[index] = numbers[index] % numbers[index + 1];
                            break;
                    }
                    numbers.RemoveAt(index + 1);
                }
                else
                {
                    // Handle Error - Next token is not the expected number
                    throw new FormatException("Next token is not the expected number");
                }
            }

            token = GetNextToken(mathEquation);
        }
    }

    // Gets the next mathematical token in the equation
    private static string GetNextToken(string mathEquation)
    {
        // If we're at the end of the equation, return string.empty
        if (mathEquation == string.Empty)
        {
            return string.Empty;
        }

        // Get next operator or numeric value in equation and return it
        string tmp = "";
        foreach (char c in mathEquation)
        {
            if (_allOperators.Contains(c))
            {
                return (tmp == "" ? c.ToString() : tmp);
            }
            else
            {
                tmp += c;
            }
        }

        return tmp;
    }
}
