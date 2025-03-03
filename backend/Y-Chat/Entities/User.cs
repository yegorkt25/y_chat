using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Y_Chat.Entities;

public class User
{
    [Key]
    public long Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Email { get; set; }
    
    [MaxLength(100)]
    public string? Username { get; set; }
    
    [Required]
    public string PasswordHash { get; set; }
    
    public DateTimeOffset CreatedAt { get; } = DateTimeOffset.UtcNow;

    public bool EmailConfirmed { get; set; } = false;
    
    [MaxLength(256)]
    public string AvatarUrl { get; set; } = string.Empty;
    public bool ProfileDetailsAdded { get; set; } = false;
}