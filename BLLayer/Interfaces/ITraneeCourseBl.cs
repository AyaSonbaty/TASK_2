using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer.Interfaces
{
    public interface ITraneeCourseBl
    {
        List<TraneeCourse> GetAll();
        TraneeCourse GetById(int traneeId, int courseId);
        void Add(TraneeCourse entity);
        void Update(TraneeCourse entity);
    }
}
