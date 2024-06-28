using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using FamilyDoctor.Data;
using FamilyDoctor.Models;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class PharmaceuticalProductController : Controller
{
    private readonly ApplicationDbContext _context;

    public PharmaceuticalProductController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _context.PharmaceuticalProducts.ToListAsync();
        return View(products);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(PharmaceuticalProduct product)
    {
        if (ModelState.IsValid)
        {
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }
}
