using System;
using System.Collections.Generic;
using UpdateNotator.Domain.Core.Common.Repository;
using UpdateNotator.Domain.Core.Topics;

namespace UpdateNotator.Domain.Core.Users
{
    public class User : BaseEntity
    {
        public virtual string UserName { get; protected set; }

        public virtual string Password { get; protected set; }

        public virtual string Salt { get; protected set; }

        public virtual Email Email { get; protected set; }

        public virtual DateTime CreatingDate { get; protected set; }

        public virtual Roles Role { get; protected set; }

        #region Static methods
        public static User Create(string email,
                                  string username,
                                  string password,
                                  string salt)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentNullException(email);

            var emailWrapper = new Email(email);

            return Create(Guid.NewGuid(), emailWrapper, username, password, salt, Roles.User);
        }

        public static User Create(Guid id,
                                  Email email,
                                  string username,
                                  string password,
                                  string salt,
                                  Roles role)
        {
            if (email == null)
                throw new ArgumentNullException("email");

            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException("username");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException("password");

            if (string.IsNullOrWhiteSpace(salt))
                throw new ArgumentNullException("salt");

            User user = new User
            {
                Id = id,
                CreatingDate = DateTime.Now,
                Email = email,
                UserName = username,
                Password = password,
                Role = role,
                Salt = salt
            };

            //todo: вызов события "создано"

            return user;
        }
        #endregion


    }
}
