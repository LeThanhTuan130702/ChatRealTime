using System.Security.Claims;
using ChatRealTime.Data;
using ChatRealTime.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThuongMaiDienTu.Helper;

namespace ChatRealTime.ViewComponents
{
    public class User_list: ViewComponent
    {
        ChatAppContext _context;
        public  User_list(ChatAppContext context)
        { 
            _context=context;
        }

        public IViewComponentResult Invoke()
        {
            var emailClaim = UserClaimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
            var user = _context.Users.FirstOrDefault(u => u.Email == emailClaim.Value.ToString());
            var messages = _context.Messages.Include(m => m.Sender ).Include(m=>m.Receiver)
            .Where(m => (m.SenderId == user.Id || m.ReceiverId == user.Id)).Select(m => new FriendVM
            {
                Username = m.SenderId == user.Id ? m.Receiver.Username : m.Sender.Username, // Nếu người gửi là người dùng hiện tại, lấy người nhận và ngược lại
                message = m.Content,
                Id = m.SenderId == user.Id ? m.Receiver.Id : m.Sender.Id,
                img= m.SenderId == user.Id ? m.Receiver.Img : m.Sender.Img,
                status= m.SenderId == user.Id ? m.Receiver.Status : m.Sender.Status,
                SentAt = m.SentAt??DateTime.Now
            })
            .GroupBy(f=>f.Id).Select(g => g.OrderByDescending(m => m.SentAt).FirstOrDefault())
             // Loại trừ người dùng hiện tại
             // Chỉ lấy tin nhắn mới nhất của mỗi Id
            /* .OrderByDescending(m => m.SentAt) */// Sắp xếp theo thời gian gửi tin nhắn từ mới nhất
            .ToList();
			
            return View(messages);
        }
    }
}
