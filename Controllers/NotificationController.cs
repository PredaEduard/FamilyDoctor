using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using FamilyDoctor.Data;
using FamilyDoctor.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[Authorize]
public class NotificationController : Controller
{
    private readonly ApplicationDbContext _context;

    public NotificationController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var notifications = await _context.Notifications
            .Where(n => n.UserId == userId)
            .ToListAsync();
        return View(notifications);
    }

    [HttpPost]
    public async Task<IActionResult> MarkAllAsSeen()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var notifications = await _context.Notifications
            .Where(n => n.UserId == userId)
            .ToListAsync();

        foreach (var notification in notifications)
        {
            notification.Seen = true;
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> DeleteAll()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var notifications = await _context.Notifications
            .Where(n => n.UserId == userId)
            .ToListAsync();

        _context.Notifications.RemoveRange(notifications);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
