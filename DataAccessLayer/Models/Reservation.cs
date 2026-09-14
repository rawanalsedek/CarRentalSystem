using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public string UserId { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal TotalPrice { get; set; }

        public ReservationStatus Status { get; set; }

        public Car? Car { get; set; }

        public User? User { get; set; }
    }
}
