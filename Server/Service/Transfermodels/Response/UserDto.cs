using DataAccess.Models;

namespace Service.Transfermodels.Response;

public class UserDto
{
    public int Userid { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    
    public string PasswordHash { get; set; } = null!;
    public DateTime? Createdat { get; set; }

    public ICollection<int> ScoreboardIds { get; set; } = new List<int>();
    public ICollection<int> UserquestprogressIds { get; set; } = new List<int>();
    public ICollection<string> Roles { get; set; } = new List<string>();
    
    public static UserDto FromEntity(User user)
    {
        return new UserDto
        {
            Userid = user.Userid,
            Username = user.Username,
            Email = user.Email,
            PasswordHash = user.Passwordhash,
            Createdat = user.Createdat,
            ScoreboardIds = user.Scoreboards.Select(s => s.Scoreid).ToList(),
            UserquestprogressIds = user.Userquestprogresses.Select(uqp => uqp.Progressid).ToList(),
            Roles = user.Userroles.Select(ur => ur.Role.Rolename).ToList()
        };
    }
}