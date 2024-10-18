using Microsoft.EntityFrameworkCore;
using WebApplication1.Data.Config;

namespace WebApplication1.Data
{
    public class CollegeDBContext : DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext>options):base(options)
        {
            
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Tabel-1 good way of implementing
           modelBuilder.ApplyConfiguration(new StudentConfig());
            //Tabel-2 
            modelBuilder.ApplyConfiguration(new DepartmentConfig());

        }

    }
}
