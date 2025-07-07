using System.Collections.Generic;
using SchoolManagementSystem.Modules.Classes.Entities;

namespace SchoolManagementSystem.Modules.Teachers.Entities
{
    public class Guru
    {
        public int id_teacher { get; set; }
        public string nama_teacher { get; set; }
        public string email_teacher { get; set; }
        public string bidang { get; set; }

        // Guru utama untuk satu kelas
        public ICollection<Kelas> KelasUtama { get; set; } = new List<Kelas>();

        // Guru mengajar banyak kelas (many-to-many)
        public ICollection<Kelas> KelasDiajarkan { get; set; } = new List<Kelas>();
    }
}
