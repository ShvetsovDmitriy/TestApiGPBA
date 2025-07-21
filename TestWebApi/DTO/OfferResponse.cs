namespace TestWebApi.DTO
{
    public class OfferResponse
    {
        public int TotalCount { get; set; }       
        public List<OfferDTO> Offers { get; set; } = new List<OfferDTO>();
    }
}
