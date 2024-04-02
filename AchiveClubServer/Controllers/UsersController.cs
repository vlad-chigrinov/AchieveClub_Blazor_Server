using AchieveClubServer.Data.DTO;
using AchieveClubServer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AchieveClubServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository _userRepo;
        private ChangeUserPasswordService _changePassword;
        public UsersController(IUserRepository userRepository)
        {
            _userRepo = userRepository;
        }
        // GET: api/<AchievementsController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userRepo.GetAll();
        }

        // GET api/<AchievementsController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _userRepo.GetById(id);
        }

        //POST api/<AchievementsController>
        [HttpPost]
        public ActionResult<int> Post([FromBody] RegisterParams registrationParams)
        {
            int result = _userRepo.Insert(registrationParams.ToUser());
            return Ok(result);

        }

        ////PUT api/<AchievementsController>/5
        [HttpPut("{id}")]
        public ActionResult<User> UpdateUser(int id, User user)
        {
            bool result = _userRepo.Update(user);
            try
            {
                if (id != user.Id)
                {
                    return BadRequest("User ID is not found!");
                }

                var userToUpdate = _userRepo.GetById(id);

                if (userToUpdate == null)
                {
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Ok(result);        }

        ////}

        //DELETE api/<AchievementsController>/5
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            bool result = _userRepo.Delete(id);
            return Ok(result);
        }
    }
}
