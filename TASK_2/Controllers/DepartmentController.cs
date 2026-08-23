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
            ViewBag.Instructors = _departmentBl.GetNotManager();
            return View(new DepartmentViewModel());
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Instructors = _departmentBl.GetNotManager();
                return View(vm);
            }

            var department = new Department
            {
                Name = vm.Name,
                ManagerId = vm.ManagerId
            };

            _departmentBl.Add(department);
            TempData["SuccessMessage"] = "Department added successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var department = _departmentBl.GetById(id);
            if (department == null) return NotFound();

            ViewBag.Instructors = _departmentBl.GetInstructorsInDepartment(id);
            return View(department); // أو الـ ViewModel لو استخدمته
        }

        [HttpPost]
        public IActionResult Edit(Department department) // أو ViewModel
        {
            if (department.ManagerId.HasValue)
            {
                var instructors = _departmentBl.GetInstructorsInDepartment(department.Id);
                if (!instructors.Any(i => i.Id == department.ManagerId.Value))
                {
                    ModelState.AddModelError("ManagerId", "Manager must be an instructor in this department");
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Instructors = _departmentBl.GetInstructorsInDepartment(department.Id);
                return View(department);
            }

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

