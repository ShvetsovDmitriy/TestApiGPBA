using TestWebApi.DTO;

namespace TestWebApi.Services.Interfaces
{
    public interface IOfferCreationService
    {
        Task<OfferDTO> CreateOfferAsync(CreateOfferDTO offer, CancellationToken cancellationToken);
    }
}
