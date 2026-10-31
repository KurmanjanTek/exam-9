using exam_9_Electronic_wallet.BLL.Services.Interfaces;
using exam_9_Electronic_wallet.DAL.Repositories.Interfaces;
using exam_9_Electronic_wallet.Models;

namespace exam_9_Electronic_wallet.BLL.Services;

public class WalletService :IWalletService
{
    private readonly IWalletRepository _walletRepository;

    public WalletService(IWalletRepository walletRepository)
    {
        _walletRepository = walletRepository;
    }
    
    public async Task<Wallet?> GetByUserIdAsync(int userId)
    {
        return await _walletRepository.GetByUserIdAsync(userId);
    }

    public async Task<Wallet?> GetByAccountNumberAsync(string accountNumber)
    {
        return await _walletRepository.GetByAccountNumberAsync(accountNumber);
    }

    public async Task CreateWalletAsync(int userId, string accountNumber)
    {
        var wallet = new Wallet
        {
            UserId = userId,
            AccountNumber = accountNumber,
            Balance = 100000m
        };

        await _walletRepository.AddAsync(wallet);
    }
    
    public async Task<string> GenerateAccountNumberAsync()
    { 
        var random = new Random();
        string accountNumber;
        do
        {
            accountNumber = random.Next(100000, 1000000).ToString();
        }
        while (await _walletRepository.GetByAccountNumberAsync(accountNumber) != null);

        return accountNumber;
    }
    
    public async Task<bool> TopUpAsync(string accountNumber, decimal amount) 
    { 
        if (amount <= 0) 
            return false;
        var wallet = await _walletRepository.GetByAccountNumberAsync(accountNumber);

        if (wallet == null)
            return false;

        wallet.Balance += amount;
        await _walletRepository.UpdateAsync(wallet);
        
        await _walletRepository.AddTransactionAsync(new Transaction
        {
            Amount = amount,
            Type = "Пополнение",
            CreatedAt = DateTime.UtcNow,
            WalletId = wallet.Id,
            CounterpartyAccountNumber = null
        });

        return true;
    }
    
    public async Task<bool> TransferAsync( int senderUserId, string recipientAccountNumber, decimal amount)
    { 
        if (amount <= 0) 
            return false;
        var sender = await _walletRepository.GetByUserIdAsync(senderUserId);

        var recipient = await _walletRepository.GetByAccountNumberAsync(
            recipientAccountNumber);

        if (sender == null || recipient == null)
            return false;

        if (sender.Id == recipient.Id)
            return false;

        if (sender.Balance < amount)
            return false;

        sender.Balance -= amount;
        recipient.Balance += amount;

        var senderTransaction = new Transaction
        {
            Amount = amount,
            Type = "Перевод",
            CreatedAt = DateTime.UtcNow,
            WalletId = sender.Id,
            CounterpartyAccountNumber = recipient.AccountNumber
        };

        var recipientTransaction = new Transaction
        {
            Amount = amount,
            Type = "Пополнение переводом",
            CreatedAt = DateTime.UtcNow,
            WalletId = recipient.Id,
            CounterpartyAccountNumber = sender.AccountNumber
        };

        await _walletRepository.SaveTransferAsync(
            sender,
            recipient,
            senderTransaction,
            recipientTransaction);

        return true;
    }
    
    public async Task<List<Transaction>> GetTransactionsAsync( int userId, DateTime? dateFrom, DateTime? dateTo)
    { 
        var wallet = await _walletRepository.GetByUserIdAsync(userId);
        if (wallet == null)
            return new List<Transaction>();

        return await _walletRepository.GetTransactionsAsync(
            wallet.Id,
            dateFrom,
            dateTo);
    }
}