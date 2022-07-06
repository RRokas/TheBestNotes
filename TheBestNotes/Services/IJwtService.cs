namespace TheBestNotes.Services;

public interface IJwtService
{
    public string GetJwtToken(Guid userId);
}