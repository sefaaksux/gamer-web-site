using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gallery.Models;
using Gallery.Entity;
using Gallery.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;


namespace Gallery.Controllers;

public class AdminController : Controller
{
    private readonly MyContext _context;
    public AdminController(MyContext context)
    {
        _context = context;
    }

    
    public IActionResult Index()
    {
        return View();
    }
    
    
    [HttpPost]
    
    public async Task<IActionResult> Index(LoginViewModel model)
    {
        if(ModelState.IsValid)
        {
            if(!string.IsNullOrEmpty(model.userName) && !string.IsNullOrEmpty(model.password))
            {
                var user =await _context.users.AnyAsync(x => x.UserName == model.userName && x.Password == model.password);
                if(user)
                {              
                    HttpContext.Session.SetString("IsUserLoggedIn","true");
                    
                    return View("AdminIndex");
                }
                
            }
        }

        return View();
    }

   
    public async Task<IActionResult> AdminIndex()
    {
        string? Giris = HttpContext.Session.GetString("IsUserLoggedIn");  
        if(Giris != null)
        {
            
            if(Giris == "true")
            {               
                
                 return View();
            }else{
                return RedirectToAction("Index");
            }
        }else{
            return RedirectToAction("Index");
        }        
    }
   

    public async Task<IActionResult> Menu(string category)
    {
        if(HttpContext.Session.GetString("IsUserLoggedIn") == "true")
        {
            ViewBag.category = category;
        
            if(category == "hepsi")
            {
                var allProduct = await _context.products.ToListAsync();
                return View(allProduct);
            }else if(category == "kokteyl")
            {
                // var filteredProduct = await _context.products
                //                             .Include(x => x.Tablo)
                //                             .Where(x => x.tabloId == 17 || x.tabloId == 25)
                //                             .ToListAsync();
                return View();
            }
            else{
                // var filteredProduct = await _context.urunler
                //                             .Include(x => x.Tablo)
                //                             .Where(x => x.Tablo.tabloName == category)
                //                             .ToListAsync();
                return View();
            }
        }else{
            return View("Index");
        }
      
    }  

    [HttpGet]
    public IActionResult GetTablolarByKategoriId(int kategoriId)
    {
         var tablolar = _context.categories.Where(t => t.CategoryId == kategoriId).ToList();
    
        // JSON olarak tabloları dön
        return Json(tablolar);
    }

   

    

}
