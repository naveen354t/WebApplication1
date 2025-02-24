﻿using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Config;

namespace WebApplication1.Data
{
    public class CollegeDBContext : DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext>options):base(options)
        {
            
        }
        DbSet<Student> Students { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Tabel-1
           modelBuilder.ApplyConfiguration(new StudentConfig());
            //Tabel-2 

        }

    }
}
