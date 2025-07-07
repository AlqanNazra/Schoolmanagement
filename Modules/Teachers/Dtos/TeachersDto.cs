using System.Collections.Generic;

namespace SchoolManagementSystem.Modules.Teachers.Dtos
{
    public class TeacherDto
    {
        public int id_teacher { get; set; }
        public string nama_teacher { get; set; }
        public string email_teacher { get; set; }
        public string bidang { get; set; }

        public List<int> kelasUtamaIds { get; set; }
        public List<int> kelasDiajarkanIds { get; set; }
    }
}
