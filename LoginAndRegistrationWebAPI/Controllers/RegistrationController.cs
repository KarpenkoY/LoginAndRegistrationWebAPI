using System;
using Microsoft.AspNetCore.Mvc;
using LoginAndRegistrationWebAPI.Models;
using LoginAndRegistrationWebAPI.Data;
using LoginAndRegistrationWebAPI.Access;

namespace LoginAndRegistrationWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
            bool isLoginUnique = Database.IsLoginNotExist(user.Login).Result;

            if (!ModelState.IsValid || !isLoginUnique) return BadRequest();

            string token = AccessControl.Registration(user);
            if (token == String.Empty) return BadRequest();
            
            return Ok(new 
            { 
                login = user.Login, 
                token = token 
            });
        }
    }
}
