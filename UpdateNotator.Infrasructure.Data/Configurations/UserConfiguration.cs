using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;
using UpdateNotator.Domain.Core.Entries;
using UpdateNotator.Domain.Core.Topics;
using UpdateNotator.Domain.Core.Users;

namespace UpdateNotator.Infrasructure.Data.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "dbo");
            builder.HasKey(m => m.Id);

            builder.OwnsOne<Email>("Email", m =>
            {
                m.Property(p => p.EmailAddress)
                 .HasColumnName("Email");
            });

            builder.Property(m => m.Role)
                   .HasColumnName("Role")
                   .HasConversion(new EnumToNumberConverter<Roles, byte>());
        }
    }
}
