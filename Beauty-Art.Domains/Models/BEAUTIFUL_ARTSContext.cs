using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Beauty_Art.Domains.Models
{
    public partial class BEAUTIFUL_ARTSContext : DbContext
    {
        public BEAUTIFUL_ARTSContext()
        {
        }

        public BEAUTIFUL_ARTSContext(DbContextOptions<BEAUTIFUL_ARTSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<CourseInOrder> CourseInOrders { get; set; } = null!;
        public virtual DbSet<Instructor> Instructors { get; set; } = null!;
        public virtual DbSet<InstructorInCourse> InstructorInCourses { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserAction> UserActions { get; set; } = null!;
        public virtual DbSet<UserInCourse> UserInCourses { get; set; } = null!;
        public virtual DbSet<UserWallet> UserWallets { get; set; } = null!;
        public virtual DbSet<WalletType> WalletTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=LAPTOP-R0K7KBGI\\TRANGQUOCDAT;Database=BEAUTIFUL_ARTS;User Id=sa;Password=12345;Encrypt=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ins_Date");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("Role_Id");

                entity.Property(e => e.UpsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ups_Date");

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("User_name");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Account_Role");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ins_Date");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.UpsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ups_Date");
            });

            modelBuilder.Entity<CourseInOrder>(entity =>
            {
                entity.ToTable("CourseInOrder");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CourseId).HasColumnName("Course_Id");

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ins_Date");

                entity.Property(e => e.OrderId).HasColumnName("Order_Id");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ups_Date");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseInOrders)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_CourseInOrder_Course");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.CourseInOrders)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_CourseInOrder_Order");
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.ToTable("Instructor");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("Date_of_birth");

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(50)
                    .HasColumnName("Image_url");

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ins_Date");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.UpsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ups_Date");
            });

            modelBuilder.Entity<InstructorInCourse>(entity =>
            {
                entity.ToTable("InstructorInCourse");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CourseId).HasColumnName("Course_Id");

                entity.Property(e => e.InstructorId).HasColumnName("Instructor_Id");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.InstructorInCourses)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_InstructorInCourse_Course");

                entity.HasOne(d => d.Instructor)
                    .WithMany(p => p.InstructorInCourses)
                    .HasForeignKey(d => d.InstructorId)
                    .HasConstraintName("FK_InstructorInCourse_Instructor");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ins_Date");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ups_Date");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Order_User");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DescriptionContent)
                    .HasMaxLength(500)
                    .HasColumnName("Description_content");

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ins_Date");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.UpsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ups_Date");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Post_User");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ins_Date");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ups_Date");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccountId).HasColumnName("Account_Id");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_of_birth");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .HasColumnName("Full_name");

                entity.Property(e => e.ImageUrl)
                    .IsUnicode(false)
                    .HasColumnName("Image_url");

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ins_Date");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Phone_number")
                    .IsFixedLength();

                entity.Property(e => e.UpsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ups_Date");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User_Account");
            });

            modelBuilder.Entity<UserAction>(entity =>
            {
                entity.ToTable("UserAction");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ins_Date");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.UpsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ups_Date");

                entity.Property(e => e.UserWalletId).HasColumnName("UserWallet_Id");

                entity.HasOne(d => d.UserWallet)
                    .WithMany(p => p.UserActions)
                    .HasForeignKey(d => d.UserWalletId)
                    .HasConstraintName("FK_UserAction_UserWallet");
            });

            modelBuilder.Entity<UserInCourse>(entity =>
            {
                entity.ToTable("UserInCourse");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AttendDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Attend_Date");

                entity.Property(e => e.CourseId).HasColumnName("Course_Id");

                entity.Property(e => e.ExpireDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Expire_Date");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.UserInCourses)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK_UserInCourse_Course");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserInCourses)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserInCourse_User");
            });

            modelBuilder.Entity<UserWallet>(entity =>
            {
                entity.ToTable("UserWallet");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ins_Date");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.UpsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ups_Date");

                entity.Property(e => e.UserId).HasColumnName("User_Id");

                entity.Property(e => e.WalletTypeId).HasColumnName("WalletType_Id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserWallets)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserWallet_User");

                entity.HasOne(d => d.WalletType)
                    .WithMany(p => p.UserWallets)
                    .HasForeignKey(d => d.WalletTypeId)
                    .HasConstraintName("FK_UserWallet_WalletType");
            });

            modelBuilder.Entity<WalletType>(entity =>
            {
                entity.ToTable("WalletType");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.InsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ins_Date");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpsDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Ups_Date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
