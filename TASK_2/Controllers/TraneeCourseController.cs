using BLLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace TASK_2.Controllers
{
    public class TraneeCourseController : Controller
    {
        private readonly ITraneeCourseBl _traneeCourseBl;
        private readonly ITraneeBl _traneeBl;
        private readonly ICourseBL _courseBl;

        public TraneeCourseController(ITraneeCourseBl traneeCourseBl, ITraneeBl traneeBl, ICourseBL courseBl)
        {
            _traneeCourseBl = traneeCourseBl;
            _traneeBl = traneeBl;
            _courseBl = courseBl;
        }

        public IActionResult Index()
        {
            var list = _traneeCourseBl.GetAll();
            return View(list);
        }

        public IActionResult Create()
        {
            ViewBag.Tranees = _traneeBl.GetAll();
            ViewBag.Courses = _courseBl.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(TraneeCourse traneeCourse)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Tranees = _traneeBl.GetAll();
                ViewBag.Courses = _courseBl.GetAll();
                return View(traneeCourse);
            }

            _traneeCourseBl.Add(traneeCourse);
            TempData["SuccessMessage"] = "Trainee registered in course successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int traneeId, int courseId)
        {
            var traneeCourse = _traneeCourseBl.GetById(traneeId, courseId);
            if (traneeCourse == null) return NotFound();

            ViewBag.Tranees = _traneeBl.GetAll();
            ViewBag.Courses = _courseBl.GetAll();
            return View(traneeCourse);
        }

        [HttpPost]
        public IActionResult Edit(TraneeCourse traneeCourse)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Tranees = _traneeBl.GetAll();
                ViewBag.Courses = _courseBl.GetAll();
                return View(traneeCourse);
            }

            _traneeCourseBl.Update(traneeCourse);
            TempData["SuccessMessage"] = "Grade updated successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int traneeId, int courseId)
        {
            var traneeCourse = _traneeCourseBl.GetById(traneeId, courseId);
            if (traneeCourse == null) return NotFound();

            return View(traneeCourse);
        }

        [HttpPost]
        public IActionResult Delete(int traneeId, int courseId, bool confirm)
        {
            _traneeCourseBl.Delete(traneeId, courseId);
            TempData["SuccessMessage"] = "Registration removed successfully";
            return RedirectToAction("Index");
        }
    }
}
