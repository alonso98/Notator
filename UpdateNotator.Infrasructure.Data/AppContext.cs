using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UpdateNotator.Domain.Core.Entries;
using UpdateNotator.Domain.Core.Topics;
using UpdateNotator.Domain.Core.Users;
using UpdateNotator.Infrasructure.Data.Configurations;

namespace UpdateNotator.Infrasructure.Data
{
    public class AppContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Entry> Entries { get; set; }

        public DbSet<Topic> Topics { get; set; }


        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;" +
                                        "Initial Catalog=UpdateNotatorDb;" +
                                        "Integrated Security=True;" +
                                        "Connect Timeout=30;" +
                                        "Encrypt=False;" +
                                        "TrustServerCertificate=False;" +
                                        "ApplicationIntent=ReadWrite;" +
                                        "MultiSubnetFailover=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new TopicConfiguration());
            modelBuilder.ApplyConfiguration(new EntryConfiguration());
        }
    }
}
