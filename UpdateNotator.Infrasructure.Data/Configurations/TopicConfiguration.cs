using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UpdateNotator.Domain.Core.Topics;

namespace UpdateNotator.Infrasructure.Data.Configurations
{
    public class TopicConfiguration : IEntityTypeConfiguration<Topic>
    {
        public void Configure(EntityTypeBuilder<Topic> builder)
        {
            builder.HasKey(topic => topic.Id);
            builder.ToTable("Topics", "dbo");
        }
    }
}
