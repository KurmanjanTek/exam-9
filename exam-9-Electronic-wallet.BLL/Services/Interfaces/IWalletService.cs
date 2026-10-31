using exam_9_Electronic_wallet.Models;

namespace exam_9_Electronic_wallet.BLL.Services.Interfaces;

public interface IWalletService
{
    Task<Wallet?> GetByUserIdAsync(int userId);

    Task<Wallet?> GetByAccountNumberAsync(string accountNumber);

    Task CreateWalletAsync(int userId, string accountNumber);

    Task<string> GenerateAccountNumberAsync();
    
    Task<bool> TopUpAsync(string accountNumber, decimal amount);
    
    Task<bool> TransferAsync(int senderUserId, string recipientAccountNumber, decimal amount);
    
    Task<List<Transaction>> GetTransactionsAsync(int userId, DateTime? dateFrom,
        DateTime? dateTo);

}