using MyEventApi.Core.Entities;

namespace MyEventApi.Core.Interfaces
{
    public interface ITokenService
    {
        (string token, DateTime expiresAt) GenerateToken(UserEntity user);
    }
}
