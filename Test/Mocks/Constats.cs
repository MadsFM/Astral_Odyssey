using Bogus;
using DataAccess.Models;

namespace Test;

public class Constats
{
    private static HashSet<string> _usernames;
    private static HashSet<string> _emails = new HashSet<string>();
    public static User GetUser()
    {
        return new Faker<User>()
            .RuleFor(u => u.Userid, f => f.IndexFaker + 1)
            .RuleFor(u => u.Username, f =>
            {
                string username;
                do
                {
                    username = f.Person.UserName;
                } while (_usernames.Contains(username));
                
                _usernames.Add(username);
                return username;
            })
            .RuleFor(u => u.Email, f =>
            {
                string email;
                do
                {
                    email = f.Person.Email;
                } while (_emails.Contains(email));

                _emails.Add(email);
                return email;
            })
            .RuleFor(u => u.Createdat, f => f.DateTimeReference)
            .RuleFor(u => u.Userroles, f => new List<Userrole>
            {
                new Userrole
                {
                    Roleid = f.Random.Int(1, 10),
                    Createdat = f.Date.Past()
                }
            });
    }
}