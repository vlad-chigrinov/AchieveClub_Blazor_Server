using AchiveClubServer.Data.DTO;
using Blazored.LocalStorage;
using System.Threading.Tasks;

namespace AchiveClubServer.Services
{
    public class LoginLocalStorageService
    {
        private ILocalStorageService _localStorage;

        public LoginLocalStorageService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task Store(LoginParams loginParams)
        {
            await _localStorage.SetItemAsync("password", loginParams.Password);
            await _localStorage.SetItemAsync("email", loginParams.Email);
        }

        public async Task<LoginParams> Load()
        {
            var loginParams = new LoginParams();
            loginParams.Password = await _localStorage.GetItemAsync<string>("password");
            loginParams.Email = await _localStorage.GetItemAsync<string>("email");
            return loginParams;
        }

        public async Task Clear()
        {
            await _localStorage.SetItemAsync("password", "");
            await _localStorage.SetItemAsync("email", "");
        }
    }
}
