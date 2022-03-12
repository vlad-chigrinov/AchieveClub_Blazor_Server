using AchiveClubServer.Data.DTO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System;
using Blazored.LocalStorage;

namespace AchiveClubServer.Services
{
    public class UserLoginService
    {
        public User CurrentUser { get; private set; }
        public bool IsAuthorized { get; private set; } = false;

        private UserLoginDataProvider _loginProvider;
        private LoginLocalStorageService _loginLocalStorage;

        public UserLoginService(UserLoginDataProvider authService, LoginLocalStorageService loginLocalStorage)
        {
            _loginProvider = authService;
            _loginLocalStorage = loginLocalStorage;
        }

        public async Task<bool> Login(LoginParams loginParams)
        {
            try
            {
                var user = _loginProvider.LoginUser(loginParams);
                CurrentUser = user;
                IsAuthorized = true;

                await _loginLocalStorage.Store(loginParams);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<bool> TryLoginFromLocalStorage()
        {
            var loginParams = await _loginLocalStorage.Load();

            Console.WriteLine($"Load data from local storage {loginParams.Email} {loginParams.Password}");

            if (CheckLoginParams(loginParams) == false)
            {
                return false;
            }

            return await Login(loginParams);
        }

        private bool CheckLoginParams(LoginParams loginParams)
        {
            bool chechEmail = string.IsNullOrEmpty(loginParams.Email) == false;
            bool checkPassword = string.IsNullOrEmpty(loginParams.Password) == false;

            return checkPassword && chechEmail;
        }

        public async Task Logout()
        {
            CurrentUser = null;
            IsAuthorized = false;
            await _loginLocalStorage.Clear();
        }
    }
}
