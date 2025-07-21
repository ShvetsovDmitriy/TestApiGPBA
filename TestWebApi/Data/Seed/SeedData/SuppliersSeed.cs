using TestWebApi.Models;

namespace TestWebApi.Data.Seed.SeedData
{
    public static class SuppliersSeed
    {
        public static IEnumerable<Supplier> GetPreconfiguredSuppliers()
        {
            return new List<Supplier>
            {
                new() { Id = Guid.NewGuid(), Name = "AutoTech" },
                new() { Id = Guid.NewGuid(), Name = "CarExpert" },
                new() { Id = Guid.NewGuid(), Name = "AutoProm" },
                new() { Id = Guid.NewGuid(), Name = "ReactCar" },
                new() { Id = Guid.NewGuid(), Name = "CarPro" }
            };
        }        
    }
}
