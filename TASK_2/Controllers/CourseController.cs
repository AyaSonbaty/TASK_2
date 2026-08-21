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
        private readonly ICourseBl _courseBl;
        private readonly IDepartmentBl _departmentBl;
        private readonly IInstructorBl _instructorBl;

        public CourseController(ICourseBl courseBl, IDepartmentBl departmentBl, IInstructorBl instructorBl)
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
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = _departmentBl.GetAll();
                ViewBag.Instructors = _instructorBl.GetAll();
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
            ViewBag.Instructors = _instructorBl.GetAll();
            return View(course);
        }

        [HttpPost]
        public IActionResult Edit(Course course)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Departments = _departmentBl.GetAll();
                ViewBag.Instructors = _instructorBl.GetAll();
                return View(course);
            }

            _courseBl.Update(course);
            TempData["SuccessMessage"] = "Course updated successfully";
            return RedirectToAction("Index");
        }

        public IActionResult AssignInstructor()
        {
            var viewModel = new AssignInstructorViewModel
            {
                Courses = _courseBl.GetAll(),
                Instructors = _instructorBl.GetAll()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AssignInstructor(AssignInstructorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Courses = _courseBl.GetAll();
                model.Instructors = _instructorBl.GetAll();
                return View(model);
            }

            _courseBl.AssignInstructorToCourse(model.CourseId, model.InstructorId);

            TempData["SuccessMessage"] = "Instructor assigned to course successfully";
            return RedirectToAction("Index");
        }
    }
}