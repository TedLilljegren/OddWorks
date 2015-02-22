using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlServerCe;
using System.Linq;
using System.Web;

namespace OddWorks.Repositories
{
    /*
     * To be inherited by any repository that uses the database
     */
    public abstract class DataBaseRepository
    {
        private const string ConnectionString = "ConnectionString";
        protected SqlCeConnection OpenConnection()
        {
            string connectionString = ConfigurationManager.AppSettings[ConnectionString];

            SqlCeConnection connection = new SqlCeConnection(connectionString);
            connection.Open();

            return connection;
        }
    }
}