using DataAccessLayer.Models;

namespace BLogicLayer.ViewModels
{
    public class CarSearchViewModel
    {
        public string? Brand { get; set; }

        public string? Model { get; set; }

        public int? CategoryId { get; set; }

        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public List<Car> Cars { get; set; } = new List<Car>();
    }
}
