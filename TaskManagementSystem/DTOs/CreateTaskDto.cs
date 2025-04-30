using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.DTOs
{
    public class CreateTaskDto
    {
        [Required(ErrorMessage = "Vazifa nomi majburiy")]
        [StringLength(100, ErrorMessage = "Vazifa nomi 100 belgidan oshmasligi kerak")]
        public required string Title { get; set; }

        [StringLength(500, ErrorMessage = "Tavsif 500 belgidan oshmasligi kerak")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "Muddat majburiy")]
        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        [Required(ErrorMessage = "Loyiha ID'si majburiy")]
        public int ProjectId { get; set; }
    }
}
