using System;
using System.Collections.Generic;
using System.Text;

namespace UpdateNotator.Domain.Core.Common.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name):base(string.Format("{0} was not found.",name))
        {

        }
    }
}
