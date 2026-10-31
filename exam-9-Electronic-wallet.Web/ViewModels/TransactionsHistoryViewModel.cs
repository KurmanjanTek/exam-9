using exam_9_Electronic_wallet.Models;
namespace exam_9_Electronic_wallet.ViewModels;

public class TransactionsHistoryViewModel
{
    public List<Transaction> Transactions { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
}