using Microsoft.AspNetCore.Mvc;
using SchoolManagementSystem.Modules.Enrollments.Dtos;
using SchoolManagementSystem.Modules.Enrollments.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagementSystem.Modules.Enrollments.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly EnrollmentServices _enrollmentService;

        public EnrollmentController(EnrollmentServices enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        // GET: api/enrollment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnrollmentDto>>> GetAllEnrollments()
        {
            try
            {
                var enrollments = await _enrollmentService.GetAllEnrollmentsAsync();
                return Ok(enrollments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/enrollment/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EnrollmentDto>> GetEnrollmentById(int id)
        {
            try
            {
                var enrollment = await _enrollmentService.GetEnrollmentByIdAsync(id);
                return Ok(enrollment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/enrollment
        [HttpPost]
        public async Task<ActionResult<EnrollmentDto>> CreateEnrollment([FromBody] EnrollmentDto enrollmentDto)
        {
            try
            {
                // Check for duplicate enrollment
                var existingEnrollments = await _enrollmentService.ExistAsync(enrollmentDto.id_student, enrollmentDto.id_kelas);
                if (existingEnrollments != null && existingEnrollments.Any())
                {
                    return BadRequest("Student is already enrolled in this class.");
                }

                var createdEnrollment = await _enrollmentService.CreateEnrollmentAsync(enrollmentDto);
                return CreatedAtAction(nameof(GetEnrollmentById), new { id = createdEnrollment.id_enrollment }, createdEnrollment);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/enrollment/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<EnrollmentDto>> UpdateEnrollment(int id, [FromBody] EnrollmentDto enrollmentDto)
        {
            try
            {
                // Check for duplicate enrollment when updating
                var existingEnrollments = await _enrollmentService.ExistAsync(enrollmentDto.id_student, enrollmentDto.id_kelas);
                if (existingEnrollments != null && existingEnrollments.Any(e => e.id_enrollment != id))
                {
                    return BadRequest("Student is already enrolled in this class.");
                }

                var updatedEnrollment = await _enrollmentService.UpdateEnrollmentAsync(id, enrollmentDto);
                return Ok(updatedEnrollment);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/enrollment/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEnrollment(int id)
        {
            try
            {
                var result = await _enrollmentService.DeleteEnrollmentAsync(id);
                if (!result)
                {
                    return NotFound($"Enrollment with ID {id} not found.");
                }
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }

    // DTO for enroll request
    public class EnrollRequestDto
    {
        public int id_student { get; set; }
        public int id_kelas { get; set; }
    }
}