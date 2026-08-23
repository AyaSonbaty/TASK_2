using BLLayer.Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using TASK_2.ViewModels;

namespace TASK_2.Validations
{
    public class CheckManagerBelongsToDepartment : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            var departmentBl = validationContext.GetService<IDepartmentBl>();
            if (departmentBl == null)
                return new ValidationResult("Validation service not available");

            var vm = validationContext.ObjectInstance as DepartmentViewModel;
            int? currentDepartmentId = (vm != null && vm.Id != 0) ? vm.Id : (int?)null;

            var availableManagers = departmentBl.GetNotManager(currentDepartmentId)
                .Select(x => x.Id)
                .ToList();

            if (availableManagers.Contains((int)value))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("you have to enter the right manager");
        }
    }
}

