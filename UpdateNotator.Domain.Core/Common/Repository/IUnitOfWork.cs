using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UpdateNotator.Domain.Core.Entries;
using UpdateNotator.Domain.Core.Topics;
using UpdateNotator.Domain.Core.Users;

namespace UpdateNotator.Domain.Core.Common.Repository
{
    public interface IUnitOfWork
    {
        IRepository<User> Users { get; }
        IRepository<Topic> Topics { get; }
        IRepository<Entry> Entries { get; }

        Task Save();
    }
}
