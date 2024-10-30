using System.ComponentModel.DataAnnotations;

public class UpdateTaskManagerRequest
{
    [StringLength(100)]
    public string? Name { get; set; }

    public bool IsComplete { get; set; }

}
