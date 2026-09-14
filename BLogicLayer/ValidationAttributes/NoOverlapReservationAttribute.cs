using System.ComponentModel.DataAnnotations;
using BLogicLayer.Interfaces;
using BLogicLayer.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace BLogicLayer.ValidationAttributes
{
    public class NoOverlapReservationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not ReservationFormViewModel model)
            {
                return ValidationResult.Success;
            }

            if (model.CarId <= 0 || model.StartDate == default || model.EndDate == default)
            {
                return ValidationResult.Success;
            }

            if (model.StartDate.Date >= model.EndDate.Date)
            {
                return ValidationResult.Success;
            }

            var reservationService = validationContext.GetService<IReservationService>();

            if (reservationService == null)
            {
                return ValidationResult.Success;
            }

            var excludeId = model.Id > 0 ? model.Id : (int?)null;

            if (reservationService.HasOverlap(model.CarId, model.StartDate, model.EndDate, excludeId))
            {
                return new ValidationResult("This car is already reserved during the selected dates.");
            }

            return ValidationResult.Success;
        }
    }
}
