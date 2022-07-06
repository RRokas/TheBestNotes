using TheBestNotes.Models.DTO;

namespace TheBestNotes.Services.Interfaces;

public interface IUserService
{
    public bool AddUser(string username, string password);
    public string Login(string username, string password);
}