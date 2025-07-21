using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TestWebApi.Models;

namespace TestWebApi.Data.Configurations
{
    public class OfferConfiguration : IEntityTypeConfiguration<Offer>
    {
        public void Configure(EntityTypeBuilder<Offer> builder)      
        {      
            builder.HasKey(o => o.Id);

            builder.Property(o => o.Brand)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.Model)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(o => o.RegistrationDate)
                .IsRequired();           

            builder.HasOne(o => o.Supplier)
                .WithMany(s => s.Offers)
                .HasForeignKey(o => o.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(o => o.Brand);
            builder.HasIndex(o => o.SupplierId);
        }
    }
}
