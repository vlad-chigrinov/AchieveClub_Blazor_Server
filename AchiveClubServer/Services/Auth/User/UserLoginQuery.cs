using System.Linq;
using System;
using AchiveClubServer.Data.DTO;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace AchiveClubServer.Services
{
    public class UserLoginQuery
    {
        private IUserRepository _userRepository;
        private HashService _hashService;
        public UserLoginQuery(IUserRepository userRepository, HashService hashService)
        {
            _userRepository = userRepository;
            _hashService = hashService;
        }

        public User LoginUser(LoginParams loginParams)
        {
            var users = _userRepository.GetAll();

            foreach (User user in users)
            {
                if (loginParams.Email == user.Email)
                {
                    if (_hashService.ValidPassword(loginParams.Password, user.Password))
                    {
                        return user;
                    }
                }
            }

            throw new Exception("Login Failed!");
        }
    }
}
