using exam_9_Electronic_wallet.Models;
namespace exam_9_Electronic_wallet.DAL.Repositories.Interfaces;

public interface IProviderRepository
{
    Task<List<ServiceProvider>> GetAllProviders();
    Task<ServiceProvider> GetByIdAsync(int id);
}