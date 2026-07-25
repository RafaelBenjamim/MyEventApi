using MyEventApi.Core.Interfaces;

namespace MyEventApi.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAcessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAcessor = httpContextAccessor;
        }
        public Guid GetStoreId()
        {
            var storeClaim = _httpContextAcessor.HttpContext?.User.FindFirst("storeId")?.Value;

            if (string.IsNullOrEmpty(storeClaim))
                throw new UnauthorizedAccessException("Token não contém storeId");


            return Guid.Parse(storeClaim);
           
        }
    }
}
