using System;
using System.Collections.Generic;
using UpdateNotator.Domain.Core.Common.Repository;
using UpdateNotator.Domain.Core.Entries;

namespace UpdateNotator.Domain.Core.Topics
{
    public class Topic : BaseEntity
    {
        public virtual string Name { get; protected set; }

        public virtual Guid UserId { get; protected set; }

        #region Static methods
        public static Topic Create(Guid id, string name, Guid userId)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException("name");

            var topic = new Topic
            {
                Id = id,
                Name = name,
                UserId = userId
            };
            return topic;
        }

        public static Topic Create(string name, Guid userId)
        {
            return Create(Guid.NewGuid(), name, userId);
        }
        #endregion

        #region Methods
        public virtual void ChangeName(string topicName)
        {
            this.Name = topicName ?? throw new ArgumentNullException(nameof(topicName));
        }
        #endregion
    }
}
