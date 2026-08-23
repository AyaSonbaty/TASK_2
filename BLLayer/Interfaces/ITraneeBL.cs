using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer.Interfaces
{
    public interface ITraneeBl : IBasicOperation<Tranee>
    {
        bool HasCourses(int traneeId);

    }
}
