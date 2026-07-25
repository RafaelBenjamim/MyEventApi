using MyEventApi.Core.Entities;

namespace MyEventApi.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetByEmail (string email);
        Task AddUser(UserEntity user);
    }
}
