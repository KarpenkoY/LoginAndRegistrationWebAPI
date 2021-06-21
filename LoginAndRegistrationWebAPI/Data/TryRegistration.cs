using System.Data;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace LoginAndRegistrationWebAPI.Data
{
    internal static partial class Database
    {
        internal static async Task<bool> TryRegistration(string login, string passwordHash, byte[] salt)
        {
            string sqe = "INSERT INTO Users (login, passwordHash, salt)" +
                        $"VALUES (@login, @hash, @salt)";

            SqlCommand command = new SqlCommand(sqe, Connection);
            command.Parameters.Add("@login", SqlDbType.VarChar, 32);
            command.Parameters.Add("@hash", SqlDbType.VarChar, 255);
            command.Parameters.Add("@salt", SqlDbType.VarBinary, 16);

            command.Parameters["@login"].Value = login;
            command.Parameters["@hash"].Value = passwordHash;
            command.Parameters["@salt"].Value = salt;

            await Connection.OpenAsync();
            int insertResult = await command.ExecuteNonQueryAsync();
            await Connection.CloseAsync();

            return insertResult == 1;
        }
    }
}
