using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWebApi.Data;
using TestWebApi.DTO;
using TestWebApi.Models;
using TestWebApi.Services.Interfaces;

namespace TestWebApi.Services
{
    public class CreationOfferService : ControllerBase, IOfferCreationService
    {
        private readonly AppDbContext _dbContext;

        private readonly ILogger<CreationOfferService> _logger;

        public CreationOfferService(AppDbContext dbContext, ILogger<CreationOfferService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<OfferDTO> CreateOfferAsync(CreateOfferDTO offer, CancellationToken cancellationToken)
        {
            if (offer is null)
            {
                throw new ArgumentNullException(nameof(offer));
            }
            // поиск по наимнование, а не по ID для удобства пользователя
            var supplier = await _dbContext.Suppliers
               .FirstOrDefaultAsync(s => s.Name == offer.SupplierName);

            if (supplier is null)
            {
                _logger.LogWarning("Поставщик с наименование {offer.SupplierName} не найден", offer.SupplierName);
                throw new KeyNotFoundException($"Поставщик с наименование {offer.SupplierName} не найден");
            }
            // проверка на дубли
            bool offerExists = await _dbContext.Offers
                .AnyAsync(o =>
                o.Brand == offer.Brand &&
                o.Model == offer.Model &&
                o.SupplierId == supplier.Id);

            if (offerExists)
            {
                _logger.LogWarning($"Оффер с указанными параметрами уже существует");
                throw new InvalidOperationException("Оффер с указанными параметрами уже существует");
            }
            try
            {
                var entity = new Offer
                {
                    Brand = offer.Brand.Trim(),
                    Model = offer.Model.Trim(),
                    SupplierId = supplier.Id,                    
                    RegistrationDate = DateTime.UtcNow
                };

                _dbContext.Offers.Add(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                await _dbContext.Entry(entity)
                   .Reference(o => o.Supplier)
                   .LoadAsync(cancellationToken);

                _logger.LogInformation("Создан оффер с ID: {offer.Id}, Brand: {offer.Brand}, Model: {offer.Model}", 
                    entity.Id, entity.Brand, entity.Model);
                return new OfferDTO
                {
                    Id = entity.Id,
                    Brand = entity.Brand,
                    Model = entity.Model,
                    SupplierId = entity.SupplierId,
                    SupplierName = entity.Supplier.Name,
                    RegistrationDate = entity.RegistrationDate
                };

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Ошибка на уровне базы данных при создании оффера");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Непредвиденная ошибка при создании оффера для {offer.Brand}/{offer.Model}", offer.Brand, offer.Model);
                throw;
            }
        }
      
    }
}
