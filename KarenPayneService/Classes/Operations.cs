using System;
using System.Configuration;
using System.Data.SqlClient;
using static System.DateTime;

namespace KarenPayneService.Classes
{
    public class Operations
    {
        /// <summary>
        /// NEEDS TO CHANGE ON YOUR PC
        /// Specifies the SQL-Server instance you might need to use SQLExpress or another server name
        /// </summary>
        private static string databaseServer = "KARENS-PC";
        /// <summary>
        /// NOTE: Name of database containing required tables
        /// </summary>
        static string defaultCatalog = "KarensServiceDatabase";
        /// <summary>
        /// Constructs our final connection string
        /// IMPORTANT: You need to change the user Id and Password for your database.
        /// </summary>
        string _connectionString = $"Data Source={databaseServer};Initial Catalog={defaultCatalog};User Id=KarenPayneDemo;Password=PasswordDemo";
        private string _exceptionMessage;
        public string ExceptionMessage { get { return _exceptionMessage; } }
        string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }
        public bool InsertMessage(string pMessage)
        {
            if (Environment.UserName != "Karens")
            {
                throw new OperationCanceledException("SQL-Server insance name needs inspection.");
            }

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
                        _exceptionMessage = ex.Message;
                        success = false;
                    }
                }
            }

            return success;
        }

    }
}
