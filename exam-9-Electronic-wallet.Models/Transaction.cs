namespace exam_9_Electronic_wallet.Models;

public class Transaction
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int WalletId { get; set; }
    public Wallet Wallet { get; set; } = null!;
    public string? CounterpartyAccountNumber { get; set; }

}