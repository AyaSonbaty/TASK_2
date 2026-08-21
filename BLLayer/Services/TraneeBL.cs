using BLLayer.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer.Services
{
    public class TraneeBL : ITraneeBl
    {
        private readonly ITIDbContext _dbContext;

        public TraneeBL(ITIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Tranee> GetAll()
        {
            return _dbContext.Tranees.ToList();
        }

        public Tranee GetById(int id)
        {
            return _dbContext.Tranees.Find(id);
        }

        public void Add(Tranee entity)
        {
            _dbContext.Tranees.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update(Tranee entity)
        {
            _dbContext.Tranees.Update(entity);
            _dbContext.SaveChanges();
        }
    }
}
