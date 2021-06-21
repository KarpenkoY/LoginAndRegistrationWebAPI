using Microsoft.Data.SqlClient;

namespace LoginAndRegistrationWebAPI.Data
{
    internal static partial class Database
    {
        internal static string FileName => "UsersData";
        internal static SqlConnection Connection { get; set; }

        static Database()
        {
            string connectionString = "Server=(localdb)\\mssqllocaldb;" +
                                     $"Database={FileName};" +
                                      "Trusted_Connection=True;";

            Connection = new SqlConnection(connectionString);
        }
    }
}
