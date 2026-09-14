using System.ComponentModel.DataAnnotations;

namespace BLogicLayer.ViewModels
{
    public class LoginVm
    {
        public string? UserName { get; set; }

        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
