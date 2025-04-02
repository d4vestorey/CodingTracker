using System.Globalization;
using Spectre.Console;

namespace CodingTracker
{
    public class Validation
    {
        private const string DateFormat = "yyyy-MM-dd HH:mm:ss";

        public static ValidationResult DateTimeValidate(string input)
        {
            return DateTime.TryParseExact(input, DateFormat,
                CultureInfo.InvariantCulture, DateTimeStyles.None, out _)
                ? ValidationResult.Success()
                : ValidationResult.Error($"[red]Invalid date format! Please use {DateFormat}.[/]");
        }
    }
}