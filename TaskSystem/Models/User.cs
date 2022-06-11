using System.Collections.Generic;

namespace TaskSystem.Models
{
    public partial class User
    {
        public User()
        {
            TaskCustomers = new HashSet<Task>();
            TaskPerformers = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string MiddleName { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;

        public string FullName => $"{LastName} {FirstName} {MiddleName}";

        public virtual ICollection<Task> TaskCustomers { get; set; }
        public virtual ICollection<Task> TaskPerformers { get; set; }
    }
}