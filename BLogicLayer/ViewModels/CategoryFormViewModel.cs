using System.ComponentModel.DataAnnotations;

namespace BLogicLayer.ViewModels
{
    public class CategoryFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        public string? Name { get; set; }
    }
}
