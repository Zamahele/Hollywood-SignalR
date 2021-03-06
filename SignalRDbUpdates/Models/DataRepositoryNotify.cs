﻿using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using SignalRDbUpdates.Hubs;

namespace SignalRDbUpdates.Models
{
    public class DataRepositoryNotify
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public IEnumerable<BLL.EventDetail.EventDetail> GetAllMessages()
        {
            var messages = new List<BLL.EventDetail.EventDetail>();
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"SELECT [EventDetailId] ,[EventDetailName] ,[EventDetailNumber] ,[EventDetailOdd],[FinishingPosition] ,[FirstTimer] FROM [dbo].[EventDetails]", connection))
                {
                    command.Notification = null;

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(Dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        messages.Add(item: new BLL.EventDetail.EventDetail
                        {
                            EventDetailId = (int)reader["EventDetailId"], 
                            EventDetailName = (string)reader["EventDetailName"], 
                            EventDetailOdd = (decimal)reader["EventDetailOdd"] ,
                            FirstTimer = (int)reader["FirstTimer"], 
                            EventDetailNumber = (int)reader["EventDetailNumber"],
                            FinishingPosition = !reader.IsDBNull(reader.GetOrdinal("FinishingPosition")) ? (int?)reader["FinishingPosition"] : null
                        });
                    }
                }

            }
            return messages;
        }

        private void Dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                MessagesHub.SendMessages();

            }
        }
    }
}