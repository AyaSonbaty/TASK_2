using System.ComponentModel.DataAnnotations;
using BLLayer.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TASK_2.ViewModels;

namespace TASK_2.Validations;

public class CheckInstructorBelongsToDepartment : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return ValidationResult.Success;

        int departmentId = validationContext.ObjectInstance switch
        {
            CourseCreateViewModel createVm => createVm.DepartmentId,
            CourseFormViewModel editVm => editVm.DepartmentId,
            _ => 0
        };

        var instructorsInDept = validationContext.GetService<IInstructorBl>()
            .GetByDepartmentId(departmentId)
            .Select(i => i.Id)
            .ToList();

        if (instructorsInDept.Contains((int)value))
        {
            return ValidationResult.Success;
        }
        else return new ValidationResult("you have to enter the right instructor");
    }
}