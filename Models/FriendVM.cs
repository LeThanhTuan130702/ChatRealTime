namespace ChatRealTime.Models
{
    public class FriendVM
    {

        public int Id { get; set; }
        public string Username { get; set; }
        public string img { get; set; }
        public string message { get; set; }
        public int status {  get; set; }
        public DateTime SentAt { get; set; }

    }
}
