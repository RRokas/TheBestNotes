using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using TheBestNotes.Services.Interfaces;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace TheBestNotes.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GetJwtToken(Guid userId)
    {
        var tokenLifeInMinutes = int.Parse(_configuration["Jwt:TokenLifetimeMinutes"]);
        var tokenLifetime = DateTime.Now.AddMinutes(tokenLifeInMinutes);
        List<Claim> claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString())
        };

        var secret = _configuration["Jwt:Key"];
        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(secret));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: tokenLifetime,
            signingCredentials: cred);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}