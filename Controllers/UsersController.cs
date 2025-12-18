using Project.Data;
using Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Project.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDBContext appDBContext;

        public UsersController(AppDBContext context)
        {
            appDBContext = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = await appDBContext.Users.ToListAsync();
            return View(users);
        }

        // GET: Users/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        public async Task<IActionResult> Search(string search)
        {
            var users = from u in appDBContext.Users
                        select u;

            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(u =>
                    u.Username.Contains(search) ||
                    u.FirstName.Contains(search) ||
                    u.LastName.Contains(search) ||
                    u.Role.Contains(search)
                );
            }

            return View("Index", await users.ToListAsync()); ;
        }

        // POST: Users/Create
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (!ModelState.IsValid)
                return View(user);

            // 1. Save the User
            appDBContext.Users.Add(user);
            await appDBContext.SaveChangesAsync();

            // 2. If the User is a PATIENT, auto-create a Patient record
            if (user.Role == "Patient")
            {
                var patient = new Patient
                {
                    Name = user.FirstName,   // or user.Username if you prefer
                    Condition = "Unknown",
                    ClinicianNotes = null,
                    AssignedClinicianId = null,
                    Notes = "",
                    UserId = user.Id,
                };

                appDBContext.Patients.Add(patient);
                await appDBContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }




        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) { return NotFound(); }
            var user = await appDBContext.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) { return NotFound(); }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await appDBContext.Users.FindAsync(id);
            if (user != null)
            {
                appDBContext.Users.Remove(user);
                await appDBContext.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: Users/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await appDBContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    appDBContext.Update(user);
                    await appDBContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!appDBContext.Users.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

    }
}
