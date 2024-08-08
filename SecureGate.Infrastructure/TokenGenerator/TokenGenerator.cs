using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrderService.Infrastructure.TokenGenerator;
using SecureGate.SharedKernel.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static SecureGate.SharedKernel.HelperMethods.Utility;

namespace SecureGate.Infrastructure.TokenGenerator;

public class TokenGenerator : ITokenGenerator
{
    private readonly IConfiguration _configuration;

    public TokenGenerator(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(string username, string profileId, string roleId)
    {
        var issuer = _configuration.GetSection("JwtSettings:Issuer").Value;
        var secretKey = _configuration.GetSection("JwtSettings:SecretKey").Value;
        var expiryMinutes = _configuration.GetSection("JwtSettings:ExpiryMinutes").Value;
        var audience = _configuration.GetSection("JwtSettings:Audience").Value;

        var claims = new[]
        {
            new Claim(SecureGateClaims.ProfileId, profileId),
            new Claim(SecureGateClaims.Role, roleId)
        };

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            SecurityAlgorithms.HmacSha512);

        var securityToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: GetCurrentTime().AddMinutes(Convert.ToInt32(expiryMinutes)),
                signingCredentials: signingCredentials
                );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
