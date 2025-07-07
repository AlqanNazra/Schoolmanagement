using SchoolManagementSystem.Modules.Enrollments.Entities;
using System.Text.Json.Serialization;

public class StudentDto
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Email { get; set; }

    public string? waktu_registrasi { get; set; }

    // Hanya input kelas di sini
    public List<int>? KelasIds { get; set; }

    // Hanya untuk output
    [JsonIgnore]
    public ICollection<Pendaftaran>? Enrollments { get; set; }
}



// Buat code untuk membuat Id_studen secara otomatis dan watu waktu registrasi secara otomatis