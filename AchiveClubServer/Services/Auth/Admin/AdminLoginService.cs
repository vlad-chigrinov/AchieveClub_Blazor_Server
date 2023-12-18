using AchieveClubServer.Data.DTO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AchieveClubServer.Services
{
    public class AdminLoginService
    {
        public Admin CurrentAdmin { get; private set; }
        public bool IsAuthorized { get; private set; } = false;

        private AdminLoginDataProvider _authService;

        public AdminLoginService(AdminLoginDataProvider authService)
        {
            _authService = authService;
        }

        public bool Login(LoginParams loginParams)
        {
            Admin admin = new Admin();
            try
            {
                admin = _authService.LoginAdmin(loginParams);
            }
            catch
            {
                return false;
            }

            if (admin != null)
            {
                CurrentAdmin = admin;
                IsAuthorized = true;

                return true;
            }
            else
            {
                Logout();

                return false;
            }
        }

        public void Logout()
        {
            CurrentAdmin = null;
            IsAuthorized = false;
        }
    }
}
