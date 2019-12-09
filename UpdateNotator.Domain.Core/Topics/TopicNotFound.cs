using System;
using System.Collections.Generic;
using System.Text;
using UpdateNotator.Domain.Core.Common.Exceptions;

namespace UpdateNotator.Domain.Core.Topics
{
    public class TopicNotFoundException : NotFoundException
    {
        public TopicNotFoundException() : base("Topic")
        {
        }
    }
}
