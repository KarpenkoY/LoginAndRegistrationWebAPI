using System;
using Microsoft.Data.SqlClient;
using LoginAndRegistrationWebAPI.Access;
using LoginAndRegistrationWebAPI.Models;

namespace LoginAndRegistrationWebAPI.Data
{
    internal static partial class Database
    {
        internal static void EnsureCreated()
        {
            string csToMaster = "Server=(localdb)\\mssqllocaldb;" +
                                        "Database=master;" +
                                        "Trusted_Connection=True;";

            using (SqlConnection connectionToMaster = new SqlConnection(csToMaster))
            {
                if (CreateDatabase(connectionToMaster))
                {
                    CreateTables(Connection);
                    InsertUserAdmin(Connection);
                }
            }
        }



        private static bool CreateDatabase(SqlConnection connection)
        {
            string sqecDatabase = $"CREATE DATABASE {FileName}";

            SqlCommand ccDatabase = new SqlCommand(sqecDatabase, connection);
            connection.Open();

            try
            {
                ccDatabase.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }



        private static void CreateTables(SqlConnection connection)
        {
            string sqecGroupsTable =
                         "CREATE TABLE Groups (" +
                         "id INT PRIMARY KEY IDENTITY," +
                         "name NVARCHAR(255) NOT NULL)";

            string sqecUsersTable =
                         "CREATE TABLE Users (" +
                         "id INT PRIMARY KEY IDENTITY," +
                         "login VARCHAR(32) NOT NULL," +
                         "passwordHash VARCHAR(255) NOT NULL," +
                         "salt VARBINARY(16) NOT NULL)";

            string sqecUsersGroupsTable =
                         "CREATE TABLE UsersGroups (" +
                         "userId INT FOREIGN KEY REFERENCES Users(id)," +
                         "groupId INT FOREIGN KEY REFERENCES Groups(id)," +
                         "CONSTRAINT UserGroups PRIMARY KEY CLUSTERED(userId, groupId))";

            SqlCommand ccGroups         = new SqlCommand(sqecGroupsTable, connection);
            SqlCommand ccUsers          = new SqlCommand(sqecUsersTable, connection);
            SqlCommand ccUsersGroups    = new SqlCommand(sqecUsersGroupsTable, connection);

            connection.Open();

            ccGroups.ExecuteNonQuery();
            ccUsers.ExecuteNonQuery();
            ccUsersGroups.ExecuteNonQuery();

            connection.Close();
        }



        private static void InsertUserAdmin(SqlConnection connection)
        {
            AccessControl.Registration(new User { Login = "Admin", Password = "Admin" });
            
            string sqeiGroup =  "INSERT INTO Groups (name)" +
                                "VALUES ('Administrators')";

            string sqeiUserGroup =  "INSERT INTO UsersGroups (userId, groupId)"+
                                    "VALUES (1, 1)";
            
            SqlCommand ciGroup      = new SqlCommand(sqeiGroup, connection);
            SqlCommand ciUserGroup  = new SqlCommand(sqeiUserGroup, connection);

            connection.Open();

            ciGroup.ExecuteNonQuery();
            ciUserGroup.ExecuteNonQuery();

            connection.Close();
        }
    }
}
