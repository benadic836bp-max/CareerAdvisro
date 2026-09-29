using CareerAdvisor.Data;
using CareerAdvisor.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CareerAdvisor.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _db;
    private readonly RecommendationService _svc;
    public HomeController(AppDbContext db, RecommendationService svc) { _db = db; _svc = svc; }

    public async Task<IActionResult> Index()
        => View(await _db.Skills.OrderBy(s => s.Category).ThenBy(s => s.Name).ToListAsync());

    [HttpPost]
    public async Task<IActionResult> Recommend(Dictionary<int, int> levels)
        => View("Results", await _svc.RecommendAsync(levels));
}
