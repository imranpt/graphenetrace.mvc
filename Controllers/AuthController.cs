using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDBContext _context;

        public AuthController(AppDBContext context)
        {
            _context = context;
        }

        // GET: Login Page
        [HttpGet]
        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }

        // POST: Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Username == username && u.Password == password);


            if (user == null)
            {
                
                ViewBag.Error = "Invalid username or password.";
                return View();
            }

            HttpContext.Session.SetString("role", user.Role);
            HttpContext.Session.SetInt32("userId", user.Id);

            if (user.Role == "Clinician")
                return RedirectToAction("Dashboard", "Clinician");

            if (user.Role == "Admin")
                return RedirectToAction("Index", "Users");

            return RedirectToAction("Dashboard", "Patient");

        }


        // Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

     
    }
}

