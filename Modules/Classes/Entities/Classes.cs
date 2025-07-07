using System.Collections.Generic;
using SchoolManagementSystem.Modules.Teachers.Entities;
using SchoolManagementSystem.Modules.Enrollments.Entities;

namespace SchoolManagementSystem.Modules.Classes.Entities
{
    public class Kelas
    {
        public int id_kelas { get; set; }
        
        public required string nama_kelas { get; set; }
        public int? id_guru { get; set; }

        // Guru utama (nullable, one-to-many)
        public required Guru GuruUtama { get; set; }

        // Banyak guru mengajar kelas ini (many-to-many)
        public ICollection<Guru> Pengajar { get; set; } = new List<Guru>();

        public ICollection<Pendaftaran> Enrollments { get; set; } = new List<Pendaftaran>();
    }
}
