using MyEventApi.Core.Dtos;
using MyEventApi.Core.Interfaces;

namespace MyEventApi.Application.Services
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepository;
        private readonly ICurrentUserService _currentUserService;

        public StoreService(IStoreRepository storeRepository, ICurrentUserService currentUserService)
        {
            _storeRepository = storeRepository;
            _currentUserService = currentUserService;
        }
        public async Task UpdatePaymentSettings(UpdatePaymentSettingsRequest request)
        {
            var storeId = _currentUserService.GetStoreId(); 

            var store = await _storeRepository.GetByIdStore(storeId);

            if (store is null)
                throw new InvalidOperationException("Loja não encontrada!");

            store.PaymentProvider = request.PaymentProvider;
            store.InfinitePayHandle = request.InfinitePayHandle;
            store.PagBankAppKey = request.PagBankAppKey;
            store.PagBankAppId = request.PagBankAppId;

            await _storeRepository.UpdateStore(store);
        }
    }
}
