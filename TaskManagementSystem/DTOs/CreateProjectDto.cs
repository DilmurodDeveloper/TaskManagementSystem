using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.DTOs
{
    public class CreateProjectDto
    {
        [Required(ErrorMessage = "Loyiha nomi majburiy")]
        [StringLength(100, ErrorMessage = "Loyiha nomi 100 belgidan oshmasligi kerak")]
        public required string Name { get; set; }

        [StringLength(500, ErrorMessage = "Tavsif 500 belgidan oshmasligi kerak")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Boshlanish sanasi majburiy")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Tugash sanasi majburiy")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Foydalanuvchi ID'si majburiy")]
        public int UserId { get; set; }
    }
}
