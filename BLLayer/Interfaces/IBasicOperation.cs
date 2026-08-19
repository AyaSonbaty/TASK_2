using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLayer.Interfaces
{
    public interface IBasicOperation<T>
    {
        List<T> GetAll();
        T GetById(int id);
    }
}

