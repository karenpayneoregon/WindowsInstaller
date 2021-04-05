using System;
using System.Configuration;
using System.Data.SqlClient;
using static System.DateTime;

namespace KarenPayneService.Classes
{
    public class SqlServerOperations
    {
        /// <summary>
        /// NEEDS TO CHANGE ON YOUR PC
        /// Specifies the SQL-Server instance you might need to use SQLExpress or another server name
        /// </summary>
        private static string databaseServer = ".\\SQLEXPRESS";
        /// <summary>
        /// NOTE: Name of database containing required tables
        /// </summary>
        static string defaultCatalog = "KarensServiceDatabase";

        public string ExceptionMessage { get; private set; }

        /// <summary>
        /// Constructs our final connection string
        /// IMPORTANT: You need to change the user Id and Password for your database.
        /// </summary>
        private string ConnectionString { get; set; } = $"Data Source={databaseServer};Initial Catalog={defaultCatalog};User Id=KarenPayneDemo;Password=PasswordDemo";

        public bool InsertMessage(string pMessage)
        {
            //if (Environment.UserName != "SYSTEM")
            //{
            //    throw new OperationCanceledException("SQL-Server instance name needs inspection.");
            //}

            var currentUser = Environment.UserName;
            bool success;
            using (var cn = new SqlConnection { ConnectionString = ConnectionString })
            {
                using (var cmd = new SqlCommand { Connection = cn})
                {
                    cmd.CommandText = "INSERT INTO MessagesFromService (MessageText ,ModifiedDateTime) VALUES (@ServiceMessage,@ModifiedDateTime)";
                    cmd.Parameters.AddWithValue("@ServiceMessage", pMessage);
                    cmd.Parameters.AddWithValue("@ModifiedDateTime", Now);
                    try
                    {
                        cn.Open();
                        cmd.ExecuteNonQuery();
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        // most likely failure is permissions to connection or write to the database
                        ExceptionMessage = ex.Message;
                        success = false;
                    }
                }
            }

            return success;
        }

    }
}
