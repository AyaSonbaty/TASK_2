using System.ComponentModel.DataAnnotations;
using TASK_2.Validations;

namespace TASK_2.ViewModels;

public class CourseFormViewModel
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "you have to enter the course name")]
    public string Name { get; set; }

    [Required(ErrorMessage = "the minimum degree is required")]
    public string MinDegree { get; set; }

    [Required]
    public int DepartmentId { get; set; }

    [CheckInstructorBelongsToDepartment]
    public int? InstructorId { get; set; }
}