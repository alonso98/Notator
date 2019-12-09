using System;
using System.Collections.Generic;
using System.Text;
using UpdateNotator.Domain.Core.Common.Exceptions;

namespace UpdateNotator.Domain.Core.Users
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException() : base("User") { }
    }
}
