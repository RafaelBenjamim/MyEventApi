using MyEventApi.Core.Dtos;
using MyEventApi.Core.Entities;
using MyEventApi.Core.Interfaces;
using System.Linq.Expressions;

namespace MyEventApi.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IStoreRepository _storeRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, IStoreRepository storeRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _storeRepository = storeRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto> Register(RegisterRequestDto request)
        {
            var slugExists = await _storeRepository.SlugExists(request.StoreSlug);

            if (slugExists)
                throw new InvalidOperationException("Ja existe uma loja com esse nome");

            var store = new StoreEntity
            {
                Id = Guid.NewGuid(),
                Name = request.StoreName,
                Slug = request.StoreSlug,
                Email = request.Email,
            };

            await _storeRepository.AddStore(store);

            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                StoreId = store.Id,
                Name = request.UserName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            };
            await _userRepository.AddUser(user);

            var (token, expiresAt) = _tokenService.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                ExpiresAt = expiresAt
            };
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmail(request.Email);

            if(user is null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Email ou senha inválidos");

            var (token, expiresAt) = _tokenService.GenerateToken(user);

            return new LoginResponseDto 
            { 
                Token = token, 
                ExpiresAt = expiresAt 
            };

        }
    }
}
