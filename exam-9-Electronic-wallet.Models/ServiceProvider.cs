namespace exam_9_Electronic_wallet.Models;

public class ServiceProvider
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public ICollection<ProviderAccount>ProviderAccounts { get; set; }
}