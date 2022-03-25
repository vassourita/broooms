namespace Broooms.Auth.Accounts;

using System.ComponentModel.DataAnnotations;
using Broooms.Auth.Shared;

public class UserAccount : Entity
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [MaxLength(512)]
    public string PasswordHash { get; set; }

    [Required]
    [MaxLength(255)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(255)]
    public string LastName { get; set; }

    public IEnumerable<AccessToken> AccessTokens { get; set; } = new List<AccessToken>();
    public IEnumerable<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    public IEnumerable<Claim> Claims { get; set; } = new List<Claim>();
}
