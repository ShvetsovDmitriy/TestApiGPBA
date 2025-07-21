using TestWebApi.Models;

namespace TestWebApi.Data.Seed.SeedData
{
    public static class OffersSeed
    {
        public static IEnumerable<Offer> GetPreconfiguredOffers(AppDbContext context)
        {
            var random = new Random();
            var suppliers = context.Suppliers.ToList();

            return Enumerable.Range(1, 15).Select(i => new Offer
            {
                Id = Guid.NewGuid(),
                Brand = $"Brand{(i % 5) + 1}",
                Model = $"Model{random.Next(100, 999)}",
                SupplierId = suppliers[random.Next(0, suppliers.Count)].Id,
                RegistrationDate = DateTime.UtcNow.AddDays(-random.Next(1, 30))
            });
        }
    }
}
