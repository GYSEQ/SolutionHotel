using HotelProject.BL.Interfaces;
using HotelProject.BL.Model.HotelActivities;
using HotelProject.DL.Exceptions;
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
                        (int)reader["duration"],
                        id
                        )
                    );
                }
            }
            return activities;
        }
        
        public int AddActivity(Activity activity)
        {
            string sql = "INSERT INTO activity (name, description, date, spots, price_adult, price_child, discount, location, duration, status, organizerId) output INSERTED.ID VALUES (@name, @description, @date, @spots, @price_adult, @price_child, @discount, @location, @duration, @status, @organizerId)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    cmd.CommandText = sql;
                    cmd.Transaction = transaction;
                    cmd.Parameters.AddWithValue("@name", activity.Name);
                    cmd.Parameters.AddWithValue("@description", activity.Description);
                    cmd.Parameters.AddWithValue("@date", activity.Date);
                    cmd.Parameters.AddWithValue("@spots", activity.Spots);
                    cmd.Parameters.AddWithValue("@price_adult", activity.PriceAdult);
                    cmd.Parameters.AddWithValue("@price_child", activity.PriceChild);
                    cmd.Parameters.AddWithValue("@discount", activity.Discount);
                    cmd.Parameters.AddWithValue("@location", activity.Location);
                    cmd.Parameters.AddWithValue("@duration", activity.Duration);
                    cmd.Parameters.AddWithValue("@status", 1);
                    cmd.Parameters.AddWithValue("@organizerId", activity.OrganizerId);
                    int id = (int)cmd.ExecuteScalar();

                    transaction.Commit();
                    return id;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new ActivityRepositoryException("AddActivity", ex);
                }
            }
        }
    }
}
