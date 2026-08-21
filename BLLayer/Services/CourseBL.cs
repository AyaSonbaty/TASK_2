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
    public class CourseBL : ICourseBL
    {
        private readonly ITIDbContext _dbContext;

        public CourseBL(ITIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Course> GetAll()
        {
            return _dbContext.Courses.Include(c => c.Department).ToList();
        }

        public Course GetById(int id)
        {
            return _dbContext.Courses
                .Include(c => c.Department)
                .Include(c => c.Instructor)
                .FirstOrDefault(c => c.Id == id);
        }

        public void Add(Course entity)
        {
            _dbContext.Courses.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Course entity)
        {
            _dbContext.Courses.Update(entity);
            _dbContext.SaveChanges();
        }

        
    }
}


