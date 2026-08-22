using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class TraneeCourse
    {
        public int TraneeId { get; set; }
        public Tranee? Tranee { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }

        public string Grade { get; set; }
    }
}
