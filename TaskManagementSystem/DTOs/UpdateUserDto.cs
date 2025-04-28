using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.DTOs
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "ID majburiy")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Foydalanuvchi nomi majburiy")]
        [StringLength(50, ErrorMessage = "Foydalanuvchi nomi 50 belgidan oshmasligi kerak")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email majburiy")]
        [EmailAddress(ErrorMessage = "Email manzili to‘g‘ri formatda emas")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Parol majburiy")]
        [MinLength(6, ErrorMessage = "Parol kamida 6 ta belgidan iborat bo‘lishi kerak")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Roli majburiy")]
        public string Role { get; set; } = "User";
    }
}
