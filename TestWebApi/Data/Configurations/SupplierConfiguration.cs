using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TestWebApi.Models;

namespace TestWebApi.Data.Configurations
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(k => k.Id);
            builder.Property(p => p.Name).IsRequired();
        }
    }
}
