using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ChatRealTime.Data;

public partial class ChatAppContext : DbContext
{
    public ChatAppContext()
    {
    }

    public ChatAppContext(DbContextOptions<ChatAppContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<User> Users { get; set; }

   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("PK__Messages__C87C0C9C7EAE8444");

            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.SentAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Receiver).WithMany(p => p.MessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .HasConstraintName("FK__Messages__Receiv__52593CB8");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .HasConstraintName("FK__Messages__Sender__5165187F");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07CB71870B");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053410EE27F6").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Img)
                .HasMaxLength(255)
                .HasColumnName("img");
            entity.Property(e => e.PasswordHash).HasMaxLength(256);
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.UniqueId)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("unique_id");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
