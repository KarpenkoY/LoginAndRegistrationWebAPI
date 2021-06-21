using LoginAndRegistrationWebAPI.Controllers;
using LoginAndRegistrationWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using System;
using System.Linq;

namespace LoginAndRegistrationWebAPI.Tests
{
    public class UserControllerTests
    {
        private const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghjklmnopqrstuvwxyz";
        private static Random Random = new Random();

        private const int MIN = 5;
        private const int MAX = 32;

        private static Type BadRequestType  = typeof(BadRequestResult);
        private static Type OkType          = typeof(OkObjectResult);

        private string GetRandomString(int length)
        {
            return new string(Enumerable.Repeat(CHARS, length)
              .Select(s => s[Random.Next(s.Length)])
              .ToArray());
        }



        [Fact]
        public void Register_AdminLogin_Returns400()
        {
            UserController controller = new UserController();

            ActionResult actual = controller.PostRegistration
                (new User { Login = "Admin", Password = "Admin" });

            Assert.IsType(BadRequestType, actual);
        }

        [Fact]
        public void RegisterAndLogin_TooShortLogin_Returns400()
        {
            UserController controller = new UserController();
            User user = new User { Login = GetRandomString(MIN - 1), Password = GetRandomString(12) };

            ActionResult actualRegistration = controller.PostRegistration(user);
            ActionResult actualLogin = controller.PostLogin(user);

            Assert.IsType(BadRequestType, actualRegistration);
            Assert.IsType(BadRequestType, actualLogin);
        }

        [Fact]
        public void RegisterAndLogin_TooShortPassword_Returns400()
        {
            UserController controller = new UserController();
            User user = new User { Login = GetRandomString(8), Password = GetRandomString(MIN - 1) };

            ActionResult actualRegistration = controller.PostRegistration(user);
            ActionResult actualLogin = controller.PostLogin(user);

            Assert.IsType(BadRequestType, actualRegistration);
            Assert.IsType(BadRequestType, actualLogin);
        }

        [Fact]
        public void RegisterAndLogin_TooLongLogin_Returns400()
        {
            UserController controller = new UserController();
            User user = new User { Login = GetRandomString(MAX + 1), Password = GetRandomString(12) };

            ActionResult actualRegistration = controller.PostRegistration(user);
            ActionResult actualLogin = controller.PostLogin(user);

            Assert.IsType(BadRequestType, actualRegistration);
            Assert.IsType(BadRequestType, actualLogin);
        }

        [Fact]
        public void RegisterAndLogin_TooLongPassword_Returns400()
        {
            UserController controller = new UserController();
            User user = new User { Login = GetRandomString(8), Password = GetRandomString(MAX + 1) };

            ActionResult actualRegistration = controller.PostRegistration(user);
            ActionResult actualLogin = controller.PostLogin(user);

            Assert.IsType(BadRequestType, actualRegistration);
            Assert.IsType(BadRequestType, actualLogin);
        }

        [Fact]
        public void RegisterAndLogin_LoginNotProvided_Returns400()
        {
            UserController controller = new UserController();
            User user = new User { Password = GetRandomString(12) };

            ActionResult actualRegistration = controller.PostRegistration(user);
            ActionResult actualLogin = controller.PostLogin(user);

            Assert.IsType(BadRequestType, actualRegistration);
            Assert.IsType(BadRequestType, actualLogin);
        }

        [Fact]
        public void RegisterAndLogin_PasswordNotProvided_Returns400()
        {
            UserController controller = new UserController();
            User user = new User { Login = GetRandomString(8) };

            ActionResult actualRegistration = controller.PostRegistration(user);
            ActionResult actualLogin = controller.PostLogin(user);

            Assert.IsType(BadRequestType, actualRegistration);
            Assert.IsType(BadRequestType, actualLogin);
        }

        [Fact]
        public void RegisterAndLogin_LoginEmpty_Returns400()
        {
            UserController controller = new UserController();
            User user = new User { Login = String.Empty, Password = GetRandomString(12) };

            ActionResult actualRegistration = controller.PostRegistration(user);
            ActionResult actualLogin = controller.PostLogin(user);

            Assert.IsType(BadRequestType, actualRegistration);
            Assert.IsType(BadRequestType, actualLogin);
        }

        [Fact]
        public void RegisterAndLogin_PasswordEmpty_Returns400()
        {
            UserController controller = new UserController();
            User user = new User { Login = GetRandomString(8), Password = String.Empty };

            ActionResult actualRegistration = controller.PostRegistration(user);
            ActionResult actualLogin = controller.PostLogin(user);

            Assert.IsType(BadRequestType, actualRegistration);
            Assert.IsType(BadRequestType, actualLogin);
        }



        [Fact]
        public void Register_UniqueUser_Returns200()
        {
            UserController controller = new UserController();
            User user = new User { Login = GetRandomString(8), Password = GetRandomString(12) };

            ActionResult actualRegistration = controller.PostRegistration(user);

            Assert.IsType(OkType, actualRegistration);
        }

        [Fact]
        public void Register_MinAllowLoginLength_Returns200()
        {
            UserController controller = new UserController();
            User user = new User { Login = GetRandomString(MIN), Password = GetRandomString(12) };

            ActionResult actualRegistration = controller.PostRegistration(user);

            Assert.IsType(OkType, actualRegistration);
        }
        
        [Fact]
        public void Register_MinAllowPasswordLength_Returns200()
        {
            UserController controller = new UserController();
            User user = new User { Login = GetRandomString(8), Password = GetRandomString(MIN) };

            ActionResult actualRegistration = controller.PostRegistration(user);

            Assert.IsType(OkType, actualRegistration);
        }
        
        [Fact]
        public void Register_MaxAllowLoginLength_Returns200()
        {
            UserController controller = new UserController();
            User user = new User { Login = GetRandomString(MAX), Password = GetRandomString(12) };

            ActionResult actualRegistration = controller.PostRegistration(user);

            Assert.IsType(OkType, actualRegistration);

        }
        
        [Fact]
        public void Register_MaxAllowPasswordLength_Returns200()
        {
            UserController controller = new UserController();
            User user = new User { Login = GetRandomString(8), Password = GetRandomString(MAX) };

            ActionResult actualRegistration = controller.PostRegistration(user);

            Assert.IsType(OkType, actualRegistration);
        }
    }
}
