using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PresentationLayer.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _service;
        private readonly ICategoryService _categoryService;

        public CarController(
            ICarService service,
            ICategoryService categoryService)
        {
            _service = service;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var cars = _service.GetAll();
            return View(cars);
        }

        [Authorize(Roles = "Super")]
        public IActionResult Manage()
        {
            var cars = _service.GetAll();
            return View(cars);
        }

        public IActionResult Details(int id)
        {
            var car = _service.GetById(id);

            if (car == null)
            {
                return NotFound();
            }

            var vm = new CarDetailsViewModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                PricePerDay = car.PricePerDay,
                Status = car.Status,
                CategoryId = car.CategoryId,
                CategoryName = car.Category?.Name,
                ImageUrl = car.ImageUrl
            };

            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Super")]
        public IActionResult Create()
        {
            LoadCategories();
            return View(new CarFormViewModel
            {
                Year = DateTime.Now.Year
            });
        }

        [HttpPost]
        [Authorize(Roles = "Super")]
        public IActionResult Create(CarFormViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var car = new Car
                {
                    Brand = vm.Brand,
                    Model = vm.Model,
                    Year = vm.Year,
                    PricePerDay = vm.PricePerDay,
                    Status = vm.Status,
                    CategoryId = vm.CategoryId,
                    ImageUrl = vm.ImageUrl
                };

                _service.Add(car);
                return RedirectToAction("Manage");
            }

            LoadCategories();
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Super")]
        public IActionResult Edit(int id)
        {
            var car = _service.GetById(id);

            if (car == null)
            {
                return NotFound();
            }

            var model = new CarFormViewModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                PricePerDay = car.PricePerDay,
                Status = car.Status,
                CategoryId = car.CategoryId,
                ImageUrl = car.ImageUrl
            };

            LoadCategories();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Super")]
        public IActionResult Edit(CarFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories();
                return View(vm);
            }

            var car = _service.GetById(vm.Id);

            if (car == null)
            {
                return NotFound();
            }

            car.Brand = vm.Brand;
            car.Model = vm.Model;
            car.Year = vm.Year;
            car.PricePerDay = vm.PricePerDay;
            car.Status = vm.Status;
            car.CategoryId = vm.CategoryId;
            car.ImageUrl = vm.ImageUrl;

            _service.Update(car);
            return RedirectToAction("Manage");
        }

        [HttpGet]
        [Authorize(Roles = "Super")]
        public IActionResult Delete(int id)
        {
            var car = _service.GetById(id);

            if (car == null)
            {
                return NotFound();
            }

            return View(car);
        }

        [HttpPost]
        [ActionName("Delete")]
        [Authorize(Roles = "Super")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _service.Delete(id);
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("Manage");
        }

        private void LoadCategories()
        {
            ViewBag.Categories = new SelectList(
                _categoryService.GetAll(),
                "Id",
                "Name");
        }
    }
}
