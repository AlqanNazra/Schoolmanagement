using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Modules.Students;
using System.Threading.Tasks;
using SchoolManagementSystem.Modules.Students.Services;
using SchoolManagementSystem.Common.Requests;
using SchoolManagementSystem.Common.Responses;
using Microsoft.AspNetCore.Authorization;

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

        [Authorize(Roles = "Admin,guru")]
        [HttpGet("paged")]
        public async Task<ActionResult<PagedResponse<List<StudentDto>>>> GetStudents([FromQuery] PaginationParams paginationParams)
        {
            try
            {
                var response = await _studentService.GetAllStudentsAsync(paginationParams);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Server error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin,guru")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _studentService.GetStudentByIdAsync(id);
            if (student == null) return NotFound();
            return Ok(student);
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _studentService.DeleteStudentAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}