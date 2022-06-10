using System.Collections.Generic;

namespace TaskSystem.Models
{
    public partial class Status
    {
        public Status()
        {
            Tasks = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<Task> Tasks { get; set; }
    }
}