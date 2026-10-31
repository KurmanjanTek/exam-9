namespace exam_9_Electronic_wallet.Models;

public class ProviderAccount
{
    public int Id { get; set; }
    public string AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int ServiceProviderId { get; set; }
    public ServiceProvider ServiceProvider { get; set; }
}