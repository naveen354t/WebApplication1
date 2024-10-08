using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CollegeRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>()
        {
            new Student
            {
                Id=1,
                StudentName="Student1",
                Email="Student@gmail.com",
                Address="sddsd"
            },
            new Student
            {
                Id=2,
                StudentName="Student2",
                Email="Student@gmail.com",
                Address="sddsd"
            },new Student
            {
                Id=3,
                StudentName="naveen",
                Email="Student@gmail.com",
                Address="sddsd"
            }
        };

    }
}
