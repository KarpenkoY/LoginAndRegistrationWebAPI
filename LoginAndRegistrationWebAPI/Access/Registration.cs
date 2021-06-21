using System;
using LoginAndRegistrationWebAPI.Data;
using LoginAndRegistrationWebAPI.Models;

namespace LoginAndRegistrationWebAPI.Access
{
    internal static partial class AccessControl
    {
        internal static string Registration(User user)
        {
            byte[] salt = GetSalt(16);

            bool successedRegistration;
            successedRegistration = Database.TryRegistration
                (user.Login, GetPasswordHash(user.Password, salt), salt).Result;

            if (!successedRegistration) return String.Empty;

            return GetToken(user.Login);
        }
    }
}
