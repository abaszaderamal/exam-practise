using AgencyBackend.Data.DAL;
using AgencyBackend.Entities;
using AgencyBackend.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyBackend.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class PortfolioController : Controller
    {

        private AppDbContext _context;
        private IWebHostEnvironment _env;

        public PortfolioController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            HomeViewModel HomeVM = new HomeViewModel
            {
                Portfolios = await _context.Portfolios.Where(p => p.IsDeleted == false).ToListAsync()
            };
            return View(HomeVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(Portfolio portfolio)
        {
            if (ModelState["Photo"].ValidationState == ModelValidationState.Invalid) return View();
            if(portfolio.Photo.Length / 1024 > 200)
            {
                ModelState.AddModelError("Photo", "Photo Must be less than 200kb");
                return View();
            }
            if (!portfolio.Photo.ContentType.Contains("image/"))
            {
                ModelState.AddModelError("Photo", "Photo Must be image file type");
                return View();
            }

            string fileName = Guid.NewGuid().ToString() + " " + portfolio.Photo.FileName;
            string resultPath = Path.Combine(_env.WebRootPath, "assets/image", fileName);
            using (FileStream stream = new FileStream(resultPath,FileMode.Create))
            {
                await portfolio.Photo.CopyToAsync(stream);
            }
            portfolio.Image = fileName;
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id)
        {
            var dbPortfolio = _context.Portfolios.Find(id);
            if (dbPortfolio is null) return BadRequest();
            
            return View(dbPortfolio);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Update(int? id,Portfolio portfolio)
        {
            var dbPortfolio = _context.Portfolios.Find(id);
            if (dbPortfolio == null) return BadRequest();

            if(portfolio.Photo != null)
            {
                if(portfolio.Photo.Length / 1024 > 200)
                {
                    ModelState.AddModelError("Photo", "Photo Must Be Less Than 200Kb");
                    return View();
                }
                if (!portfolio.Photo.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("Photo", "File must be image file type");
                }

                string fileName = Guid.NewGuid().ToString() + " " + portfolio.Photo.FileName;
                string resultPath = Path.Combine(_env.WebRootPath, "assets/image", fileName);
                using(FileStream stream = new FileStream(resultPath, FileMode.Create))
                {
                    await portfolio.Photo.CopyToAsync(stream);
                }
                dbPortfolio.Image = fileName;
            }

            dbPortfolio.Title = portfolio.Title;
            dbPortfolio.Info = portfolio.Info;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var dbPortfolio = await _context.Portfolios.FindAsync(id);
            if (dbPortfolio == null) return BadRequest();

            dbPortfolio.IsDeleted = dbPortfolio.IsDeleted == false ? true : false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

    }
}
