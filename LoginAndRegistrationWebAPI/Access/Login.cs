using LoginAndRegistrationWebAPI.Data;
using LoginAndRegistrationWebAPI.Models;

namespace LoginAndRegistrationWebAPI.Access
{
    internal static partial class AccessControl
    {
        internal static string Login(User user)
        {
            string token = "";

            if (Database.IsLoginNotExist(user.Login).Result) return token;
            
            PasswordHashAndSalt current = Database.GetUserData(user.Login).Result;
            string currentPasswordHash  = current.Hash;
            string inputPasswordHash    = GetPasswordHash(user.Password, current.Salt);

            if (inputPasswordHash == currentPasswordHash) 
            {
                token = GetToken(user.Login);
            }

            return token;
        }
    }
}
