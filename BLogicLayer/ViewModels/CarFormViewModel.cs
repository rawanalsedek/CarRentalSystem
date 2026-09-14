using System.ComponentModel.DataAnnotations;
using DataAccessLayer.Models;

namespace BLogicLayer.ViewModels
{
    public class CarFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Brand is required.")]
        public string? Brand { get; set; }

        [Required(ErrorMessage = "Model is required.")]
        public string? Model { get; set; }

        [Range(1990, 2035, ErrorMessage = "Enter a valid year.")]
        public int Year { get; set; }

        [Range(0.01, 100000, ErrorMessage = "Enter a valid price.")]
        public decimal PricePerDay { get; set; }

        public CarStatus Status { get; set; }

        [Required(ErrorMessage = "Please select a category.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a category.")]
        public int CategoryId { get; set; }

        public string? ImageUrl { get; set; }
    }
}
