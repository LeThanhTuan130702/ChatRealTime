using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using ChatRealTime.Data;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.Security.Claims;

public class ChatHub : Hub
{
	// Lưu trữ danh sách người dùng và kết nối của họ
	private static Dictionary<int, string> _connections = new Dictionary<int, string>();

	private readonly ChatAppContext _context; // Inject DbContext để thao tác với DB

	public ChatHub(ChatAppContext context)
	{
		_context = context;
		
	}
	public int LoadUser()
	{
        var emailClaim = Context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);
        var user = _context.Users.FirstOrDefault(u => u.Email == emailClaim.Value.ToString());
		return user != null ? user.Id : 0;
    }
		

	// Khi người dùng kết nối
	public override async Task OnConnectedAsync()
	{
       
        

        int userId = LoadUser();// Lấy userId từ Identity
		_connections[userId] = Context.ConnectionId; // Map userId với ConnectionId

		// Kiểm tra tin nhắn chưa đọc và gửi cho người dùng khi kết nối
		var unreadMessages = _context.Messages
			.Where(m => m.SenderId == userId && m.IsRead==false)
			.ToList();

		foreach (var message in unreadMessages)
		{
			await Clients.Caller.SendAsync("ReceiveMessage", message.SenderId, message.Content);
			message.IsRead = true; // Đánh dấu tin nhắn đã đọc
		}

		await _context.SaveChangesAsync(); // Cập nhật tin nhắn đã đọc vào DB

		await base.OnConnectedAsync();
	}

	// Khi người dùng ngắt kết nối
	public override async Task OnDisconnectedAsync(System.Exception exception)
	{
		int userId = LoadUser();
		_connections.Remove(userId); // Loại bỏ kết nối khi người dùng ngắt kết nối
		await base.OnDisconnectedAsync(exception);
	}

	// Gửi tin nhắn từ SenderId đến ReceiverId và lưu vào DB
	public async Task SendMessageToUser(int receiverId, string message)
	{
		int senderId = LoadUser();

		// Lưu tin nhắn vào DB
		var newMessage = new Message
		{
			SenderId = senderId,
			ReceiverId = receiverId,
			Content = message,
			SentAt = DateTime.Now,
			IsRead = false
		};

		_context.Messages.Add(newMessage);
		await _context.SaveChangesAsync();

		// Nếu người nhận đang kết nối, gửi tin nhắn ngay lập tức
		if (_connections.TryGetValue(receiverId, out string connectionId))
		{
			await Clients.Client(connectionId).SendAsync("ReceiveMessage", senderId, message);
			newMessage.IsRead = true; // Cập nhật trạng thái tin nhắn là đã đọc
			await _context.SaveChangesAsync();
		}
	}
}
