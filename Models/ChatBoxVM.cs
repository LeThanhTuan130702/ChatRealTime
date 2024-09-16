namespace ChatRealTime.Models
{
    public class ChatBoxVM
    {
        
        public int? UserId {  get; set; }
        public int? SenderId { get; set; }

        public int? ReceiverId { get; set; }

        public string? Content { get; set; }

        public DateTime? SentAt { get; set; }
    }
}
