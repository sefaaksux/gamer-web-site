using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gallery.Models;
using Gallery.Data;
using Microsoft.EntityFrameworkCore;

namespace Gallery.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MyContext _context;

    public HomeController(ILogger<HomeController> logger,MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
         var productsWithImages = _context.products.Include(p => p.Images).ToList();
        return View(productsWithImages);
    }



    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
