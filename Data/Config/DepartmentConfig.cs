using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data.Config
{
    public class DepartmentConfig : IEntityTypeConfiguration<Department>
    {

        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Departments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(n => n.DepartmentName).IsRequired().HasMaxLength(200);
            builder.Property(n => n.Description).IsRequired(false).HasMaxLength(500);
            builder.HasData(new List<Department>()
            {
                new Department
                {
                    Id = 1,
                    DepartmentName ="CSE",
                   Description="USA"
                },
                new Department
                {
                    Id = 2,
                    DepartmentName ="MECH",
                    Description="USA"

                }

            });


        }
    }
}
