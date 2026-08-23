using BLLayer.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BLLayer.Services;

public class DepartmentBL : IDepartmentBl
{
    private readonly ITIDbContext _dbContext;

    public DepartmentBL(ITIDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public List<Department> GetAll()
    {
        return _dbContext.Departments.Include(d => d.Manager).ToList();
    }

    public Department GetById(int id)
    {
        return _dbContext.Departments
            .Include(d => d.Manager)
            .FirstOrDefault(d => d.Id == id);
    }

    public void Add(Department entity)
    {
        _dbContext.Departments.Add(entity);
        _dbContext.SaveChanges();
    }

    public void Update(Department entity)
    {
        _dbContext.Departments.Update(entity);
        _dbContext.SaveChanges();
    }

    public bool HasInstructorsOrCourses(int departmentId)
    {
        return _dbContext.Instructors.Any(i => i.DepartmentId == departmentId)
            || _dbContext.Courses.Any(c => c.DepartmentId == departmentId);
    }

    public List<Instructor> GetInstructorsInDepartment(int departmentId)
    {
        return _dbContext.Instructors.Where(i => i.DepartmentId == departmentId).ToList();
    }

    public void Delete(int id)
    {
        var department = _dbContext.Departments.Find(id);
        if (department is not null)
        {
            _dbContext.Departments.Remove(department);
            _dbContext.SaveChanges();
        }
    }

    public List<Instructor> GetNotManager(int? excludeDepartmentId = null)
    {
        var assignedManagers = _dbContext.Departments
            .Where(d => d.ManagerId != null && d.Id != excludeDepartmentId)
            .Select(d => d.ManagerId.Value)
            .ToList();

        return _dbContext.Instructors
            .Where(i => !assignedManagers.Contains(i.Id))
            .ToList();
    }
}



