using BLLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace TASK_2.Controllers
{
    public class TraneeController : Controller
    {
        private readonly ITraneeBl _traneeBl;

        public TraneeController(ITraneeBl traneeBl)
        {
            _traneeBl = traneeBl;
        }

        public IActionResult Index()
        {
            var tranees = _traneeBl.GetAll();
            return View(tranees);
        }

        public IActionResult Details(int id)
        {
            var tranee = _traneeBl.GetById(id);
            if (tranee == null) return NotFound();
            return View(tranee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tranee tranee)
        {
            if (!ModelState.IsValid)
            {
                return View(tranee);
            }

            _traneeBl.Add(tranee);
            TempData["SuccessMessage"] = "Trainee added successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var tranee = _traneeBl.GetById(id);
            if (tranee == null) return NotFound();
            return View(tranee);
        }

        [HttpPost]
        public IActionResult Edit(Tranee tranee)
        {
            if (!ModelState.IsValid)
            {
                return View(tranee);
            }

            _traneeBl.Update(tranee);
            TempData["SuccessMessage"] = "Trainee updated successfully";
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var tranee = _traneeBl.GetById(id);
            if (tranee == null) return NotFound();

            if (_traneeBl.HasCourses(id))
            {
                ViewBag.BlockedMessage = "This trainee is still registered in one or more courses. Remove those registrations first.";
            }

            return View(tranee);
        }

        [HttpPost]
        public IActionResult Delete(int id, bool confirm)
        {
            if (_traneeBl.HasCourses(id))
            {
                TempData["ErrorMessage"] = "Cannot delete this trainee while they are still registered in courses.";
                return RedirectToAction("Index");
            }

            _traneeBl.Delete(id);
            TempData["SuccessMessage"] = "Trainee deleted successfully";
            return RedirectToAction("Index");
        }
        }
}
