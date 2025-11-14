using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using TELpro.DATA;
using TELpro.Models;

namespace TELpro.Controllers
{
    public class AdminProductController1 : Controller
    {
        private readonly AppDbContext _appDbContext;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminProductController1(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            var products = _appDbContext.Products.ToList();
            return View(products);
          
        }
        public async Task<IActionResult> Edit(int Id)
        {

            var pro = await _appDbContext.Products.FindAsync(Id);
            return View(pro);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var product = await _appDbContext.Products.FindAsync(Id);
            return View(product);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            // If new image uploaded
            if (product.ImageFile != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(product.ImageFile.FileName);
                string filePath = Path.Combine("wwwroot/images/", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(stream);
                }

                product.ImagePath = "images/" + fileName;
            }

            _appDbContext.Products.Update(product);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }




        [HttpGet]
        public IActionResult Create()
        {
           return View();
        }
        [HttpPost]
      
        public async Task<IActionResult>  Create(Product _product)
        {

            // custom validation
            if (_product.Name?.Trim().ToLower() == "test")
            {
                ModelState.AddModelError("Name", "Product name cannot be 'test'");
            }
            // server validation
            if (_product.ImageFile != null && _product.ImageFile.Length > 0)
            {
                // 1. Get path to wwwroot
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                // 2. Get filename without extension and replace spaces
                string fileNameWithoutExt = Path.GetFileNameWithoutExtension(_product.ImageFile.FileName)
                                                .Replace(" ", "_");

                // 3. Get extension (.jpg, .png, etc.)
                string extension = Path.GetExtension(_product.ImageFile.FileName);

                // 4. Create a unique filename
                string uniqueFilename = $"{fileNameWithoutExt}_{Guid.NewGuid():N}{extension}";

                // 5. Create folder path wwwroot/images
                string imageFolder = Path.Combine(wwwRootPath, "images");

                // 6. Create folder if not exists
                if (!Directory.Exists(imageFolder))
                {
                    Directory.CreateDirectory(imageFolder);
                }

                // 7. Full physical path where file will be saved
                string filePath = Path.Combine(imageFolder, uniqueFilename);

                // 8. Copy uploaded file into the folder
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    _product.ImageFile.CopyTo(fileStream);
                }

                // 9. Save filename to database Property
                /*_product.ImagePath = uniqueFilename;*/
                _product.ImagePath = "/images/" + uniqueFilename;

            }
            if (ModelState.IsValid)
            {
                // 10. Save Product into database
                _appDbContext.Products.Add(_product);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Create");

            }
        // 11. Redirect to List Page
            return RedirectToAction("Create");


         
        }

    }
}
