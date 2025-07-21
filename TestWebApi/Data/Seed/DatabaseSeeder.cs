using TestWebApi.Data.Seed.SeedData;

namespace TestWebApi.Data.Seed
{
    public static class DatabaseSeeder
    {
        public static void Seed(this AppDbContext context)
        {           
            if (!context.Suppliers.Any())
            {
                context.Suppliers.AddRange(SuppliersSeed.GetPreconfiguredSuppliers());
                context.SaveChanges();
            }

            if (!context.Offers.Any())
            {
                context.Offers.AddRange(OffersSeed.GetPreconfiguredOffers(context));
                context.SaveChanges();
            }
        }
    }
}
