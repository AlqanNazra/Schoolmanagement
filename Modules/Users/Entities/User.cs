using System.Security;
using SchoolManagementSystem.Modules.Students.Entities;
using SchoolManagementSystem.Modules.Teachers.Entities;

namespace SchoolManagementSystem.Modules.Users.Entities
{
    public class User
    {
        public int userId { get; set; }
        public string? UserName { get; set; }
        public string? PasswordHash { get; set; }
        public string? Role { get; set; } // e.g., "Admin", "Teacher", "Student"
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Murid? Murid { get; set; } 
        public Guru? Guru { get; set; }
    }
}