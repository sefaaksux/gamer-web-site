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

    public IActionResult Menu(string category)
    {
        if(category != null)
        {
            if(category == "uc")
            {
                var Product = _context.products.Where(x => x.Category.CategoryId == 2).Include(x => x.Images).ToList();
                return View(Product);
            }else if(category == "hesap"){
                var Product = _context.products.Where(x => x.Category.CategoryId == 1).Include(x => x.Images).ToList();
                return View(Product);
            }
        }

        return View();
    }

    
    public IActionResult Details(int id)
    {
        var product = _context.products.Include(p => p.Images).FirstOrDefault(p => p.ProductId == id);
        if (product == null)
        {
            return NotFound();
        }
        
        if(product.CategoryId == 1)
        {
            ViewBag.Category = "masa";
        }else if(product.CategoryId == 2)
        {
            ViewBag.Category = "sandalye";
        }

        return View(product);
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
