using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MinDegree { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        public ICollection<Instructor> Instructors { get; set; } = new List<Instructor>();

    }
}

