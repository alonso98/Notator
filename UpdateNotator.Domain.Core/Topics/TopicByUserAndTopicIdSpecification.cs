using LinqSpecs.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace UpdateNotator.Domain.Core.Topics
{
    public class TopicByUserAndTopicIdSpecification :Specification<Topic>
    {
        private Guid userId;
        private Guid topicId;

        public TopicByUserAndTopicIdSpecification(Guid userId, Guid topicId)
        {
            this.topicId = topicId;
            this.userId = userId;
        }

        public override Expression<Func<Topic, bool>> ToExpression()
        {
            return m => m.Id == this.topicId && m.UserId == userId;
        }
    }
}
