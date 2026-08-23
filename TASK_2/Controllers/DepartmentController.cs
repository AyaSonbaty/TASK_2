using BLLayer.Interfaces;
using BLLayer.Services;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using TASK_2.ViewModels;

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

            var vm = new DepartmentEditViewModel
            {
                Id = department.Id,
                Name = department.Name,
                ManagerName = department.ManagerName,
                ManagerId = department.ManagerId
            };

            ViewBag.Instructors = _departmentBl.GetNotManager();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(DepartmentEditViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Instructors = _departmentBl.GetNotManager();
                return View(vm);
            }

            var department = _departmentBl.GetById(vm.Id);
            if (department == null) return NotFound();

            department.Name = vm.Name;
            department.ManagerName = vm.ManagerName;
            department.ManagerId = vm.ManagerId;

            _departmentBl.Update(department);
            TempData["SuccessMessage"] = "Department updated successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var department = _departmentBl.GetById(id);
            if (department == null) return NotFound();

            if (_departmentBl.HasInstructorsOrCourses(id))
            {
                ViewBag.BlockedMessage = "This department still has instructors or courses linked to it. Remove them first before deleting the department.";
            }

            return View(department);
        }

        [HttpPost]
        public IActionResult Delete(int id, bool confirm)
        {
            if (_departmentBl.HasInstructorsOrCourses(id))
            {
                TempData["ErrorMessage"] = "Cannot delete this department while it still has instructors or courses linked to it.";
                return RedirectToAction("Index");
            }

            _departmentBl.Delete(id);
            TempData["SuccessMessage"] = "Department deleted successfully";
            return RedirectToAction("Index");
        }
    }
}


