using TASK_2.Validations;

namespace TASK_2.ViewModels;

public class CourseCreateViewModel
{
    public string Name { get; set; }
    public string MinDegree { get; set; }
    public int DepartmentId { get; set; }

    [CheckInstructorBelongsToDepartment]
    public int? InstructorId { get; set; }
}