namespace Broooms.Auth.Accounts;

using System.ComponentModel.DataAnnotations;
using Broooms.Auth.Shared;

public class Claim : Entity
{
    public Claim(string name, string description)
    {
        Name = name;
        Description = description;
    }

    [Required]
    [MaxLength(64)]
    public string Name { get; set; }

    [Required]
    [MaxLength(512)]
    public string Description { get; set; }

    public IEnumerable<UserAccount> Accounts { get; set; } = new List<UserAccount>();
}
