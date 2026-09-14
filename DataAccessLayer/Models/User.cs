using Microsoft.AspNetCore.Identity;

namespace DataAccessLayer.Models
{
    public class User : IdentityUser
    {
        public List<Reservation>? Reservations { get; set; }
    }
}
