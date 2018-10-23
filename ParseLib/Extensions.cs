using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseLib
{
    public static class Extensions
    {
        public static bool IsAlphaNumeric(this char c)
        {
            return IsAlpha(c) || IsNumeric(c);
        }

        public static bool IsWhiteSpace(this char c)
        {
            return string.IsNullOrWhiteSpace(c + "");
        }

        public static bool IsNumeric(this char c)
        {
            return c >= 48 && c <= 57;
        }

        public static IEnumerable<char> ParseCharArray(this StringReader reader)
        {
            List<char> chars = new List<char>();
            if (reader.Next() != '(')
                throw new Exception("Array not starting with '(' " + reader);
            while (true)
            {
                chars.Add(reader.Next());
                if (reader.Peek() == ',')
                    reader.Next();
                else break;
            }
            if (reader.Next() != ')')
                throw new Exception("Array not ending with ')' " + reader);
            return chars;
        }

        public static IEnumerable<int> ParseIntArray(this StringReader reader)
        {
            List<int> chars = new List<int>();
            if (reader.Next() != '(')
                throw new Exception("Array not starting with '(' " + reader);
            while (true)
            {
                chars.Add(int.Parse(reader.ReadNumericString()));
                if (reader.Peek() == ',')
                    reader.Next();
                else break;
            }
            if (reader.Next() != ')')
                throw new Exception("Array not ending with ')' " + reader);
            return chars;
        }

        public static bool IsAlpha(this char c)
        {
            return IsUpperCaseAlpha(c) || IsLowerCaseAlpha(c);
        }

        public static bool IsUpperCaseAlpha(this char c)
        {
            return c >= 65 && c <= 90 || (c == 'Ä' || c == 'Ö' || c == 'Ü' || c == '_');
        }

        public static bool IsLowerCaseAlpha(this char c)
        {
            return c >= 97 && c <= 122 || (c == 'ä' || c == 'ö' || c == 'ü' || c == '_');
        }
    }
}
