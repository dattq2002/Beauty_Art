using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Services.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Price)
                   .HasColumnType("money");

            builder.HasOne(s => s.Category)
                .WithMany(s => s.Courses)
                .HasForeignKey(fk => fk.CategoryId);
        }
    }

    
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.TotalPrice)
                   .HasColumnType("money");
            builder.HasOne(s => s.User)
                   .WithMany(s => s.Orders)
                   .HasForeignKey(fk => fk.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
    public class CategoryConfig : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);

        }
    }
    public class ChapterConfig : IEntityTypeConfiguration<Chapter>
    {
        public void Configure(EntityTypeBuilder<Chapter> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(s => s.Course)
                   .WithMany(s => s.Chapters)
                   .HasForeignKey(fk => fk.CourseId);

        }
    }
    public class WalletConfig : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(s => s.User)
                   .WithOne(s => s.Wallet)
                   .HasForeignKey<User>(fk => fk.WalletId);

        }
    }
    public class PaymentConfig : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(s => s.Order)
                   .WithOne(s => s.Payment)
                   .HasForeignKey<Order>(fk => fk.PaymentId);

        }
    }
    public class UserCourseConfig : IEntityTypeConfiguration<UserCourse>
    {
        public void Configure(EntityTypeBuilder<UserCourse> builder)
        {
            builder.HasKey(x => x.Id);


            builder.HasOne(s => s.User)
                   .WithMany(s => s.UserCourses)
                   .HasForeignKey(fk => fk.UserId);
            builder.HasOne(s => s.Course)
                   .WithMany(s => s.UserCourses)
                   .HasForeignKey(fk => fk.CourseId);

        }
    }


    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Email).IsUnique();
        }
    }
}
