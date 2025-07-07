using SchoolManagementSystem.Modules.Enrollments.Entities;
public class StudentDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public string? waktu_registrasi { get; set; }

    // Input: ID kelas saja
    public List<int>? KelasIds { get; set; }

    // Output: relasi enrollments (diabaikan saat input)
    public ICollection<Pendaftaran>? Enrollments { get; set; }
}


// Buat code untuk membuat Id_studen secara otomatis dan watu waktu registrasi secara otomatis