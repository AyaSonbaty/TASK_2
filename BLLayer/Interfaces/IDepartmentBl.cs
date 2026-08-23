using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer.Interfaces
{
    public interface IDepartmentBl: IBasicOperation<Department>

    {
        bool HasInstructorsOrCourses(int departmentId);
        List<Instructor> GetInstructorsInDepartment(int departmentId);
        List<Instructor> GetNotManager();
    }
}
