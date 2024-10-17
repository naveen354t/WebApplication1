
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Data.Repository
{
    public class StudentRepositiory : IStudentReopository
    {
        private readonly CollegeDBContext _dbContext;
        public StudentRepositiory(CollegeDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public async Task<int> CreateAsync(Student student)
        {
            _dbContext.Students.Add(student);
            await _dbContext.SaveChangesAsync();
            return student.Id;
        }

        public async Task<bool> DeleteAsync(Student student)
        {
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
            return true;


        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id, bool useNoTracking=false)
        {
            if (useNoTracking)
            {
                return await _dbContext.Students.AsNoTracking().Where(student => student.Id == id).FirstOrDefaultAsync();
            } else {
                return await _dbContext.Students.Where(student => student.Id == id).FirstOrDefaultAsync();
            }

        }

        public async Task<Student> GetByNameAsync(string name)
        {
            return await _dbContext.Students.Where(student => student.StudentName.ToLower().Contains(name.ToLower())).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAsync(Student student)
        {

            //var studentToUpdate=await _dbContext.Students.AsNoTracking().Where(student => student.Id == student.Id).FirstOrDefaultAsync();
            //if (studentToUpdate == null)
            //    throw new ArgumentNullException($"No Student found with id:{student.Id}");
            //studentToUpdate.StudentName=student.StudentName;
            //studentToUpdate.Address=student.Address;
            //studentToUpdate.Email=student.Email;
            //studentToUpdate.DOB = student.DOB;
             _dbContext.Update(student);
            
            await _dbContext.SaveChangesAsync();

            return student.Id;

            
        }
    }
}
