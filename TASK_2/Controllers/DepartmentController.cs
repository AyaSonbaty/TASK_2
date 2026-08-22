using BLLayer.Interfaces;
using BLLayer.Services;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace TASK_2.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentBl _departmentBl;

        public DepartmentController(IDepartmentBl departmentBl)
        {
            _departmentBl = departmentBl;
        }

        public IActionResult Index()
        {
            var departments = _departmentBl.GetAll();
            return View(departments);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }

            _departmentBl.Add(department);
            TempData["SuccessMessage"] = "Department added successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var department = _departmentBl.GetById(id);
            if (department == null) return NotFound();
            return View(department);
        }

        [HttpPost]
        public IActionResult Edit(Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }

            _departmentBl.Update(department);
            TempData["SuccessMessage"] = "Department updated successfully";
            return RedirectToAction("Index");
        }
    }
}


