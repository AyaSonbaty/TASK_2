using TASK_2.Validations;
using System.ComponentModel.DataAnnotations;

namespace TASK_2.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "the department name is required")]
        public string Name { get; set; }

        [CheckManagerBelongsToDepartment(ErrorMessage = "you have to enter the right manager")]
        public int? ManagerId { get; set; }
    }
}