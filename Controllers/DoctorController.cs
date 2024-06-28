using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FamilyDoctor.Data;
using FamilyDoctor.Models;
using System.Linq;
using System.Threading.Tasks;

[Authorize(Roles = "Doctor")]
public class DoctorController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public DoctorController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var userId = _userManager.GetUserId(User);
        var appointments = await _context.Appointments
            .Include(a => a.Patient)
            .Where(a => a.DoctorId == userId)
            .ToListAsync();
        return View(appointments);
    }

    [HttpPost]
    public async Task<IActionResult> Accept(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment != null)
        {
            appointment.Status = "Accepted";
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Reject(int id)
    {
        var appointment = await _context.Appointments.FindAsync(id);
        if (appointment != null)
        {
            appointment.Status = "Rejected";
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
