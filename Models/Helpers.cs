using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace SmartShopping.Models
{
    public static class Helpers
    {
        public static string Simplify(string val)
        {
            return RemoveDiacritics(val.ToLower());
        }

        public static string Format(string val)
        {
            return Regex.Replace(val.ToLower(), @"\s+", " ").FirstCharToUpper();
        }

        public static string RemoveDiacritics(string stIn)
        {
            string stFormD = stIn.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }

        public static string FirstCharToUpper(this string input)
        {
            return input switch
            {
                null => throw new ArgumentNullException(nameof(input)),
                "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
                _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
            };
        }
    }
}
