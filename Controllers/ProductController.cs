using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gallery.Models;
using Gallery.Entity;
using Gallery.Data;
using Microsoft.EntityFrameworkCore;

namespace Gallery.Controllers;

public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly MyContext _context;

    public ProductController(ILogger<ProductController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    
    public IActionResult Details(int id)
    {
        var product = _context.products.Include(p => p.Images).FirstOrDefault(p => p.ProductId == id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
