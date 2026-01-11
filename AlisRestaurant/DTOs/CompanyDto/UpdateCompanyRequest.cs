using AlisRestaurant.Data.Entities;
//using AlisRestaurant.Data.Entities.WarehouseAndStock;

namespace AlisRestaurant.DTOs.CompanyDto;

public class UpdateCompanyRequest
{
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

}
