using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Authorize(Roles = "Super")]
    public class AdminController : Controller
    {
        private readonly ICarService _carService;
        private readonly IReservationService _reservationService;
        private readonly ICustomerService _customerService;

        public AdminController(
            ICarService carService,
            IReservationService reservationService,
            ICustomerService customerService)
        {
            _carService = carService;
            _reservationService = reservationService;
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            var vm = new DashboardViewModel
            {
                CarsCount = _carService.GetCount(),
                ReservationsCount = _reservationService.GetCount(),
                CustomersCount = _customerService.GetCount(),
                PendingReservationsCount = _reservationService.GetPendingCount()
            };

            ViewBag.RecentReservations = _reservationService.GetAll().Take(5).ToList();

            return View(vm);
        }

        public IActionResult Customers()
        {
            var customers = _customerService.GetAll();
            ViewBag.ActiveReservations = _reservationService.GetAll()
                .Count(r => r.Status == ReservationStatus.Pending ||
                            r.Status == ReservationStatus.Approved);

            return View(customers);
        }
    }
}
