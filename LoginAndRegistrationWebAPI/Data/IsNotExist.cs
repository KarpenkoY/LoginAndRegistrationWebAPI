using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace LoginAndRegistrationWebAPI.Data
{
    internal static partial class Database
    {
        internal static async Task<bool> IsLoginNotExist(string login)
        {
            string sq = $"SELECT id FROM Users WHERE login = '{login}'";
            return await IsNotExist(Connection, sq);
        }



        private static async Task<bool> IsNotExist(SqlConnection connection, string sqe)
        {
            SqlCommand command = new SqlCommand(sqe, connection);

            await Connection.OpenAsync();

            SqlDataReader reader = await command.ExecuteReaderAsync();
            bool isNotExist = !reader.HasRows;

            await Connection.CloseAsync();

            return isNotExist;
        }
    }
}
