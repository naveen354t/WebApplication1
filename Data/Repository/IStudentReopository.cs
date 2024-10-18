namespace WebApplication1.Data.Repository
{
    public interface IStudentReopository:ICollegeRepository<Student>
    {
       Task<List<Student>> GetStudentsByFeeStatusAsync(int feeStatus);
    }
}
