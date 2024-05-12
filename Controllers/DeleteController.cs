using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gallery.Models;
using Gallery.Data;
using Gallery.Entity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gallery.Controllers;

public class DeleteController : Controller
{
    private readonly MyContext _context;

    public DeleteController(MyContext context)
    {
        _context = context;
    }


    [HttpGet]

    public async Task<IActionResult> Index(int id)
    {   
        if(HttpContext.Session.GetString("IsUserLoggedIn") == "true")
        {
                var product =await _context.products
                             .Include(p => p.Images)
                             .Include(x => x.Category)
                             .FirstOrDefaultAsync(x => x.ProductId == id);

                ViewBag.Categories = new SelectList(_context.categories.ToList(), "CategoryId", "CategoryName");

                if (product == null)
                {
                    return NotFound(); // Ürün bulunamazsa hata sayfasına yönlendirme yapabilirsiniz
                }
            
            return View(product);
        }else{
            return RedirectToAction("Index","Admin");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Index(int id,string cate)
    {

        if(HttpContext.Session.GetString("IsUserLoggedIn") == "true")
        {
            var product = await _context.products.FindAsync(id);
            
            if(product == null)
            {
                return NotFound();
            }
                try
                {                   
                    _context.Remove(product);
                    await _context.SaveChangesAsync();   
                }
                catch (System.Exception)
                {              
                    throw;
                }
            
            
            return RedirectToAction("AdminIndex","Admin");

        }else{
            return RedirectToAction("Index","Admin");
        }
           
    }


}
