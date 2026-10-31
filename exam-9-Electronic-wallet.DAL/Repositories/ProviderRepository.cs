using exam_9_Electronic_wallet.DAL.Data;
using exam_9_Electronic_wallet.Models;
using exam_9_Electronic_wallet.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace exam_9_Electronic_wallet.DAL.Repositories;

public class ProviderRepository :IProviderRepository
{
    private readonly EWalletContext _context;

    public ProviderRepository(EWalletContext context)
    {
        _context = context;
    }

    public async Task<List<ServiceProvider>> GetAllProviders()
    {
        return await _context.ServiceProviders.ToListAsync();
    }

    public async Task<ServiceProvider> GetByIdAsync(int id)
    {
        return await _context.ServiceProviders.FirstOrDefaultAsync(p => p.Id == id);
    }
}