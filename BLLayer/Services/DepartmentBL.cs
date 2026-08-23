using BLLayer.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Models;

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
        return _dbContext.Departments.ToList();
    }

    public Department GetById(int id)
    {
        return _dbContext.Departments.Find(id);
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
    }
