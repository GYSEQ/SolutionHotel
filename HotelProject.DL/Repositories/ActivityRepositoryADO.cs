using HotelProject.BL.Interfaces;
using HotelProject.BL.Model.HotelActivities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.DL.Repositories
{
    public class ActivityRepositoryADO : IActivityRepository
    {
        private string connectionString;

        public ActivityRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Activity> GetActivitiesByOrganizerId(int id)
        {
            List<Activity> activities = new List<Activity>();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand("SELECT * FROM activity WHERE organizerId = @id AND status = 1", connection);
                connection.Open();
                command.Parameters.AddWithValue("@id", id);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    activities.Add(new Activity
                        (
                        (string)reader["name"],
                        (int)reader["id"],
                        (string)reader["description"],
                        (DateTime)reader["date"],
                        (int)reader["spots"],
                        (decimal)reader["price_adult"],
                        (decimal)reader["price_child"],
                        (int)reader["discount"],
                        (string)reader["location"],
                        (int)reader["duration"]
                        )
                    );
                }
            }
            return activities;
        }   
    }
}
