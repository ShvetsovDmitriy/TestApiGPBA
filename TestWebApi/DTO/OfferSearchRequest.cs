namespace TestWebApi.DTO
{
    public class OfferSearchRequest
    {
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Supplier { get; set; }
        public int? Page { get; set; } = 1;
        public int? PageSize { get; set; } = 20;
    }
}
