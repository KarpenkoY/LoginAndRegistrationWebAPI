using System;
using Microsoft.AspNetCore.Mvc;
using LoginAndRegistrationWebAPI.Models;
using LoginAndRegistrationWebAPI.Data;
using LoginAndRegistrationWebAPI.Access;

namespace LoginAndRegistrationWebAPI.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("api/registration")]
        public ActionResult PostRegistration([FromBody] User user)
        {
            bool isLoginNotExist = Database.IsLoginNotExist(user.Login).Result;

            if (!ModelState.IsValid || !IsValid(user))  return BadRequest();
            if (!isLoginNotExist)                       return BadRequest();

            string token = AccessControl.Registration(user);
            if (token == String.Empty)                  return BadRequest();
            
            return Ok(new 
            { 
                login = user.Login, 
                token = token 
            });
        }



        [HttpPost]
        [Route("api/login")]
        public ActionResult PostLogin([FromBody] User user)
        {
            if (!ModelState.IsValid || IsValid(user))   return BadRequest();

            string token = AccessControl.Login(user);
            if (token == String.Empty)                  return BadRequest();

            return Ok(new
            {
                login = user.Login,
                token = token
            });
        }



        private bool IsValid(User u)
        {
            if (u.Login == null || u.Password == null) return false;
            
            int loginLength     = u.Login.Length;
            int passwordLength  = u.Password.Length;

            if (4 < loginLength && loginLength < 33)
                if (4 < passwordLength && passwordLength < 33)
                    return true;

            return false;
        }
    }
}
