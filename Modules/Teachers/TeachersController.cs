using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Modules.Teachers.Dtos;
using SchoolManagementSystem.Modules.Teachers.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using SchoolManagementSystem.Modules.Teachers.Repositories;
using SchoolManagementSystem.Common.Requests;
using SchoolManagementSystem.Common.Responses;
using Microsoft.AspNetCore.Authorization;

namespace SchoolManagementSystem.Modules.Teachers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherRepo _teacherService;

        public TeachersController(ITeacherRepo teacherService)
        {
            _teacherService = teacherService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("paged")]
        public async Task<IActionResult> GetPaged([FromQuery] PaginationParams paginationParams)
        {
            var result = await _teacherService.GetAllTeachersAsync(paginationParams);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherDto>> GetTeacher(int id)
        {
            try
            {
                var teacher = await _teacherService.GetTeacherByIdAsync(id);
                return Ok(teacher);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<TeacherDto>> CreateTeacher([FromBody] TeacherDto teacherDto)
        {
            try
            {
                var createdTeacher = await _teacherService.CreateTeacherAsync(teacherDto);
                return CreatedAtAction(nameof(GetTeacher), new { id = createdTeacher.id_teacher }, createdTeacher);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<TeacherDto>> UpdateTeacher(int id, [FromBody] TeacherDto teacherDto)
        {
            try
            {
                var updatedTeacher = await _teacherService.UpdateTeacherAsync(id, teacherDto);
                return Ok(updatedTeacher);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeacher(int id)
        {
            var result = await _teacherService.DeleteTeacherAsync(id);
            if (!result)
            {
                return NotFound($"Teacher with ID {id} not found.");
            }
            return NoContent();
        }
    }
}