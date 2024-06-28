using FamilyDoctor.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FamilyDoctor.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Products()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Reviews()
        {
            return View();
        }
    }
}