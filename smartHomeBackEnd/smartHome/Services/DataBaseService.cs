using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace smartHome.Services
{
    public class DataBaseService
    {
        private MySqlConnection mySqlConnection;

        public DataBaseService(IConfiguration configuration)
        {
            this.mySqlConnection = new MySqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
        }

        public void ExecuteNonQuery(string commandText, LinkedList<MySqlParameter> paramList)
        {
            mySqlConnection.Open();
            MySqlCommand command = mySqlConnection.CreateCommand();
            command.CommandText = commandText;
            foreach(MySqlParameter param in paramList)
            {
                command.Parameters.Add(param);
            }
            command.ExecuteNonQuery();
            mySqlConnection.Close();
        }
    }
}
