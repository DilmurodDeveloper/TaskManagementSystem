namespace TaskManagementSystem.Entities
{
    public class TaskEntity
    {
        public int Id { get; set; }                 
        public required string Title { get; set; }            
        public required string Description { get; set; }      
        public DateTime DueDate { get; set; }        
        public bool IsCompleted { get; set; } = false; 

        public int ProjectId { get; set; }           

        public required Project Project { get; set; }        
    }
}