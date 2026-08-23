using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer.Interfaces
{
    public interface ICourseBL:IBasicOperation<Course>
    {
        bool HasTranees(int courseId);

    }
}
