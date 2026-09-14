using System.ComponentModel.DataAnnotations;
using BLogicLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.Extensions.DependencyInjection;

namespace BLogicLayer.ValidationAttributes
{
    public class CarNotMaintenanceAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            if (!int.TryParse(value.ToString(), out int carId) || carId == 0)
            {
                return ValidationResult.Success;
            }

            var carService = validationContext.GetService<ICarService>();

            if (carService == null)
            {
                return ValidationResult.Success;
            }

            var car = carService.GetById(carId);

            if (car == null)
            {
                return new ValidationResult("Car must exist.");
            }

            if (car.Status == CarStatus.Maintenance)
            {
                return new ValidationResult("This car is under maintenance and cannot be reserved.");
            }

            return ValidationResult.Success;
        }
    }
}
