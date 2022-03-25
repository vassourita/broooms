namespace Broooms.Auth.Accounts;

using System.ComponentModel.DataAnnotations;
using Broooms.Auth.Shared;

public class RefreshToken : Entity
{
    public Guid UserId { get; set; }
    public UserAccount Account { get; set; }

    [Required]
    public DateTime ExpiresAt { get; set; }
}
