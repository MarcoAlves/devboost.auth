using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using devboost.jwt.Models;
using devboost.jwt.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace devboost.jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok();
        }

        [HttpGet]
        [Route("curso")]
        [Authorize]
        public string GetCurso()
        {

            return "Algoritmo";

        } 

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> LoginAuthenticate([FromBody] User user)
        {

            var userReturn = UserRepository.Get(user.UserName, user.Password);

            if (userReturn == null)
                return NotFound();

            var token = TokenService.GenerateToken(userReturn);

            return Ok(token);

        }

    }
}
