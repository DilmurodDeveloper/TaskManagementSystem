namespace TaskManagementSystem.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public int ProjectId { get; set; }
        public required string ProjectName { get; set; } 
    }
}
