namespace Broooms.Auth.Accounts.Dtos;

using System.ComponentModel.DataAnnotations;

public class CreateAccountDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(
        255,
        ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
        MinimumLength = 6
    )]
    [Compare(
        "PasswordConfirmation",
        ErrorMessage = "The password and confirmation password do not match."
    )]
    public string Password { get; set; }

    [Required]
    public string PasswordConfirmation { get; set; }

    [Required]
    [StringLength(
        255,
        ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
        MinimumLength = 2
    )]
    public string FirstName { get; set; }

    [Required]
    [StringLength(
        255,
        ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
        MinimumLength = 2
    )]
    public string LastName { get; set; }
}
