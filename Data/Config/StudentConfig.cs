﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApplication1.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {

        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(n => n.StudentName).IsRequired().HasMaxLength(250);
            builder.Property(n => n.Address).IsRequired(false).HasMaxLength(500);
            builder.Property(n => n.Email).IsRequired().HasMaxLength(250);
            builder.HasData(new List<Student>()
            {
                new Student
                {
                    Id = 1,
                    StudentName = "naveenThadigadapa",
                    Address="USA",
                    Email="naveen@tezo.com",
                    DOB=new DateTime(2015, 12, 31)


                },
                new Student
                {
                    Id = 2,
                    StudentName = "naveen",
                    Address="IND",
                    Email="naveen@gmail.com",
                    DOB=new DateTime(2015, 12, 3)

                }

            });

            builder.HasOne(n=>n.Department).WithMany(n=>n.Students).HasForeignKey(n => n.DepartmentId).HasConstraintName("FK_Students_Department1");
        }
    }
}
