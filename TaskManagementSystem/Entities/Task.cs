namespace TaskManagementSystem.Entities
{
    public class Task
    {
        public int Id { get; set; }                 
        public string Title { get; set; }            
        public string Description { get; set; }      
        public DateTime DueDate { get; set; }        
        public bool IsCompleted { get; set; } = false; 

        public int ProjectId { get; set; }           

        public Project Project { get; set; }        
    }
}