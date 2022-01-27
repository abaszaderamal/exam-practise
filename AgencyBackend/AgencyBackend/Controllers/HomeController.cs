using AgencyBackend.Data.DAL;
using AgencyBackend.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AgencyBackend.Controllers
{

    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> IndexAsync()
        {
            HomeViewModel HomeVM = new HomeViewModel
            {
                Portfolios = await _context.Portfolios.Where(p => p.IsDeleted == false).ToListAsync()
            };
            return View(HomeVM);
        }
    }
}
