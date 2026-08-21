using System;
using BLLayer.Services;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace TASK_2.Controllers
{
    public class CourseController : Controller
    {
        CourseBL _courseBl = new CourseBL();

        // GET:
        public IActionResult Index()
        {
            var courses = _courseBl.GetAll();
            string msg = "Courses loaded";

            ViewData["msg"] = msg;
            ViewBag.CoursesCount = courses.Count;
            TempData["msg"] = msg;

            HttpContext.Session.SetString("LastVisitedPage", "Course Index");

            var options = new CookieOptions()
            {
                Expires = DateTime.Now.AddMinutes(1),
                HttpOnly = false
            };
            HttpContext.Response.Cookies.Append("CourseViewMode", "table", options);

            return View(courses);
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var course = _courseBl.GetById(id.Value);

            if (course == null) return NotFound();

            return View(course);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Save(string name, string minDegree, int departmentId)
        {
            var course = new Course()
            {
                Name = name,
                MinDegree = minDegree,
                DepartmentId = departmentId
            };
            _courseBl.Add(course);

            return RedirectToAction("Index");
        }

        public IActionResult ShowCookie()
        {
            return Content(HttpContext.Request.Cookies["CourseViewMode"]);
        }

        public IActionResult ShowSession()
        {
            return Content(HttpContext.Session.GetString("LastVisitedPage"));
        }
    }
}

