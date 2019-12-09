using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UpdateNotator.Domain.Core.Common.Repository;
using UpdateNotator.Domain.Core.Topics;

namespace UpdateNotator.Domain.Core.Entries
{
    public class Entry : BaseEntity
    {
        private List<Link> links = new List<Link>();

        public virtual string Text { get; protected set; }

        public virtual string Head { get; protected set; }

        public virtual Guid TopicId { get; protected set; }

        public virtual DateTime CreationTime { get; protected set; }

        public virtual EntryTypes Type { get; protected set; }

        public virtual ReadOnlyCollection<Link> Links { get { return links.AsReadOnly(); } }

        #region Static methods
        public static Entry Create(Guid id,
                                  string head,
                                  string text,
                                  Guid topicId,
                                  EntryTypes type)
        {
            var note = new Entry
            {
                Id = id,
                Head = head,
                Text = text,
                TopicId = topicId,
                Type = type,
                CreationTime = DateTime.Now
            };

            return note;
        }

        public static Entry Create(string head,
                                  string text,
                                  Guid topicId,
                                  EntryTypes type)
        {
            return Create(Guid.NewGuid(), head, text, topicId, type);
        }
        #endregion

        #region Public methods
        public void AddLink(string url, string name = null)
        {
            var link = new Link(url, name);
            links.Add(link);
        }
        #endregion
    }
}
