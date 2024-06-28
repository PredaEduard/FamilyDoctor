using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using FamilyDoctor.Data;
using FamilyDoctor.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[Authorize]
public class ReviewController : Controller
{
    private readonly ApplicationDbContext _context;

    public ReviewController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var reviews = await _context.Reviews.ToListAsync();
        return View(reviews);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Review review)
    {
        if (ModelState.IsValid)
        {
            review.PatientId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Add(review);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(review);
    }
}
