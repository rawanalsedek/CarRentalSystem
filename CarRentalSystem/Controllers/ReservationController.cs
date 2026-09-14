using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationService _service;
        private readonly ICarService _carService;
        private readonly UserManager<User> _userManager;

        public ReservationController(
            IReservationService service,
            ICarService carService,
            UserManager<User> userManager)
        {
            _service = service;
            _carService = carService;
            _userManager = userManager;
        }

        [Authorize(Roles = "Super")]
        public IActionResult Index()
        {
            var reservations = _service.GetAll();
            return View(reservations);
        }

        [Authorize]
        public IActionResult MyReservations()
        {
            var userId = _userManager.GetUserId(User);

            if (string.IsNullOrEmpty(userId))
            {
                return Challenge();
            }

            var reservations = _service.GetByUserId(userId);
            return View(reservations);
        }

        [Authorize(Roles = "Super")]
        public IActionResult Details(int id)
        {
            var reservation = _service.GetById(id);

            if (reservation == null)
            {
                return NotFound();
            }

            var vm = new ReservationDetailsViewModel
            {
                Id = reservation.Id,
                CarId = reservation.CarId,
                CarName = $"{reservation.Car?.Brand} {reservation.Car?.Model}",
                CategoryName = reservation.Car?.Category?.Name,
                CustomerName = reservation.User?.UserName,
                CustomerEmail = reservation.User?.Email,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                TotalPrice = reservation.TotalPrice,
                Status = reservation.Status
            };

            return View(vm);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create(int carId)
        {
            return RedirectToAction("Details", "Car", new { id = carId });
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(ReservationFormViewModel model)
        {
            var car = _carService.GetById(model.CarId);

            if (car == null)
            {
                ModelState.AddModelError(string.Empty, "Car must exist.");
            }
            else
            {
                model.CarName = $"{car.Brand} {car.Model}";
                model.PricePerDay = car.PricePerDay;
            }

            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);

                if (string.IsNullOrEmpty(userId))
                {
                    return Challenge();
                }

                var reservation = new Reservation
                {
                    CarId = model.CarId,
                    UserId = userId,
                    StartDate = model.StartDate.Date,
                    EndDate = model.EndDate.Date
                };

                try
                {
                    _service.Add(reservation);
                    return RedirectToAction("MyReservations");
                }
                catch (InvalidOperationException ex)
                {
                    TempData["Error"] = ex.Message;
                    return RedirectToAction("Details", "Car", new { id = model.CarId });
                }
            }

            var error = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .FirstOrDefault();

            TempData["Error"] = string.IsNullOrWhiteSpace(error)
                ? "Please check the selected dates."
                : error;

            return RedirectToAction("Details", "Car", new { id = model.CarId });
        }

        [HttpPost]
        [Authorize(Roles = "Super")]
        public IActionResult Approve(int id)
        {
            _service.ChangeStatus(id, ReservationStatus.Approved);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Super")]
        public IActionResult Reject(int id)
        {
            _service.ChangeStatus(id, ReservationStatus.Rejected);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Super")]
        public IActionResult Complete(int id)
        {
            _service.ChangeStatus(id, ReservationStatus.Completed);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Cancel(int id)
        {
            var reservation = _service.GetById(id);

            if (reservation == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);
            var isSuper = User.IsInRole("Super");

            if (!isSuper && reservation.UserId != userId)
            {
                return Forbid();
            }

            if (reservation.Status != ReservationStatus.Pending &&
                reservation.Status != ReservationStatus.Approved)
            {
                if (isSuper)
                {
                    return RedirectToAction("Index");
                }

                return RedirectToAction("MyReservations");
            }

            _service.ChangeStatus(id, ReservationStatus.Cancelled);

            if (isSuper)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("MyReservations");
        }
    }
}
