using System;
using System.Collections.Generic;
using System.Text;

namespace UpdateNotator.Domain.Core.Common.Repository
{
    public class BaseEntity
    {
        public Guid Id { get; protected set; }
    }
}
