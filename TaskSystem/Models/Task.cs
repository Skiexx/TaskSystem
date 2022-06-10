namespace TaskSystem.Models
{
    public partial class Task
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int CustomerId { get; set; }
        public int? PerformerId { get; set; }
        public int StatusId { get; set; }

        public virtual User Customer { get; set; } = null!;
        public virtual User? Performer { get; set; }
        public virtual Status Status { get; set; } = null!;
    }
}