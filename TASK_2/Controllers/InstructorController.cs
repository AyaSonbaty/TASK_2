using BLLayer.Interfaces;
using BLLayer.Services;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TASK_2.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorBl _instructorBl;
        private readonly IDepartmentBl _departmentBl;

        public InstructorController(IInstructorBl instructorBl, IDepartmentBl departmentBl)
        {
            _instructorBl = instructorBl;
            _departmentBl = departmentBl;
        }

        public IActionResult ByDepartment(int id)
        {
            var instructors = _instructorBl.GetByDepartmentId(id);
            return View(instructors);
        }

        public IActionResult Details(int id)
        {
            var instructor = _instructorBl.GetById(id);
            if (instructor == null) return NotFound();
            return View(instructor);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = _departmentBl.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = _departmentBl.GetAll();
                return View(instructor);
            }

            _instructorBl.Add(instructor);
            TempData["SuccessMessage"] = "Instructor added successfully";
            return RedirectToAction("ByDepartment", new { id = instructor.DepartmentId });
        }

        public IActionResult Edit(int id)
        {
            var instructor = _instructorBl.GetById(id);
            if (instructor == null) return NotFound();

            ViewBag.Departments = _departmentBl.GetAll();
            return View(instructor);
        }

        [HttpPost]
        public IActionResult Edit(Instructor instructor)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = _departmentBl.GetAll();
                return View(instructor);
            }

            _instructorBl.Update(instructor);
            TempData["SuccessMessage"] = "Instructor updated successfully";
            return RedirectToAction("ByDepartment", new { id = instructor.DepartmentId });
        }
        public IActionResult Delete(int id)
        {
            var instructor = _instructorBl.GetById(id);
            if (instructor == null) return NotFound();

            if (_instructorBl.HasCourses(id))
            {
                ViewBag.BlockedMessage = "This instructor still teaches one or more courses. Reassign or remove those courses first.";
            }
            else if (_instructorBl.IsManagerOfDepartment(id))
            {
                ViewBag.BlockedMessage = "This instructor is the manager of a department. Assign a different manager first.";
            }

            return View(instructor);
        }

        [HttpPost]
        public IActionResult Delete(int id, bool confirm)
        {
            var instructor = _instructorBl.GetById(id);
            if (instructor == null) return NotFound();

            if (_instructorBl.HasCourses(id) || _instructorBl.IsManagerOfDepartment(id))
            {
                TempData["ErrorMessage"] = "Cannot delete this instructor while they teach courses or manage a department.";
                return RedirectToAction("ByDepartment", new { id = instructor.DepartmentId });
            }

            int departmentId = instructor.DepartmentId;
            _instructorBl.Delete(id);
            TempData["SuccessMessage"] = "Instructor deleted successfully";
            return RedirectToAction("ByDepartment", new { id = departmentId });
        }
    }
}