using System.Collections.Generic;
using SchoolManagementSystem.Modules.Enrollments.Entities;

namespace SchoolManagementSystem.Modules.Students.Entities
{
    public class Murid
    {
        public int id_student { get; set; }
        public required string nama { get; set; }
        public required string email { get; set; }
        public required string waktu_registrasi { get; set; }
        public ICollection<Pendaftaran> Enrollments { get; set; } = new List<Pendaftaran>();
    }
}