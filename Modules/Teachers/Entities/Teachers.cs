using System.Collections.Generic;
using SchoolManagementSystem.Modules.Classes.Entities;
using SchoolManagementSystem.Modules.Users.Entities;

namespace SchoolManagementSystem.Modules.Teachers.Entities
{
    public class Guru
    {
        public int id_teacher { get; set; }
        public required string nama_teacher { get; set; }
        public required string email_teacher { get; set; }
        public required string bidang { get; set; }

        // Guru utama untuk satu kelas
        public ICollection<Kelas> KelasUtama { get; set; } = new List<Kelas>();

        // Guru mengajar banyak kelas (many-to-many)
        public ICollection<Kelas> KelasDiajarkan { get; set; } = new List<Kelas>();

        // Relasi dengan User
        public User? User { get; set; }
        public int? userId { get; set; } // Foreign key untuk User
    }
}
