using TheBestNotes.Models.DTO;

namespace TheBestNotes.Services;

public interface IUserService
{
    public bool AddUser(string username, string password);
    public UserDTO GetUser(string username);
    public UserDTO GetUser(Guid userId);
    public string Login(string username, string password);
}