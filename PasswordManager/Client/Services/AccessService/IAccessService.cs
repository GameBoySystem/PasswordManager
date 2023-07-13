using PasswordManager.Shared;

namespace PasswordManager.Client.Services.AccessService
{
    public interface IAccessService
    {
        List<ApplicationUser> Users { get; set; }
        Task GetUsers(int id);
        Task AddUser(int accountId, ApplicationUser applicationUser);
        Task DeleteUser(string userId, int accountId);
    }
}
