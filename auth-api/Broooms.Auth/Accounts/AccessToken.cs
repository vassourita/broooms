namespace Broooms.Auth.Accounts;

using System.ComponentModel.DataAnnotations;
using Broooms.Auth.Shared;

public class AccessToken : Entity
{
    public Guid UserId { get; set; }
    public UserAccount Account { get; set; }

    [Required]
    public string TokenString { get; set; }
}
