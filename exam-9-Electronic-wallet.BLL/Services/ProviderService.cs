using exam_9_Electronic_wallet.BLL.Services.Interfaces;
using exam_9_Electronic_wallet.DAL.Repositories.Interfaces;
using exam_9_Electronic_wallet.Models;

namespace exam_9_Electronic_wallet.BLL.Services;

public class ProviderService : IProviderService
{ 
    private readonly IProviderRepository _providerRepository;
    
    public ProviderService(IProviderRepository providerRepository)
    {
        _providerRepository = providerRepository;
    }

    public async Task<List<ServiceProvider>> GetAllAsync()
    {
        return await _providerRepository.GetAllProviders();
    }

    public async Task<ServiceProvider> GetByIdAsync(int id)
    {
        return await _providerRepository.GetByIdAsync(id);
    }
}