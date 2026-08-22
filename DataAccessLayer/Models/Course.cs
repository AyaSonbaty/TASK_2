using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string MinDegree { get; set; }

        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public int? InstructorId { get; set; }
        public Instructor? Instructor { get; set; }
        public ICollection<TraneeCourse> TraneeCourses { get; set; } = new List<TraneeCourse>();

    }
}

