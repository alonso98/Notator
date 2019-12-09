using System;
using System.Threading.Tasks;
using UpdateNotator.Domain.Core.Common.Repository;
using UpdateNotator.Domain.Core.Entries;
using UpdateNotator.Domain.Core.Topics;
using UpdateNotator.Domain.Core.Users;

namespace UpdateNotator.Infrasructure.Data
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private AppContext context;

        public UnitOfWork(AppContext context,
                          IRepository<User> users,
                          IRepository<Topic> topics,
                          IRepository<Entry> entries)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.Users = users ?? throw new ArgumentNullException(nameof(users));
            this.Entries = entries ?? throw new ArgumentNullException(nameof(entries));
            this.Topics = topics ?? throw new ArgumentNullException(nameof(topics));
        }

        public IRepository<User> Users { get; }

        public IRepository<Topic> Topics { get; }

        public IRepository<Entry> Entries { get; }


        public Task Save() => context.SaveChangesAsync();

        #region Dispose
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
