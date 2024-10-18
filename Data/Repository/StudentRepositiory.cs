
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data.Repository
{
    public class StudentRepositiory : CollegeRepository<Student>,IStudentReopository
    {
        private readonly CollegeDBContext _dbContext;
        public StudentRepositiory(CollegeDBContext dBContext):base(dBContext)
        {
            _dbContext = dBContext;
        }

        public Task<List<Student>> GetStudentsByFeeStatusAsync(int feeStatus)
        {
            //throw new NotImplementedException();
            return null;
        }
    }
}
