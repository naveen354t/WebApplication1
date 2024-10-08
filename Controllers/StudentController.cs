using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController:ControllerBase
    {
        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            var students = CollegeRepository.Students.Select(s => new StudentDTO()
            {
                Id = s.Id,
                StudentName = s.StudentName,
                Address = s.Address,
                Email = s.Email
            });
            //Ok-200 -success
            return Ok(students);
               
        }
        [HttpGet]
        [Route("{id:int}",Name ="GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            //BadRequest-400 -Client error 
            if (id <= 0)
                return BadRequest();
            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
                return NotFound($"The student with id {id} not found");

            var studentDTO = new StudentDTO
            {
                Id=student.Id,
                StudentName=student.StudentName,
                Address=student.Address,
                Email=student.Email
            };
            //Ok-200 -success
            return Ok(studentDTO);
        }
        [HttpGet("{name:alpha}", Name = "DeleteStudentByName")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();
            var student = CollegeRepository.Students.Where(n => n.StudentName == name).FirstOrDefault();

            if (student == null)
                return NotFound($"The student with name {name} not found");
            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Address = student.Address,
                Email = student.Email
            };
            //Ok-200 -success
            return Ok(studentDTO);
            
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> CreateStudent([FromBody]StudentDTO model)
        {
            if (model==null)
                return BadRequest();
            int id = CollegeRepository.Students.LastOrDefault().Id + 1;
            Student student = new Student
            {
                Id = id,
                StudentName = model.StudentName,
                Address = model.Address,
                Email = model.Email
            };
            CollegeRepository.Students.Add(student);
            model.Id=student.Id;
            return CreatedAtRoute("GetStudentById",new { id=model.Id },model);
        }

        [HttpPut]
        [Route("Update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudent( [FromBody] StudentDTO model)
        {
            if(model==null || model.Id<=0)
                return BadRequest();
            var existingStudent= CollegeRepository.Students.Where(S=>S.Id==model.Id).FirstOrDefault();
            if(existingStudent==null)
                return NotFound();
            existingStudent.StudentName = model.StudentName;
            existingStudent.Address = model.Address;
            existingStudent.Email = model.Email;
            return NoContent();

        }

        [HttpDelete("{id:int}",Name ="DeleteStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteStudent(int id)
        {
            //BadRequest-400 -Client error 
            if (id <= 0)
                return BadRequest();

            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
                return NotFound($"The student with id {id} not found");
            CollegeRepository.Students.Remove(student);
            //Ok-200 -success
            return Ok(true);

        }

    }
}

