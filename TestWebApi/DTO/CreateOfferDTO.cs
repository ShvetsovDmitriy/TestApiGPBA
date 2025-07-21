using System.ComponentModel.DataAnnotations;

namespace TestWebApi.DTO
{
    public class CreateOfferDTO
    {
        [Required(ErrorMessage = "Заполните поле \"Brand\"")]
        public required string Brand { get; set; }

        [Required(ErrorMessage = "Заполните поле \"Model\"")]
        public required string Model { get; set; }
   
        [Required(ErrorMessage = "Заполните поле \"SupplierName\"")]
        public required string SupplierName { get; set; }
    }
}
