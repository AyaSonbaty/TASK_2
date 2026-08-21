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
}
