using System.ComponentModel.DataAnnotations;
using DataAccess.Models;
using BCrypt.Net;

namespace Service.Transfermodels.Request;

public class UpdateUserDto
{
    
    [StringLength(50)]
    public string Username { get; set; } = null!;
    
    [StringLength(100)]
    public string Email { get; set; } = null!;
    
    [StringLength(255)]
    public string NewPassword { get; set; } = null!;

    public void UpdateUser(User user)
    {
        if (!string.IsNullOrEmpty(Username))
        {
            Username = user.Username;
        }
        if (!string.IsNullOrEmpty(Email))
        {
            Email = user.Email;
        }
        if (!string.IsNullOrEmpty(NewPassword))
        {
            user.Passwordhash = BCrypt.Net.BCrypt.HashPassword(NewPassword);
        }
    }

}