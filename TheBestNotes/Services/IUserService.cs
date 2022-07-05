using TheBestNotes.Models.DTO;

namespace TheBestNotes.Services;

public interface IUserService
{
    public bool CreateUser(string username, string password);
    public UserDTO GetUser(string username);
    public UserDTO GetUser(Guid userId);
}