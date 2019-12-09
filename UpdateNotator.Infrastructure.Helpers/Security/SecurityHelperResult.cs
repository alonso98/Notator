using System;
using System.Collections.Generic;
using System.Text;

namespace UpdateNotator.Infrastructure.Helpers.Security
{
    public class SecurityHelperResult
    {
        public string Password { get; }

        public string Salt { get; }

        internal SecurityHelperResult(string password, string salt)
        {
            Password = password;
            Salt = salt;
        }
    }
}
