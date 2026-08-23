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
        SyncManagerName(entity);
        _dbContext.Departments.Add(entity);
        _dbContext.SaveChanges();
    }

    public void Update(Department entity)
    {
        SyncManagerName(entity);
        _dbContext.Departments.Update(entity);
        _dbContext.SaveChanges();
    }

    // Keeps ManagerName always set to a real string (never null), matching
    // the column's NOT NULL constraint in the database as-is, with no
    // migration needed. Uses the chosen Instructor's name when a manager
    // is picked, or "No Manager" otherwise.
    private void SyncManagerName(Department entity)
    {
        entity.ManagerName = entity.ManagerId.HasValue
            ? (_dbContext.Instructors.Find(entity.ManagerId.Value)?.Name ?? "No Manager")
            : "No Manager";
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