using System;
using System.Collections.Generic;

namespace ChatRealTime.Data;

public partial class User
{
    public int Id { get; set; }

    public decimal UniqueId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? Img { get; set; }

    public int Status { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Message> MessageReceivers { get; set; } = new List<Message>();

    public virtual ICollection<Message> MessageSenders { get; set; } = new List<Message>();
}
