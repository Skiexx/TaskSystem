using Microsoft.EntityFrameworkCore;
using TaskSystem.Models;

namespace TaskSystem.Core
{
    public partial class ReconstructionContext : DbContext
    {
        public ReconstructionContext()
        {
        }

        public ReconstructionContext(DbContextOptions<ReconstructionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Task> Tasks { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=localhost;uid=skiexx;pwd=qweASD123;database=Reconstruction",
                    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8_general_ci")
                .HasCharSet("utf8mb3");

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.Title).HasMaxLength(100);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("Task");

                entity.HasIndex(e => e.CustomerId, "Task_Customer_FK");

                entity.HasIndex(e => e.PerformerId, "Task_Performer_FK");

                entity.HasIndex(e => e.StatusId, "Task_Status_FK");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.TaskCustomers)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_Customer_FK");

                entity.HasOne(d => d.Performer)
                    .WithMany(p => p.TaskPerformers)
                    .HasForeignKey(d => d.PerformerId)
                    .HasConstraintName("Task_Performer_FK");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Tasks)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_Status_FK");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.Login).HasMaxLength(100);

                entity.Property(e => e.MiddleName).HasMaxLength(100);

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.PhoneNumber).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}