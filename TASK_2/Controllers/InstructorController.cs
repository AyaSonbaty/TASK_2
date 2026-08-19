using BLLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

public class InstructorController : Controller
{
    public IActionResult ByDepartment(int id)
    {
        var instructorBl = new InstructorBL();
        var instructors = instructorBl.GetByDepartmentId(id);
        return View(instructors);
    }

    public IActionResult Details(int id)
    {
        var instructorBl = new InstructorBL();
        var instructor = instructorBl.GetById(id);

        if (instructor == null)
        {
            return NotFound();
        }

        return View(instructor);
    }
}

