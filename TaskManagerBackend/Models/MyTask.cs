using System.ComponentModel.DataAnnotations;

namespace TaskManagerBackend.Models
{
    public class MyTask
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }

        public MyTask()
        {
            IsComplete = false;
        }

    }
}
