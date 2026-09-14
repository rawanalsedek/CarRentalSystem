using System.ComponentModel.DataAnnotations;
using BLogicLayer.ValidationAttributes;

namespace BLogicLayer.ViewModels
{
    [StartBeforeEnd]
    [NoOverlapReservation]
    public class ReservationFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Car must exist.")]
        [Range(1, int.MaxValue, ErrorMessage = "Car must exist.")]
        [CarNotMaintenance]
        public int CarId { get; set; }

        public string? CarName { get; set; }

        public decimal PricePerDay { get; set; }

        [Required(ErrorMessage = "Start Date is required.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public decimal TotalPrice { get; set; }
    }
}
