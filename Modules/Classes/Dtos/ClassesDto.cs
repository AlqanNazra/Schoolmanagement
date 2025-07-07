using System.Collections.Generic;

namespace SchoolManagementSystem.Modules.Classes.Dtos
{
    public class KelasDto
    {
        public int id_kelas { get; set; }
        public string? nama_kelas { get; set; }
        public int? id_guru { get; set; }

        public required List<int> pengajarIds { get; set; }
        
    }
}
