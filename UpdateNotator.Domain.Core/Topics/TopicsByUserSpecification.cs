using LinqSpecs.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace UpdateNotator.Domain.Core.Topics
{
    public class TopicsByUserSpecification : Specification<Topic>
    {
        private Guid guid;


        public TopicsByUserSpecification(Guid id)
        {
            this.guid = id;
        }

        public override Expression<Func<Topic, bool>> ToExpression()
        {
            return m => m.UserId == this.guid;
        }
    }
}
