using BLLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace BLLayer.Validations
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

            var availableManagers = departmentBl.GetNotManager()
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