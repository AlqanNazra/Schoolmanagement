using SchoolManagementSystem.Modules.Students.Entities;
using SchoolManagementSystem.Modules.Classes.Entities;

namespace SchoolManagementSystem.Modules.Enrollments.Entities
{
    public class Pendaftaran
    {
        public int id_enrollment { get; set; }
        public int id_student { get; set; }
        public int id_kelas { get; set; }
        public DateTime waktu_pendaftaran { get; set; } = DateTime.Now;
        public Murid Murid { get; set; }
        public Kelas kelas { get; set; }
    }
}