using AchieveClubServer.Data.DTO;
using AchieveClubServer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AchieveClubServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private IAdminRepository _adminRepo;
        public AdminsController(IAdminRepository adminRepository)
        {
            _adminRepo = adminRepository;
        }
        ////// GET: api/<AchievementsController>
        [HttpGet]
        public IEnumerable<Admin> Get()
        {
            return _adminRepo.GetAll();
        }

        // GET api/<AchievementsController>/5
        [HttpGet("{id}")]
        public Admin Get(int id)
        {
            return _adminRepo.GetById(id);
        }

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
