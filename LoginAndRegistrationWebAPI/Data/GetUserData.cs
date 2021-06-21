using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using LoginAndRegistrationWebAPI.Models;

namespace LoginAndRegistrationWebAPI.Data
{
    internal static partial class Database
    {
        internal static async Task<PasswordHashAndSalt> GetUserData(string login)
        {
            string sqe = $"SELECT * FROM Users WHERE login = '{login}'";
            SqlCommand command                 = new SqlCommand(sqe, Connection);
            PasswordHashAndSalt passwordHash   = new PasswordHashAndSalt();

            await Connection.OpenAsync();

            SqlDataReader reader = await command.ExecuteReaderAsync();
            if (reader.HasRows)
            {
                await reader.ReadAsync();

                passwordHash.Hash   = reader.GetString(2);
                passwordHash.Salt   = (byte[])reader.GetValue(3);
            }

            await Connection.CloseAsync();

            return passwordHash;
        }
    }
}
