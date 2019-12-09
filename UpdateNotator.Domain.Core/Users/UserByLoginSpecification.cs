using LinqSpecs.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace UpdateNotator.Domain.Core.Users
{
    public class UserByLoginSpecification : Specification<User>
    {
        private string username;

        public UserByLoginSpecification(string username)
        {
            this.username = username ?? throw new ArgumentNullException(nameof(username));
        }
        public override Expression<Func<User, bool>> ToExpression()
        {
            return m => m.UserName == this.username;
        }
    }
}
