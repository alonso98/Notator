using System;
using System.Collections.Generic;
using System.Text;

namespace UpdateNotator.Domain.Core.Common.Exceptions
{
    public class InvalidInputException : ApplicationException
    {
        public InvalidInputException(string varName):base(string.Format("Invalid format of {0}", varName))
        {
        }
    }
}
