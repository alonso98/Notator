using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UpdateNotator.Domain.Core.Common.Exceptions;

namespace UpdateNotator.Domain.Core.Users
{
    public class Email
    {
        private const string template =
            @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";

        public Email()
        {

        }

        public Email(string email)
        {
            Regex regex = new Regex(template);
            if (email == null)
                throw new ArgumentNullException(nameof(email));
            if (!regex.IsMatch(email))
                throw new InvalidEmailException();

            this.EmailAddress = email;
        }

        public string EmailAddress { get; protected set; }

        public override string ToString()
        {
            return EmailAddress;
        }
    }

    public class InvalidEmailException : InvalidInputException
    {
        public InvalidEmailException():base("Email")
        {

        }
    }
}
