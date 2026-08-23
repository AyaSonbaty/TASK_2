using BLLayer.Interfaces;
using BLLayer.Services;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using TASK_2.ViewModels;

namespace TASK_2.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseBL _courseBl;
        private readonly IDepartmentBl _departmentBl;
        private readonly IInstructorBl _instructorBl;

        public CourseController(ICourseBL courseBl, IDepartmentBl departmentBl, IInstructorBl instructorBl)
        {
            _courseBl = courseBl;
            _departmentBl = departmentBl;
            _instructorBl = instructorBl;
        }

        public IActionResult Index()
        {
            var courses = _courseBl.GetAll();

            ViewData["TotalCourses"] = courses.Count;
            ViewBag.PageTitle = "All Courses";

            HttpContext.Session.SetString("LastVisitedPage", "CourseIndex");

            int visitCount = 0;
            if (Request.Cookies["CourseVisitCount"] != null)
            {
                visitCount = int.Parse(Request.Cookies["CourseVisitCount"]);
            }
            visitCount++;
            Response.Cookies.Append("CourseVisitCount", visitCount.ToString(),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(7) });

            ViewBag.VisitCount = visitCount;

            return View(courses);
        }

        public IActionResult Details(int id)
        {
            var course = _courseBl.GetById(id);
            if (course == null) return NotFound();

            var viewModel = new CourseDetailsViewModel
            {
                Id = course.Id,
                Name = course.Name,
                MinDegree = course.MinDegree,
                DepartmentName = course.Department?.Name,
                InstructorName = course.Instructor?.Name
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_departmentBl.GetAll(), "Id", "Name");
            ViewBag.Instructors = new SelectList(_instructorBl.GetAll(), "Id", "Name");
            return View(new CourseCreateViewModel());
        }

        [HttpPost]
        public IActionResult Create(CourseCreateViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(_departmentBl.GetAll(), "Id", "Name", vm.DepartmentId);
                ViewBag.Instructors = new SelectList(_instructorBl.GetAll(), "Id", "Name", vm.InstructorId);
                return View(vm);
            }

            var course = new Course
            {
                Name = vm.Name,
                MinDegree = vm.MinDegree,
                DepartmentId = vm.DepartmentId,
                InstructorId = vm.InstructorId
            };

            _courseBl.Add(course);
            TempData["SuccessMessage"] = "Course added successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var course = _courseBl.GetById(id);
            if (course == null) return NotFound();

            var vm = new CourseFormViewModel
            {
                Id = course.Id,
                Name = course.Name,
                MinDegree = course.MinDegree,
                DepartmentId = course.DepartmentId,
                InstructorId = course.InstructorId
            };

            ViewBag.Departments = new SelectList(_departmentBl.GetAll(), "Id", "Name", vm.DepartmentId);
            ViewBag.Instructors = new SelectList(_instructorBl.GetAll(), "Id", "Name", vm.InstructorId);
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(CourseFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = new SelectList(_departmentBl.GetAll(), "Id", "Name", vm.DepartmentId);
                ViewBag.Instructors = new SelectList(_instructorBl.GetAll(), "Id", "Name", vm.InstructorId);
                return View(vm);
            }

            var course = _courseBl.GetById(vm.Id);
            if (course == null) return NotFound();

            course.Name = vm.Name;
            course.MinDegree = vm.MinDegree;
            course.DepartmentId = vm.DepartmentId;
            course.InstructorId = vm.InstructorId;

            _courseBl.Update(course);
            TempData["SuccessMessage"] = "Course updated successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var course = _courseBl.GetById(id);
            if (course == null) return NotFound();

            if (_courseBl.HasTranees(id))
            {
                ViewBag.BlockedMessage = "This course still has trainees registered in it. Remove those registrations first.";
            }

            return View(course);
        }

        [HttpPost]
        public IActionResult Delete(int id, bool confirm)
        {
            if (_courseBl.HasTranees(id))
            {
                TempData["ErrorMessage"] = "Cannot delete this course while trainees are still registered in it.";
                return RedirectToAction("Index");
            }

            _courseBl.Delete(id);
            TempData["SuccessMessage"] = "Course deleted successfully";
            return RedirectToAction("Index");
        }


    }
}