using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseLib
{
    public class StringReader
    {
        public readonly string Text;
        public int Position { get; private set; }

        /// <summary>
        /// Determines if the StringReader is pointing before the last character
        /// </summary>
        public bool IsAtEnd => Text.Length == Position;

        /// <summary>
        /// Determines if the StringReader is pointing before the last character
        /// </summary>
        public bool IsOneBeforeEnd => Text.Length - 1 == Position;

        /// <summary>
        /// Determines if the StringReader is pointing to the first character
        /// </summary>
        public bool IsAtStart => 0 == Position;

        public StringReader(string text)
        {
            Position = 0;
            if (text == null)
                throw new ArgumentNullException(nameof(text));
            Text = (string)text.Clone();
        }

        /// <summary>
        /// Deletes all whitespace characters till the next non whitespace character
        /// </summary>
        public void ConsumeWhiteSpace()
        {
            while (Peek().IsWhiteSpace() && !IsAtEnd)
                Next();
        }

        /// <summary>
        /// Reads a string that can begin with a '"' and end with a '"' but does not have to
        /// '"' can be escaped with a leading '\'
        /// </summary>
        /// <returns>The string in the '"' quotes without the '"' quotes or the string till the next whitespace character</returns>
        public string ReadMaybeQuotedString()
        {
            if (Peek() == '"')
                return ReadQuotedString();
            else return ReadAlphaNumericString();
        }

        /// <summary>
        /// Reads a string beginning with a '"' and ending with a '"'
        /// '"' can be escaped with a leading '\'
        /// </summary>
        /// <exception cref="UnexpectedCharException">Is thrown when the next char is not a '"' or when there is no '"' to end the string</exception>
        /// <returns>The string in the '"' quotes without the '"' quotes</returns>
        public string ReadQuotedString()
        {
            if (Peek() != '"')
                throw new UnexpectedCharException(this, nameof(ReadQuotedString) + " Expected '\"' ");
            Next();
            string str = "";
            while (Peek() != '"' && !IsAtEnd)
            {
                char current = Next();
                if (current == '\\' && Peek() == '"') // Skip Escaped '"'
                    str += Next();
                else str += current;
            }
            Next();
            return str;
        }

        /// <summary>
        /// Gets the next character from the stream
        /// </summary>
        /// <returns>Either the next character or '\0' if the stream is empty or at the end</returns>
        public char Next()
        {
            if (IsAtEnd)
                return '\0';
            char character = Text[Position++];
            return character;
        }

        /// <summary>
        /// Gets the next character from the stream without consuming it
        /// </summary>
        /// <returns>Either the next character or '\0' if the stream is empty or at the end</returns>
        public char Peek()
        {
            if (IsAtEnd)
                return '\0';
            return Text[Position];
        }

        /// <summary>
        /// Sets the position in the stream
        /// <exception cref="IndexOutOfRangeException">Is thrown when the index is negative or greater then the length of the string</exception>
        /// </summary>
        public void Seek(int position)
        {
            if (position < 0 || position > Text.Length)
                throw new IndexOutOfRangeException("Invalid position in stream");
            Position = position;
        }

        /// <summary>
        /// Gets the rest of the string from the current Position without consuming
        /// </summary>
        /// <returns></returns>
        public string PeekRestOfString()
        {
            return Text.Substring(Position);
        }

        /// <summary>
        /// Reads a string till the end or till the next non alpha character
        /// </summary>
        /// <returns>Either the red string or '\0' if there is nothing to read or the next character is not an alpha character</returns>
        public string ReadAlphaString()
        {
            string result = "";
            char c;
            while (true)
            {
                c = Peek();
                if (!c.IsAlpha())
                    break;
                Next();
                result += c;
            }
            return result;
        }

        /// <summary>
        /// Reads a string till the end or till the next non numeric character
        /// </summary>
        /// <returns>Either the red string or '\0' if there is nothing to read or the next character is not a numeric character</returns>
        public string ReadNumericString()
        {
            string result = "";
            char c;
            while (true)
            {
                c = Peek();
                if (!c.IsNumeric())
                    break;
                Next();
                result += c;
            }
            return result;
        }

        /// <summary>
        /// Reads a string till the end or till the next non alpha-numeric character
        /// </summary>
        /// <returns>Either the red string or '\0' if there is nothing to read or the next character is not an alpha-numeric character</returns>
        public string ReadAlphaNumericString()
        {
            string result = "";
            char c;
            while (true)
            {
                c = Peek();
                if (!c.IsAlphaNumeric())
                    break;
                Next();
                result += c;
            }
            return result;
        }

        public override string ToString()
        {
            return "At Position: " + Position + " Current char: " + Peek() + " Rest of string: \"" + PeekRestOfString() + "\"";
        }
    }
}
