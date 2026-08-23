using BLLayer.Validations;
using System.ComponentModel.DataAnnotations;

namespace TASK_2.ViewModels
{
    public class DepartmentEditViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ManagerName { get; set; }

        [CheckManagerBelongsToDepartment]
        public int? ManagerId { get; set; }
    }
}
