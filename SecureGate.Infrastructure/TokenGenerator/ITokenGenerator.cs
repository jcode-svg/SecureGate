namespace OrderService.Infrastructure.TokenGenerator;

public interface ITokenGenerator
{
    string GenerateToken(string username, string profileId, string roleId);
}
