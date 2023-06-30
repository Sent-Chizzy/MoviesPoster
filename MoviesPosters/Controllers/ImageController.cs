using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesPosters.Data;
using MoviesPosters.Models;

namespace MoviesPosters.Controllers
{
    [Authorize]
    public class ImageController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly INotyfService _notyfService;

        public ImageController(UserManager<ApplicationUser> userManager, 
                            ApplicationDbContext context, 
                            INotyfService notyfService)
        {
            _userManager = userManager;
            _context = context;
            _notyfService= notyfService;
        }
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
