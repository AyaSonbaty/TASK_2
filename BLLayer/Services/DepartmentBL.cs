using BLLayer.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Models;

namespace BLLayer.Services;

public class DepartmentBL : IDepartmentBl
{
    private ITIDbContext _dbContext = new ITIDbContext();

    public List<Department> GetAll()
    {
        return _dbContext.Departments.ToList();
    }

    public Department GetById(int id)
    {
        return _dbContext.Departments.Find(id);
    }
}

