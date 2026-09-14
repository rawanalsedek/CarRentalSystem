namespace BLogicLayer.ViewModels
{
    public class CustomerViewModel
    {
        public string Id { get; set; } = string.Empty;

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public int ReservationsCount { get; set; }
    }
}
