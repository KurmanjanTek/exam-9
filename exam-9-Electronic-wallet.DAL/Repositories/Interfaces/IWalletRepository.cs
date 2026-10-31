using exam_9_Electronic_wallet.Models;

namespace exam_9_Electronic_wallet.DAL.Repositories.Interfaces;

public interface IWalletRepository
{
    Task<Wallet?> GetByIdAsync(int id);

    Task<Wallet?> GetByUserIdAsync(int userId);

    Task<Wallet?> GetByAccountNumberAsync(string accountNumber);

    Task AddAsync(Wallet wallet);

    Task UpdateAsync(Wallet wallet);
    
    Task SaveTransferAsync(Wallet sender, Wallet recipient, Transaction senderTransaction,
        Transaction recipientTransaction);
    
    Task AddTransactionAsync(Transaction transaction);

    Task<List<Transaction>> GetTransactionsAsync(int walletId, DateTime? dateFrom,
        DateTime? dateTo);
}