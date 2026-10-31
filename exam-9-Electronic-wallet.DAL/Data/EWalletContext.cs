using exam_9_Electronic_wallet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace exam_9_Electronic_wallet.DAL.Data;

public class EWalletContext: IdentityDbContext<User, IdentityRole<int>, int>
{
    public DbSet<User> Users { get; set; }
    public  DbSet<Wallet> Wallets { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    
    public EWalletContext(DbContextOptions options) : base(options) { }
    
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Wallet>().HasIndex(w => w.AccountNumber).IsUnique();
        builder.Entity<Wallet>().HasIndex(w => w.UserId).IsUnique();
    }
}