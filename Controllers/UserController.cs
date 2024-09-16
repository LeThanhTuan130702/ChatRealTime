using System.Security.Claims;
using ChatRealTime.Data;
using ChatRealTime.Helper;
using ChatRealTime.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using ThuongMaiDienTu.Helper;

namespace ChatRealTime.Controllers
{
    
    public class UserController : Controller
    {
      private readonly  ChatAppContext _context;
        public UserController(ChatAppContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.SingleOrDefault(x => x.Email==model.Email);
                if(user==null)
                {
                    ModelState.AddModelError("lõi", "Người dùng không tồn tại");
                }
                else
                {
                    if(user.PasswordHash!=model.PasswordHash.ToSHA256String())
                    {
                        ModelState.AddModelError("lõi", "sai Email hoặc mật khẩu");

                    }
                    else
                    {
                        var claims = new List<Claim>()
                            {
                                new Claim(ClaimTypes.Email, user.Email),
                                new Claim(ClaimTypes.Name, user.Username),
                                new Claim("CUSTOMERID", user.Id.ToString()),
								// động
								new Claim(ClaimTypes.Role, "User"),
                            };
                        user.Status = 1;
                        _context.SaveChanges();
                        var claimsIdentity = new ClaimsIdentity(claims, "login");
                        var claimsPrinciple = new ClaimsPrincipal(claimsIdentity);
                        HttpContext.Session.Set<string>("UserEmail",user.Email);
                        await HttpContext.SignInAsync(claimsPrinciple);
                        return RedirectToAction("Index", "Home");
                    }
                }
            }    


                return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> LogOutAsync()
        {
            var emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            var user = _context.Users.FirstOrDefault(u => u.Email == emailClaim.Value.ToString());
            HttpContext.Session.Remove("UserEmail");
            user.Status = 0;
            _context.SaveChanges();
            await  HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Register(RegisterVM model, IFormFile Image)
        {
            if(ModelState.IsValid)
            {
                var CheckEmail = _context.Users.FirstOrDefault(x => x.Email == model.Email);
                if (CheckEmail != null)
                {
                    TempData["Message"] = "Email đã tồn tại ";
                    return View();
                }
                var user = new User();
                user.Username = model.fname + " " + model.lname;
                user.PasswordHash = model.Password.ToSHA256String();
                user.CreatedAt = DateTime.Now;
                user.Email = model.Email;
                user.Status = 1;
                user.UniqueId = decimal.Parse(Util.GenerateRandomNumber());
                user.Img = Util.UploadImage(Image, "Image");
                try
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                }
                catch (Exception ex) {
                    TempData["Message"] = "Lỗi dữ liệu";
                    return View();

                }

                TempData["Message"] = "Tạo thành công ";
                return RedirectToAction("Login");
            }    
            return View();
          

           
        }
    }
}
