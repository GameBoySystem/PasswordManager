using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Components;
using PasswordManager.Shared;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace PasswordManager.Client.Services.AccessService
{
    public class AccessService : IAccessService
    {
        private readonly HttpClient _http;
        private readonly NavigationManager _navigationManager;

        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

        public AccessService(HttpClient http, NavigationManager navigationManager)
        {
            _http = http;
            _navigationManager = navigationManager;
        }

        public async Task GetUsers(int id)
        {
            var result = await _http.GetFromJsonAsync<List<ApplicationUser>>($"/api/Access/{id}");
            if (result != null)
                Users = result;
        }

        public async Task AddUser(int accountId, ApplicationUser applicationUser)
        {
            await _http.PutAsJsonAsync($"api/Access/{accountId}", applicationUser);
            _navigationManager.NavigateTo($"api/Access/{accountId}");
        }

        public async Task DeleteUser(string UserId, int accountId)
        {
            await _http.DeleteAsync($"api/Access/{UserId}&{accountId}");
        }
    }
}
