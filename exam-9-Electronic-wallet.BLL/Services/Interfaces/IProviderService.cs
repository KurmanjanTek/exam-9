using exam_9_Electronic_wallet.Models;
namespace exam_9_Electronic_wallet.BLL.Services.Interfaces;

public interface IProviderService
{
    Task<List<ServiceProvider>> GetAllAsync();

    Task<ServiceProvider> GetByIdAsync(int id);
}