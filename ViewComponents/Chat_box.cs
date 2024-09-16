using System.Security.Claims;
using ChatRealTime.Data;
using ChatRealTime.Models;
using Microsoft.AspNetCore.Mvc;
using ThuongMaiDienTu.Helper;

namespace ChatRealTime.ViewComponents
{
    public class Chat_box:ViewComponent
    {
        ChatAppContext _context;
        public Chat_box(ChatAppContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke(ChatRealTime.Data.User friend)
        {
            var emailClaim = UserClaimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            var user = _context.Users.FirstOrDefault(u => u.Email == emailClaim.Value.ToString());
            var list_message = _context.Messages.Where(x => x.SenderId == friend.Id || x.ReceiverId == friend.Id).Where(x=>x.SenderId==user.Id||x.ReceiverId==user.Id).Select(f=>new ChatBoxVM
            {
             UserId=user.Id,
             ReceiverId= f.ReceiverId == user.Id ? user.Id : friend.Id,
             SenderId = f.SenderId == user.Id ? user.Id : friend.Id,
             Content = f.Content,
             SentAt = f.SentAt??DateTime.Now
            }).OrderBy(m => m.SentAt).ToList();
            //if (list_message == null)
            //{
            //    return View(new ChatBoxVM
            //    {
            //        UserId = user.Id,
            //        ReceiverId = f.ReceiverId == user.Id ? user.Id : friend.Id,
            //        SenderId = f.SenderId == user.Id ? user.Id : friend.Id,
            //        Content = f.Content,
            //        SentAt = f.SentAt ?? DateTime.Now
            //    });
            //}
            ViewBag.fId=friend.Id;
            ViewBag.userId = user.Id;
            return View(list_message);
        }
    }
}
