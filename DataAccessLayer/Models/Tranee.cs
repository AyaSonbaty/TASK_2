using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace DataAccessLayer.Models
{
    public class Tranee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "you have to enter the trainee name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "the address is required")]
        public string Address { get; set; }
        public ICollection<TraneeCourse> TraneeCourses { get; set; } = new List<TraneeCourse>();


    }
}


