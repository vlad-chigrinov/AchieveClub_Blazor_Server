using AchieveClubServer.Data.DTO;
using Blazored.LocalStorage;
using System.Threading.Tasks;

namespace AchieveClubServer.Services
{
    public class LoginLocalStorage
    {
        private ILocalStorageService _localStorage;

        public LoginLocalStorage(ILocalStorageService localStorage)
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
            await _localStorage.ClearAsync();
        }
    }
}
