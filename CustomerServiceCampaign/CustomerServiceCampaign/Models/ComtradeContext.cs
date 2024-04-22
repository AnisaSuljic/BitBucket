using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CustomerServiceCampaign.Models;

public partial class ComtradeContext : DbContext
{
    public ComtradeContext()
    {
    }

    public ComtradeContext(DbContextOptions<ComtradeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agent> Agents { get; set; }

    public virtual DbSet<Campaign> Campaigns { get; set; }

    public virtual DbSet<RewardedCustomer> RewardedCustomers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=Comtrade;Trusted_Connection=True;Encrypt=False;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Agent__3214EC07430FDBD1");

            entity.ToTable("Agent");

            entity.Property(e => e.FirstName).HasMaxLength(255);
            entity.Property(e => e.LastName).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PasswordSalt).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(255);
        });

        modelBuilder.Entity<Campaign>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Campaign__3214EC0794E5592F");

            entity.ToTable("Campaign");

            entity.Property(e => e.CampaignName).HasMaxLength(255);
        });

        modelBuilder.Entity<RewardedCustomer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rewarded__3214EC0701848A0F");

            entity.ToTable("RewardedCustomer");

            entity.HasIndex(e => e.AgentId, "IX_RewardedCustomer_AgentId");

            entity.HasIndex(e => e.CampaignId, "IX_RewardedCustomer_CampaignId");

            entity.Property(e => e.Ssn)
                .HasMaxLength(255)
                .HasColumnName("SSN");

            entity.HasOne(d => d.Agent).WithMany(p => p.RewardedCustomers)
                .HasForeignKey(d => d.AgentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RewardedCustomer_AgentId_FK");

            entity.HasOne(d => d.Campaign).WithMany(p => p.RewardedCustomers)
                .HasForeignKey(d => d.CampaignId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("RewardedCustomer_CampaignId_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
