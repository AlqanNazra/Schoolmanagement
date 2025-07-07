using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Modules.Students;
using System.Threading.Tasks;
using SchoolManagementSystem.Modules.Students.Repositories;

namespace SchoolManagementSystem.Modules.Students.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentService.GetAllStudentsAsync();
            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Add(StudentDto studentDto)
        {
            if (studentDto == null)
            {
                return BadRequest("Student data is null.");
            }

            var student = await _studentService.AddStudentAsync(studentDto);
            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, StudentDto studentDto)
        {
            if (studentDto == null)
            {
                return BadRequest("Student data is null.");
            }

            var student = await _studentService.UpdateStudentAsync(id, studentDto);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _studentService.DeleteStudentAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}