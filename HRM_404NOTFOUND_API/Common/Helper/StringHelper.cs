namespace SalaryManagement.Api.Common.Helper
{
    public static class StringHelper
    {
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string ToTitleCase(this string value)
        {
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
        }

        public static string RemoveSpecialCharacters(this string value)
        {
            string result = value;
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
                result = result.Replace(c, '_');
            foreach (char c in System.IO.Path.GetInvalidPathChars())
                result = result.Replace(c, '_');

            return result;
        }
    }
}
