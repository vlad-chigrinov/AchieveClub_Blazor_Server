using AchieveClubServer.Data.DTO;
using AchieveClubServer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AchieveClubServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository _userRepo;
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
        ////[HttpGet("{id}")]
        ////public string Get(int id)
        ////{
        ////    return "value";
        ////}

        ////POST api/<AchievementsController>
        ////[HttpPost]
        ////public void Post([FromBody] string value)
        ////{
        ////}

        ////PUT api/<AchievementsController>/5
        ////[HttpPut("{id}")]
        ////public void Put(int id, [FromBody] string value)
        ////{
        ////}

        ////DELETE api/<AchievementsController>/5
        ////[HttpDelete("{id}")]
        ////public void Delete(int id)
        ////{
        ////}
    }
}
