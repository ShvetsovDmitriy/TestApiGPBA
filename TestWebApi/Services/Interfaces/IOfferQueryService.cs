using TestWebApi.DTO;

namespace TestWebApi.Services.Interfaces
{
    public interface IOfferQueryService
    {
        Task<List<TopSupplierResponse>> GetTopSuppliersAsync(CancellationToken cancellationToken);
        Task<OfferResponse> SearchOffersAsync(OfferSearchRequest offerSearch, CancellationToken cancellationToken);
    }
}
