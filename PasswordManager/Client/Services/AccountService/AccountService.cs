using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using PasswordManager.Client.Services.AccountService;
using System.Net.Http.Json;

namespace PasswordManager.Client.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public List<Account> Accounts { get; set; } = new List<Account>();

        public AccountService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public async Task GetAccount()
        {
            var result = await _http.GetFromJsonAsync<List<Account>>("api/Passwords");
            //if (result != null)
            //    Accounts = result;
        }

        public async Task<Account> GetAccount(int id)
        {
            var result = await _http.GetFromJsonAsync<Account>($"api/Passwords/{id}");
            if (result != null)
                return result;
            throw new Exception("Password not found!");
        }

        public async Task PostAccount(Account account)
        {
            await _http.PostAsJsonAsync("api/Passwords", account);
            _navigationManager.NavigateTo("mypasswords");
        }

        public async Task PutAccount(int id, Account account)
        {
            await _http.PutAsJsonAsync($"api/Passwords/{account.Id}", account);
            _navigationManager.NavigateTo("mypasswords");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAccount(int id)
        {
            //поставить проверку на null
            await _http.DeleteAsync($"api/Passwords/{id}");
            _navigationManager.NavigateTo("mypasswords");
        }
    }
}
