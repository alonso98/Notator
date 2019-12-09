using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text;
using UpdateNotator.Domain.Core.Entries;

namespace UpdateNotator.Infrasructure.Data.Configurations
{
    public class EntryConfiguration : IEntityTypeConfiguration<Entry>
    {
        public void Configure(EntityTypeBuilder<Entry> builder)
        {
            builder.ToTable("Entries", "dbo");
            builder.HasKey(note => note.Id);

            builder.OwnsMany<Link>("links", m =>
            {
                m.HasForeignKey("EntryId");
                m.HasKey("Id");
                m.Property("Url").IsRequired(true);
                
                m.ToTable("Links", "dbo");
            });

            //builder.Property(m => m.Links)
            //       .HasField("links")
            //       .UsePropertyAccessMode(PropertyAccessMode.Field);

            builder.Property(m => m.Type)
                   .HasColumnName("Type")
                   .HasConversion(new EnumToNumberConverter<EntryTypes, byte>());
        }
    }
}
