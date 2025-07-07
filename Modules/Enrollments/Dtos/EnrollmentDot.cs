using System.Collections.Generic;
using SchoolManagementSystem.Modules.Enrollments.Entities;
using SchoolManagementSystem.Modules.Students.Entities;
using SchoolManagementSystem.Modules.Classes.Entities;
namespace SchoolManagementSystem.Modules.Enrollments.Dtos
{
    public class EnrollmentDto
    {
    public int id_enrollment { get; set; }
    public int id_student { get; set; }
    public int id_kelas { get; set; }
    }
}