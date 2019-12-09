using LinqSpecs.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace UpdateNotator.Domain.Core.Entries
{
    public class EntriesByTopicIdSpecification : Specification<Entry>
    {
        private Guid topicId;
        public EntriesByTopicIdSpecification(Guid topicId)
        {
            this.topicId = topicId;
        }

        public override Expression<Func<Entry, bool>> ToExpression()
        {
            return m => m.TopicId == topicId;
        }
    }
}
