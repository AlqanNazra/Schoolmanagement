using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Modules.Classes.Dtos;
using SchoolManagementSystem.Modules.Classes.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Modules.Classes.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassController : ControllerBase
    {
        private readonly IClassService  _classService;

        public ClassController(IClassService  classService)
        {
            _classService = classService;
        }

        // GET: api/class
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KelasDto>>> GetAllClasses()
        {
            try
            {
                var classes = await _classService.GetAllClassesAsync();
                return Ok(classes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Kesalahan server: {ex.Message}");
            }
        }

        // GET: api/class/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<KelasDto>> GetClassById(int id)
        {
            try
            {
                var kelas = await _classService.GetClassByIdAsync(id);
                return Ok(kelas);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Kesalahan server: {ex.Message}");
            }
        }

        // POST: api/class
        [HttpPost]
        public async Task<ActionResult<KelasDto>> CreateClass([FromBody] KelasDto kelasDto)
        {
            try
            {
                var createdClass = await _classService.CreateClassAsync(kelasDto);
                return CreatedAtAction(nameof(GetClassById), new { id = createdClass.id_kelas }, createdClass);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Kesalahan server: {ex.Message}");
            }
        }

        // PUT: api/class/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<KelasDto>> UpdateClass(int id, [FromBody] KelasDto kelasDto)
        {
            try
            {
                var updatedClass = await _classService.UpdateClassAsync(id, kelasDto);
                return Ok(updatedClass);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Kesalahan server: {ex.Message}");
            }
            
        }

        // DELETE: api/class/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteClass(int id)
        {
            try
            {
                var result = await _classService.DeleteClassAsync(id);
                if (!result)
                {
                    return NotFound($"Kelas dengan ID {id} tidak ditemukan.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Kesalahan server: {ex.Message}");
            }
        }

        // POST: api/class/{id_kelas}/assign-teacher
        [HttpPost("{id_kelas}/assign-teacher")]
        public async Task<ActionResult<KelasDto>> AssignTeacher(int id_kelas, [FromBody] AssignTeacherDto assignTeacherDto)
        {
            try
            {
                var updatedClass = await _classService.AssignTeacherAsync(id_kelas, assignTeacherDto.id_guru);
                return Ok(updatedClass);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Kesalahan server: {ex.Message}");
            }
        }

        // POST: api/class/{id_kelas}/unassign-teacher
        [HttpPost("{id_kelas}/unassign-teacher")]
        public async Task<ActionResult<KelasDto>> UnassignTeacher(int id_kelas)
        {
            try
            {
                var updatedClass = await _classService.UnassignTeacherAsync(id_kelas);
                return Ok(updatedClass);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Kesalahan server: {ex.Message}");
            }
        }
    }

    public class AssignTeacherDto
    {
        public int id_guru { get; set; }
    }
}