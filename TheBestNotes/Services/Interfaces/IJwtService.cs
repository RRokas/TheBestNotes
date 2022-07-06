namespace TheBestNotes.Services.Interfaces;

public interface IJwtService
{
    public string GetJwtToken(Guid userId);
}