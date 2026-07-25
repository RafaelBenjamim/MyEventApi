using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using MyEventApi.Core.Dtos;

namespace MyEventApi.Core.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Register(RegisterRequestDto request);

        Task<LoginResponseDto> Login(LoginRequestDto request);

    }
}
