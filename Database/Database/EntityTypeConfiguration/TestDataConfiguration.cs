using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntityTypeConfiguration;

public class TestDataConfiguration : IEntityTypeConfiguration<TestData>
{

    public void Configure(EntityTypeBuilder<TestData> builder)
    {
        builder.Property(c => c.Temperature).ValueGeneratedOnAdd();
    }

}
