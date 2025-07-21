using Microsoft.AspNetCore.Mvc;
using TestWebApi.DTO;
using TestWebApi.Services.Interfaces;

namespace TestWebApi.Controllers
{
    [ApiController]
    [Route("api/offers")]
    public class OffersController : ControllerBase
    {
        private readonly IOfferCreationService _offerCreationService;
        private readonly IOfferQueryService _offerSearchService;     

        public OffersController(
            IOfferCreationService offerCreationService,
            IOfferQueryService offerSearchService)
        {
            _offerCreationService = offerCreationService;
            _offerSearchService = offerSearchService;      
        }
       
        [HttpPost("create-supplier")]
        [ProducesResponseType(typeof(OfferDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateOffer(CreateOfferDTO offer, CancellationToken cancellationToken)
        {
            var offerDTO = await _offerCreationService.CreateOfferAsync(offer, cancellationToken);

            if (offerDTO is null) 
            {
                return NotFound();
            }

            return Ok(offerDTO);
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(OfferResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SearchOffers([FromQuery] OfferSearchRequest request, CancellationToken cancellationToken)
        {
            var result = await _offerSearchService.SearchOffersAsync(request, cancellationToken);

            return Ok(result);
        }

        [HttpGet("top-suppliers")]
        [ProducesResponseType(typeof(List<TopSupplierResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTopSuppliers(CancellationToken cancellationToken)
        {
            var result = await _offerSearchService.GetTopSuppliersAsync(cancellationToken);

            if (result is null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
