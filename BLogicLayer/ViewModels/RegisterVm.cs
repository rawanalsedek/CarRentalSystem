using System.ComponentModel.DataAnnotations;

namespace BLogicLayer.ViewModels
{
    public class RegisterVm
    {
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter A Valid Email Address")]
        [Required]
        public string? Email { get; set; }

        [Required]
        public string? UserName { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Compare("Password", ErrorMessage = "the password doesn't match")]
        public string? confirmPassword { get; set; }
    }
}
