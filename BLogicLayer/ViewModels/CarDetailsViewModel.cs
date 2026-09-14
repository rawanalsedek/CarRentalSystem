using DataAccessLayer.Models;

namespace BLogicLayer.ViewModels
{
    public class CarDetailsViewModel
    {
        public int Id { get; set; }

        public string? Brand { get; set; }

        public string? Model { get; set; }

        public int Year { get; set; }

        public decimal PricePerDay { get; set; }

        public CarStatus Status { get; set; }

        public int CategoryId { get; set; }

        public string? CategoryName { get; set; }

        public string? ImageUrl { get; set; }
    }
}
