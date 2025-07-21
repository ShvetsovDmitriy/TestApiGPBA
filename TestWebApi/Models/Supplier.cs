using System.Text.Json.Serialization;

namespace TestWebApi.Models
{
    public class Supplier
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();
    }
}
