using System;
using System.Collections.Generic;
using System.Text;

namespace ParseLib
{
    public class UnexpectedCharException : Exception
    {
        public StringReader StringReader { get; private set; }

        public UnexpectedCharException(StringReader stringReader, string message = "", Exception innerException = null)
            :base(message + stringReader, innerException)
        {
            this.StringReader = StringReader;
        }
    }
}
