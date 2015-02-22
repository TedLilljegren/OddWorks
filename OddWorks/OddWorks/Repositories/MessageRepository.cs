using OddWorks.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.Linq;
using System.Web;

namespace OddWorks.Repositories
{
    public class MessageRepository : DataBaseRepository, IMessageRepository
    {

        public bool InsertMessage(Message message)
        {
            bool inserted = false;

            using (var dbConnection = this.OpenConnection()) {
                //Using parameterized sql command protects against SQL injection attacks
                SqlCeCommand cmd = dbConnection.CreateCommand();
                cmd.CommandText = "INSERT INTO tbl_Messages ([content]) Values(@content)";
                cmd.Parameters.Add("@content", SqlDbType.NVarChar).Value = message.Content;
                
                cmd.ExecuteNonQuery();
                inserted = true;
            }

            return inserted;
        }


        public List<Message> GetMessages()
        {
            var messages = new List<Message>();
            using (var dbConnection = this.OpenConnection())
            {
                SqlCeCommand cmd = dbConnection.CreateCommand();
                cmd.CommandText = "SELECT * FROM tbl_Messages";

                using (SqlCeDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var message = new Message();
                        message.Id = (int)(reader["id"]);
                        message.Date = (DateTime)(reader["date"]);
                        message.Content = (string)(reader["content"]);
                        messages.Add(message);
                    }
                }

                //Sort with LinQ
                messages= messages.OrderBy(message => message.Date).ToList();
            }

            return messages;
        }
    }
}