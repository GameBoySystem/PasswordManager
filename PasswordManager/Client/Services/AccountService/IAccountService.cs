using PasswordManager.Shared;

namespace PasswordManager.Client.Services.AccountService
{
    public interface IAccountService
    {
        List<Account> Accounts { get; set; }
        Task GetAccount();
        Task<Account> GetAccount(int id);
        Task PostAccount(Account account);
        Task PutAccount(int id, Account account);
        Task DeleteAccount(int id);
    }
}