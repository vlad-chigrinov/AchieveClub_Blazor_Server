using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AchiveClubServer.Data.DTO;

namespace AchiveClubServer.Services
{
    public class RegistrationService
    {
        private UserLoginService _userLoginService;
        private IUserRepository _userRepository;
        private HashService _hashService;
        private UniqEmailQuery _uniqEmailProvider;
        public RegistrationService(UserLoginService userLoginService, IUserRepository userRepository, HashService hashService, UniqEmailQuery uniqEmailProvider)
        {
            _userLoginService = userLoginService;
            _userRepository = userRepository;
            _hashService = hashService;
            _uniqEmailProvider = uniqEmailProvider;
        }
        public async Task<bool> Registrate(RegisterParams registerParams)
        {
            if(_uniqEmailProvider.IsUniq(registerParams.Email) == false)
            {
                return false;
            }

            try
            {
                _userRepository.Insert(new User
                {
                    FirstName = registerParams.FirstName,
                    LastName = registerParams.LastName,
                    Email = registerParams.Email,
                    Password = _hashService.HashPassword(registerParams.Password).ToString(),
                    Avatar = registerParams.Avatar,
                    ClubRefId = registerParams.ClubId
                });
            }
            catch
            {
                return false;
            }

            return await _userLoginService.Login(new LoginParams
            {
                Email = registerParams.Email,
                Password = registerParams.Password
            });
        }
    }
}
