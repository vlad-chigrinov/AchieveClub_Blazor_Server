using AchieveClubServer.Data.DTO;
using AchieveClubServer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AchieveClubServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementsController : ControllerBase
    {
        private IAchieveRepository _achieveRepo;
        public AchievementsController(IAchieveRepository achieveRepository)
        {
            _achieveRepo = achieveRepository;
        }
        ////// GET: api/<AchievementsController>
        [HttpGet]
        public IEnumerable<Achievement> Get()
        {
            return _achieveRepo.GetAll();
        }

        // GET api/<AchievementsController>/5
        [HttpGet("{id}")]
        public Achievement Get(int id) 
        { 
            return _achieveRepo.GetById(id); 
        }

        ////POST api/<AchievementsController>
        [HttpPost]
        public ActionResult<Achievement> Post([FromBody] Achievement achievement)
        {
            int result = _achieveRepo.Insert(achievement);
            return Ok(result);
        }

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
