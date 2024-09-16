using System.Diagnostics;
using System.Security.Claims;
using ChatRealTime.Data;
using ChatRealTime.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ChatRealTime.Helper;
using ThuongMaiDienTu.Helper;

namespace ChatRealTime.Controllers
{
    public class HomeController : Controller
    {
        private readonly ChatAppContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger ,ChatAppContext context)
        {
            _context = context;
            _logger = logger;
        }
        [Authorize]
        public IActionResult Index()
        {
            var emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            var Email = HttpContext.Session.Get<string>("UserEmail");
            var user=_context.Users.FirstOrDefault(u => u.Email == emailClaim.Value.ToString());
            return View(user);
        }
        [Authorize]
        public IActionResult Chat(int id)
        {
          var Users = _context.Users.FirstOrDefault(x => x.Id == id);
            return View(Users);
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetFriends(string search)
    {
            // Lọc danh sách bạn bè theo tên
            var friends = _context.Users
                .Where(f => f.Username.Contains(search))
                .Select(f => new {
                 f.Id,
                 f.Username,
                 f.Img
                })
                .ToList();

           return Ok(friends);
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
}
