using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SignalRWithEntityFramework.Models;

public partial class SignalRnotificationDbContext : DbContext
{
    public SignalRnotificationDbContext()
    {
    }

    public SignalRnotificationDbContext(DbContextOptions<SignalRnotificationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<HubConnection> HubConnections { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HubConnection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HubConne__3214EC07095D2E63");

            entity.ToTable("HubConnection");

            entity.Property(e => e.ConnectionId)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Notifica__3214EC071EB79202");

            entity.ToTable("Notification");

            entity.Property(e => e.Message).IsUnicode(false);
            entity.Property(e => e.MessageType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NotificationDateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Password)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
