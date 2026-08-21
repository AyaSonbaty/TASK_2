using System;
using BLLayer.Services;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace TASK_2.Controllers
{
    public class InstructorController : Controller
    {
        InstructorBL _instructorBl = new InstructorBL();

        // GET:
        public IActionResult Index()
        {
            var instructors = _instructorBl.GetAll();
            string msg = "Instructors loaded";

            ViewData["msg"] = msg;
            ViewBag.InstructorsCount = instructors.Count;
            TempData["msg"] = msg;

            HttpContext.Session.SetString("LastVisitedPage", "Instructor Index");

            var options = new CookieOptions()
            {
                Expires = DateTime.Now.AddMinutes(1),
                HttpOnly = false
            };
            HttpContext.Response.Cookies.Append("InstructorViewMode", "table", options);

            return View(instructors);
        }

        public IActionResult ByDepartment(int id)
        {
            var instructors = _instructorBl.GetByDepartmentId(id);
            return View(instructors);
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var instructor = _instructorBl.GetById(id.Value);

            if (instructor == null) return NotFound();

            return View(instructor);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Save(string name, string address, decimal salary, int departmentId)
        {
            var instructor = new Instructor()
            {
                Name = name,
                Address = address,
                Salary = salary,
                DepartmentId = departmentId
            };
            _instructorBl.Add(instructor);

            return RedirectToAction("Index");
        }

        public IActionResult ShowCookie()
        {
            return Content(HttpContext.Request.Cookies["InstructorViewMode"]);
        }

        public IActionResult ShowSession()
        {
            return Content(HttpContext.Session.GetString("LastVisitedPage"));
        }
    }
}