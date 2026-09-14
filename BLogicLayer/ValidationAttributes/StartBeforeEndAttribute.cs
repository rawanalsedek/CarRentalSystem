using System.ComponentModel.DataAnnotations;
using BLogicLayer.ViewModels;

namespace BLogicLayer.ValidationAttributes
{
    public class StartBeforeEndAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not ReservationFormViewModel model)
            {
                return ValidationResult.Success;
            }

            if (model.StartDate == default || model.EndDate == default)
            {
                return ValidationResult.Success;
            }

            if (model.StartDate.Date >= model.EndDate.Date)
            {
                return new ValidationResult("Start Date must be before End Date.");
            }

            if (model.StartDate.Date < DateTime.Today)
            {
                return new ValidationResult("Start Date cannot be in the past.");
            }

            return ValidationResult.Success;
        }
    }
}
