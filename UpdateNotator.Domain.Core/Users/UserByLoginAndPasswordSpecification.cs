using LinqSpecs.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace UpdateNotator.Domain.Core.Users
{
    public class UserByPasswordSpecification : Specification<User>
    {
        private string password;
        public UserByPasswordSpecification(string password)
        {
            this.password = password;
        }
        public override Expression<Func<User, bool>> ToExpression()
        {
            return m => m.Password == password;
        }
    }
}
