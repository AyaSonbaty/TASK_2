using BLLayer.Interfaces;
using BLLayer.Services;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
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
            ViewBag.Departments = _departmentBl.GetAll();
            ViewBag.Instructors = _instructorBl.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            // التحقق المنطقي
            if (course.InstructorId.HasValue)
            {
                var instructor = _instructorBl.GetById(course.InstructorId.Value);
                if (instructor == null || instructor.DepartmentId != course.DepartmentId)
                {
                    ModelState.AddModelError("InstructorId", "The instructor must belong to the selected department");
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Departments = _departmentBl.GetAll();
                ViewBag.Instructors = course.DepartmentId > 0
                    ? _instructorBl.GetByDepartmentId(course.DepartmentId)
                    : _instructorBl.GetAll();
                return View(course);
            }

            _courseBl.Add(course);
            TempData["SuccessMessage"] = "Course added successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var course = _courseBl.GetById(id);
            if (course == null) return NotFound();

            ViewBag.Departments = _departmentBl.GetAll();
            ViewBag.Instructors = _instructorBl.GetByDepartmentId(course.DepartmentId);
            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            if (course.InstructorId.HasValue)
            {
                var instructor = _instructorBl.GetById(course.InstructorId.Value);
                if (instructor == null || instructor.DepartmentId != course.DepartmentId)
                {
                    ModelState.AddModelError("InstructorId", "The instructor must belong to the selected department");
                }
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Departments = _departmentBl.GetAll();
                ViewBag.Instructors = _instructorBl.GetByDepartmentId(course.DepartmentId);
                return View(course);
            }

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



