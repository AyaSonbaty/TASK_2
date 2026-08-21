using BLLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Data;
using DataAccessLayer.Models;


namespace BLLayer.Services
{
    public class CourseBL:ICourseBL
    {
        private ITIDbContext _dbContext=new ITIDbContext(); 
        public List<Course> GetAll()
        {
            return _dbContext.Courses.ToList();
        }
        public Course GetById(int id)
        {
            return _dbContext.Courses
                .Include(c => c.Department)
                .FirstOrDefault(c => c.Id == id);
                
        }
        public void Add(Course course)
        {
            _dbContext.Courses .Add(course);
            _dbContext.SaveChanges();
        }
    }
}


