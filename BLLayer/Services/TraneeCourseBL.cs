using BLLayer.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace BLLayer.Services
{
    public class TraneeCourseBL : ITraneeCourseBl
    {
        private readonly ITIDbContext _dbContext;

        public TraneeCourseBL(ITIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TraneeCourse> GetAll()
        {
            return _dbContext.TraneeCourses
                .Include(tc => tc.Tranee)
                .Include(tc => tc.Course)
                .ToList();
        }

        public TraneeCourse GetById(int traneeId, int courseId)
        {
            return _dbContext.TraneeCourses
                .Include(tc => tc.Tranee)
                .Include(tc => tc.Course)
                .FirstOrDefault(tc => tc.TraneeId == traneeId && tc.CourseId == courseId);
        }

        public void Add(TraneeCourse entity)
        {
            _dbContext.TraneeCourses.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(TraneeCourse entity)
        {
            _dbContext.TraneeCourses.Update(entity);
            _dbContext.SaveChanges();
        }
        public void Delete(int traneeId, int courseId)
        {
            var traneeCourse = _dbContext.TraneeCourses
                .FirstOrDefault(tc => tc.TraneeId == traneeId && tc.CourseId == courseId);

            if (traneeCourse is not null)
            {
                _dbContext.TraneeCourses.Remove(traneeCourse);
                _dbContext.SaveChanges();
            }
        }
    }
}


