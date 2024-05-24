using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Gallery.Models;
using Gallery.Entity;
using Gallery.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gallery.Controllers;

public class CreateController : Controller
{
    private readonly ILogger<CreateController> _logger;
    private readonly MyContext _context;

    public CreateController(ILogger<CreateController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        ViewBag.Categories = new SelectList(_context.categories.ToList(),"CategoryId","CategoryName");
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ProductViewModel model)
    {
        ViewBag.Categories = new SelectList(_context.categories.ToList(),"CategoryId","CategoryName");
        if (ModelState.IsValid)
        {
            var product = new Product
            {
                Name = model.Name,
                Description = model.Description,
                Amount = model.Amount,  
                CategoryId = model.CategoryId,
                // Diğer özellikler
            };

            // Video dosyasını işle
            if (model.VideoFile != null)
            {
                var videoName = Guid.NewGuid().ToString() + Path.GetExtension(model.VideoFile.FileName);
                var videoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/videos", videoName);
                using (var stream = new FileStream(videoPath, FileMode.Create))
                {
                    await model.VideoFile.CopyToAsync(stream);
                }
                product.VideoUrl = "/videos/" + videoName; // Video dosyasının URL'sini atama
            }

            // Resim dosyalarını işle
            product.Images = model.Images.Select(image =>
            {
                var imageName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    image.CopyTo(stream);
                }
                return new Image { Url = "/images/" + imageName };
            }).ToList();

            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        else
        {
            return View();
        }
    }


    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
