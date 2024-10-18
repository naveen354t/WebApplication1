using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using WebApplication1.Data;
using WebApplication1.Data.Repository;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController:ControllerBase
    {
        //private readonly ILogger<StudentController> _logger;
        //private readonly CollegeDBContext _dbContext;
        private readonly ICollegeRepository<Student> _studentReopository;
        private readonly IMapper _mapper;
        public StudentController(IMapper mapper, ICollegeRepository<Student> studentReopository)
        {
            //_logger = logger;
            //_dbContext = dBContext;
            _mapper = mapper;
            _studentReopository = studentReopository;
        }

        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task< ActionResult<IEnumerable<StudentDTO>>> GetStudents()
        {
            //var students = _dbContext.Students;
            //var students =await _dbContext.Students.ToListAsync();
            var students=await _studentReopository.GetAllAsync();

            var studentDTOData=_mapper.Map<List<StudentDTO>>(students);
            //Ok-200 -success
            return Ok(students);
               
        }
        [HttpGet]
        [Route("{id:int}",Name ="GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> GetStudentById(int id)
        {
            //BadRequest-400 -Client error 
            if (id <= 0)
                return BadRequest();
            //var student = await _dbContext.Students.Where(n => n.Id == id).FirstOrDefaultAsync();
            var student=await _studentReopository.GetAsync(student=>student.Id==id);
            if (student == null)
                return NotFound($"The student with id {id} not found");

            //var studentDTO = new StudentDTO
            //{
            //    Id=student.Id,
            //    StudentName=student.StudentName,
            //    Address=student.Address,
            //    Email=student.Email,
            //    DOB=student.DOB.ToShortDateString()
            //};
            var studentDTO=_mapper.Map<StudentDTO>(student);
            //Ok-200 -success
            return Ok(studentDTO);
        }
        [HttpGet("{name:alpha}", Name = "DeleteStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> GetStudentByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();
            //var student = await _dbContext.Students.Where(n => n.StudentName == name).FirstOrDefaultAsync();
            var student= await _studentReopository.GetAsync(student=>student.StudentName.ToLower().Contains(name.ToLower()));
            if (student == null)
                return NotFound($"The student with name {name} not found");
            //var studentDTO = new StudentDTO
            //{
            //    Id = student.Id,
            //    StudentName = student.StudentName,
            //    Address = student.Address,
            //    Email = student.Email,
            //    DOB = student.DOB.ToShortDateString()
            //};
            var studentDTO = _mapper.Map<StudentDTO>(student);

            //Ok-200 -success
            return Ok(studentDTO);
            
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDTO>> CreateStudent([FromBody]StudentDTO dto)
        {
            if (dto==null)
                return BadRequest();
            //int id = _dbContext.Students.LastOrDefault().Id + 1;
            //Student student = new Student
            //{
            //    //Id = id,
            //    StudentName = model.StudentName,
            //    Address = model.Address,
            //    Email = model.Email
            //};
            Student student=_mapper.Map<Student>(dto);
            var studentAfterCreation=await _studentReopository.CreateAsync(student);
            
            
            //await _dbContext.Students.AddAsync(student);
            //await _dbContext.SaveChangesAsync();
            dto.Id=studentAfterCreation.Id;
            return CreatedAtRoute("GetStudentById",new { id=dto.Id },dto);
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateStudent( [FromBody] StudentDTO dto)
        {
            if(dto==null || dto.Id<=0)
                return BadRequest();
            var existingStudent=await _studentReopository.GetAsync(student=>student.Id==dto.Id,true);
            //var existingStudent= await _dbContext.Students.AsNoTracking().Where(S=>S.Id==dto.Id).FirstOrDefaultAsync();
            if(existingStudent==null)
                return NotFound();
            //var newRecord = new Student()
            //{
            //    Id =existingStudent.Id,
            //    StudentName = model.StudentName,
            //    Address = model.Address,
            //    Email = model.Email,
            //    DOB = Convert.ToDateTime(model.DOB)
            //};
            var newRecord = _mapper.Map<Student>(dto);
            //_dbContext.Students.Update(newRecord);
            //existingStudent.StudentName = model.StudentName;
            //existingStudent.Address = model.Address;
            //existingStudent.Email = model.Email;
            //existingStudent.DOB=Convert.ToDateTime(model.DOB);
           //await _dbContext.SaveChangesAsync();
           await _studentReopository.UpdateAsync(newRecord);
            return NoContent();

        }

        [HttpDelete("{id:int}",Name ="DeleteStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteStudent(int id)
        {
            //BadRequest-400 -Client error 
            if (id <= 0)
                return BadRequest();

            //var student = await _dbContext.Students.Where(n => n.Id == id).FirstOrDefaultAsync();
            var studentId=await _studentReopository.GetAsync(Student=>Student.Id==id);
            if (studentId == null)
            {
                return NotFound($"The student with id {id} not found");
            }
            //_dbContext.Students.Remove(student);
            //await _dbContext.SaveChangesAsync();
            //Ok-200 -success
            var student = await _studentReopository.DeleteAsync(studentId);
            return Ok(true);

        }

    }
}

