namespace Broooms.Auth.Shared;

using System.ComponentModel.DataAnnotations;

public class Entity
{
    [Key]
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
