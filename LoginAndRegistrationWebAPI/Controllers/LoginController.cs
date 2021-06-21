using System;
using Microsoft.AspNetCore.Mvc;
using LoginAndRegistrationWebAPI.Models;
using LoginAndRegistrationWebAPI.Access;

namespace LoginAndRegistrationWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            string token = AccessControl.Login(user);

            if (token == String.Empty) return BadRequest();

            return Ok(new
            {
                login = user.Login,
                token = token
            });
        }
    }
}
