using System.Text.Json.Serialization;

namespace TestWebApi.Models
{
    public class Offer
    {
        public Guid Id { get; set; }
        public string Brand { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public Guid SupplierId { get; set; }

        [JsonIgnore]
        public virtual Supplier Supplier { get; set; } = null!;

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;
    }
}
