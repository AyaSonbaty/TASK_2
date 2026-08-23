using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace DataAccessLayer.Models
{
    public class TraneeCourse
    {
        public int TraneeId { get; set; }
        public Tranee? Tranee { get; set; }

        public int CourseId { get; set; }
        public Course? Course { get; set; }

        [Required(ErrorMessage = "you have to enter a grade")]
        public string Grade { get; set; }
    }
}
