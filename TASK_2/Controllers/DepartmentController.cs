using BLLayer.Interfaces;
using BLLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace TASK_2.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            var bl = new DepartmentBL();   
            var departments = bl.GetAll();
            return View(departments);
        }
    }
}

