using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWebApi.Data;
using TestWebApi.DTO;
using TestWebApi.Services.Interfaces;

namespace TestWebApi.Services
{
    public class SearchOfferService : ControllerBase, IOfferQueryService
    {
        private readonly AppDbContext _dbContext;

        private readonly ILogger<SearchOfferService> _logger;

        public SearchOfferService(AppDbContext dbContext, ILogger<SearchOfferService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<OfferResponse> SearchOffersAsync(OfferSearchRequest request, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Начат поиск офферов.\n " +
                "Запрос: {request.Brand}, {request.Model},{request.Supplier}", request.Brand, request.Model, request.Supplier);

            try
            {
                var query = _dbContext.Offers.AsQueryable();

                // предположим что нам не важен регистр при поиске 
                if (!string.IsNullOrWhiteSpace(request.Brand))
                {
                    _logger.LogDebug("Применен фильтр по наименованию бренда: {request.Brand}", request.Brand);
                    query = query.Where(b => b.Brand.Contains(request.Brand));
                }

                if (!string.IsNullOrWhiteSpace(request.Model))
                {
                    _logger.LogDebug("Применен фильтр по наименованию модели: {request.Model}", request.Model);
                    query = query.Where(m => m.Model.Contains(request.Model));
                }

                if (!string.IsNullOrWhiteSpace(request.Supplier))
                {
                    _logger.LogDebug("Применен фильтр по наименованию поставщика: {request.Supplier}", request.Supplier);
                    query = query.Where(s => s.Supplier.Name.Contains(request.Supplier));
                }

                var totalCount = await query.CountAsync(cancellationToken);
                _logger.LogInformation("Всего найдено строк: {totalCount} шт.", totalCount);

                // применяем пагинацию, нужна сортировка для согласованности данных
                query = query.OrderBy(x => x.Id);

                int currentPage = request.Page ?? 1;
                int currentPageSize = request.PageSize ?? 20;

                var pagedQuery = query
                    .Skip((currentPage - 1) * currentPageSize)
                    .Take(currentPageSize);

                var results = await pagedQuery.Select(o => new OfferDTO
                {
                    Id = o.Id,
                    Brand = o.Brand,
                    Model = o.Model,
                    SupplierId = o.SupplierId,
                    SupplierName = o.Supplier.Name,
                    RegistrationDate = o.RegistrationDate
                }).ToListAsync(cancellationToken);

                return new OfferResponse
                {
                    TotalCount = totalCount,
                    Offers = results
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при поиске офферов.\n " +
                     "Запрос: {request.Brand}, {request.Model},{request.Supplier}", request.Brand, request.Model, request.Supplier);
                throw;
            }
        }

        public async Task<List<TopSupplierResponse>> GetTopSuppliersAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _dbContext.Suppliers
                .Select(s => new TopSupplierResponse
                {
                    SupplierName = s.Name,
                    OffersCount = s.Offers.Count
                })
                .OrderByDescending(s => s.OffersCount)
                .Take(3)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

                _logger.LogInformation("Всего найдено строк: {result.Count} шт.", result.Count);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при поиске популярных поставщиков.");

                throw;
            }
        }
    }
}
