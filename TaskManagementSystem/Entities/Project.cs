namespace TaskManagementSystem.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int UserId { get; set; }

        public required User User { get; set; }
        public required ICollection<TaskEntity> Tasks { get; set; }
    }
}
