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
            var result = await _http.GetFromJsonAsync<List<Account>>("api/Accounts");
            if (result != null)
                Accounts = result;
        }

        public async Task<Account> GetAccount(int id)
        {
            var result = await _http.GetFromJsonAsync<Account>($"api/Accounts/{id}");
            if (result != null)
                return result;
            throw new Exception("Account not found!");
        }

        public async Task PostAccount(Account account)
        {
            await _http.PostAsJsonAsync("api/Accounts", account);
            _navigationManager.NavigateTo("passwords");
        }

        public async Task PutAccount(int id, Account account)
        {
            await _http.PutAsJsonAsync($"api/Accounts/{account.Id}", account);
            _navigationManager.NavigateTo("passwords");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAccount(int id)
        {
            //поставить проверку на null
            await _http.DeleteAsync($"api/Accounts/{id}");
            _navigationManager.NavigateTo("passwords");
        }
    }
}
