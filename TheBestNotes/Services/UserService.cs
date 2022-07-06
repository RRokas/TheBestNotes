using System.Security.Cryptography;
using TheBestNotes.Models;
using TheBestNotes.Models.DTO;
using TheBestNotes.Services.Interfaces;

namespace TheBestNotes.Services;

public class UserService : IUserService
{
    private readonly SqliteDb _db;
    private readonly IJwtService _jwtService;

    public UserService(SqliteDb sqliteDb, IJwtService jwtService)
    {
        _db = sqliteDb;
        _jwtService = jwtService;
    }
    
    public bool AddUser(string username, string password)
    {
        var usernameAlreadyExists = _db.Users.Where(u => u.Username == username).Any();
        if (!usernameAlreadyExists)
        {
            var newUser = CreateUser(username, password);
            _db.Users.Add(newUser);
            _db.SaveChanges();
            return true;
        }

        return false;

    }

    private User GetCompleteUser(string username)
    {
        return _db.Users.Single(u => u.Username == username);
    }
    
    private User GetCompleteUser(Guid userId)
    {
        return _db.Users.Single(u => u.Id == userId);
    }

    private User CreateUser(string username, string password)
    {
        CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);
        
        var user = new User()
        {
            Username = username,
            Password = passwordHash,
            Salt = passwordSalt,
            Role = "Basic"
        };

        return user;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    public string Login(string username, string password)
    {
        var token = "";
        
        var user = GetCompleteUser(username);
        var passwordIsValid = VerifyPassword(password, user.Password, user.Salt);
        if (passwordIsValid)
        {
            token = _jwtService.GetJwtToken(user.Id);
        }

        return token;
    }

    private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        return computedHash.SequenceEqual(passwordHash);
    }
}