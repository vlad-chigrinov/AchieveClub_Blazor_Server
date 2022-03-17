using AchiveClubServer.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AchiveClubServer.Services
{
    public class ChangeUserPasswordService
    {
        private HashService _hashService;
        private IUserRepository _userRepository;

        public ChangeUserPasswordService(HashService hashService, IUserRepository userRepository)
        {
            (_hashService, _userRepository) = (hashService, userRepository);
        }

        public bool ChangePassword(int userId, string newPassword)
        {
            User currentUser;
            try
            {
                currentUser = _userRepository.GetById(userId);
            }
            catch
            {
                return false;
            }

            string newPasswordHash = _hashService.HashPassword(newPassword).ToString();
            currentUser.Password = newPasswordHash;

            return _userRepository.Update(currentUser);
        }
    }
}
