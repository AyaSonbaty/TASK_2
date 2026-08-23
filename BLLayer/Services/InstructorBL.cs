using BLLayer.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BLLayer.Services;

public class InstructorBL : IInstructorBl
{
    private readonly ITIDbContext _dbContext;

    public InstructorBL(ITIDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Instructor> GetAll()
    {
        return _dbContext.Instructors.Include(i => i.Department).ToList();
    }

    public Instructor GetById(int id)
    {
        return _dbContext.Instructors
            .Include(i => i.Department)
            .FirstOrDefault(i => i.Id == id);
    }

    public List<Instructor> GetByDepartmentId(int departmentId)
    {
        return _dbContext.Instructors
            .Include(i => i.Department)
            .Where(i => i.DepartmentId == departmentId)
            .ToList();
    }

    public void Add(Instructor entity)
    {
        _dbContext.Instructors.Add(entity);
        _dbContext.SaveChanges();
    }

    public void Update(Instructor entity)
    {
        _dbContext.Instructors.Update(entity);
        _dbContext.SaveChanges();
    }
    public bool HasCourses(int instructorId)
    {
        return _dbContext.Courses.Any(c => c.InstructorId == instructorId);
    }

    public bool IsManagerOfDepartment(int instructorId)
    {
        return _dbContext.Departments.Any(d => d.ManagerId == instructorId);
    }

    public void Delete(int id)
    {
        var instructor = _dbContext.Instructors.Find(id);
        if (instructor is not null)
        {
            _dbContext.Instructors.Remove(instructor);
            _dbContext.SaveChanges();
        }
    }
}

