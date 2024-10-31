using System.ComponentModel.DataAnnotations;
using DataAccess.Models;

namespace Service.Transfermodels.Request;

public class CreateUserDto
{
    [Required]
    [StringLength(50)]
    public string Username { get; set; } = null!;

    [Required]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(255)]
    public string Passwordhash { get; set; } = null!;
    
    public User ToUser()
    {
        return new User
        {
            Username = this.Username,
            Email = this.Email,
            Passwordhash = this.Passwordhash,
            Createdat = DateTime.UtcNow // Sets created date to current UTC time
        };
    }
}