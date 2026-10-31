using exam_9_Electronic_wallet.DAL.Data;
using exam_9_Electronic_wallet.DAL.Repositories.Interfaces;
using exam_9_Electronic_wallet.Models;
using Microsoft.EntityFrameworkCore;

namespace exam_9_Electronic_wallet.DAL.Repositories;

public class WalletRepository :IWalletRepository
{
    private readonly EWalletContext _context;

    public WalletRepository(EWalletContext context)
    {
        _context = context;
    }
    
    public async Task<Wallet?> GetByIdAsync(int id)
    {
        return await _context.Wallets
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<Wallet?> GetByUserIdAsync(int userId)
    {
        return await _context.Wallets
            .FirstOrDefaultAsync(w => w.UserId == userId);
    }

    public async Task<Wallet?> GetByAccountNumberAsync(string accountNumber)
    {
        return await _context.Wallets
            .FirstOrDefaultAsync(w => w.AccountNumber == accountNumber);
    }

    public async Task AddAsync(Wallet wallet)
    {
        await _context.Wallets.AddAsync(wallet);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Wallet wallet)
    {
        _context.Wallets.Update(wallet);
        await _context.SaveChangesAsync();
    }
    
    public async Task SaveTransferAsync( Wallet sender, Wallet recipient, Transaction senderTransaction,
        Transaction recipientTransaction) 
    { 
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.Wallets.Update(sender);
            _context.Wallets.Update(recipient);

            await _context.Transactions.AddAsync(senderTransaction);
            await _context.Transactions.AddAsync(recipientTransaction);

            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task AddTransactionAsync(Transaction transaction)
    {
        await _context.Transactions.AddAsync(transaction);
        await _context.SaveChangesAsync();
    }
    
    
    public async Task<List<Transaction>> GetTransactionsAsync( int walletId, DateTime? dateFrom, 
        DateTime? dateTo) 
    { 
        var query = _context.Transactions .Where(t => t.WalletId == walletId);
        if (dateFrom.HasValue)
            query = query.Where(t => t.CreatedAt >= dateFrom.Value.Date);

        if (dateTo.HasValue)
            query = query.Where(t => t.CreatedAt < dateTo.Value.Date.AddDays(1));

        return await query
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }
}