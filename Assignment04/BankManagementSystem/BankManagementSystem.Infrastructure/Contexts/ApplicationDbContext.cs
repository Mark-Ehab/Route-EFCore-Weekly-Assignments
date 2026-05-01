using BankManagementSystem.Application.Configurations;
using BankManagementSystem.Application.Interceptors;
using BankManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace BankManagementSystem.Application.Contexts;

public sealed class ApplicationDbContext : DbContext  
{
    /* Fields */
    private readonly string _databaseConnectionString = "Server=.; Database=BankSystemDb; Trusted_Connection=True; TrustServerCertificate=True;";

    /* DbSets */
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Branch> Branches { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<AccountCustomer> AccountsCustomers { get; set; }

    /* Methods */
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(_databaseConnectionString)
            .AddInterceptors([new SoftDeleteInterceptor(), new AuditInterceptor()]);
            //.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
            //.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /* Apply Fluent API configurations for all models */
        modelBuilder
            .ApplyConfiguration(new BaseEntityConfiguration());
        modelBuilder
            .ApplyConfiguration(new AccountConfiguration());
        modelBuilder
            .ApplyConfiguration(new BranchConfiguration());
        modelBuilder
            .ApplyConfiguration(new CustomerConfiguration());
        modelBuilder
            .ApplyConfiguration(new ManagerConfiguration());
        modelBuilder
            .ApplyConfiguration(new TransactionConfiguration());
        modelBuilder
            .ApplyConfiguration(new AccountCustomerConfiguration());
    }
}
