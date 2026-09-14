using DataAccessLayer.Models;

namespace BLogicLayer.ViewModels
{
    public class ReservationDetailsViewModel
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public string? CarName { get; set; }

        public string? CategoryName { get; set; }

        public string? CustomerName { get; set; }

        public string? CustomerEmail { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal TotalPrice { get; set; }

        public ReservationStatus Status { get; set; }
    }
}
