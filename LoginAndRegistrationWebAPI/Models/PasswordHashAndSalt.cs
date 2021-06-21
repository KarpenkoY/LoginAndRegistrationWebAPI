using System;

namespace LoginAndRegistrationWebAPI.Models
{
    internal struct PasswordHashAndSalt
    {
        internal string Hash { get; set; }
        internal byte[] Salt { get; set; }
    }
}
