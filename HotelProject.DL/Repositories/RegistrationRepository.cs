using HotelProject.BL.Interfaces;
using HotelProject.BL.Model.Customer;
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
    public class RegistrationRepository : IRegistrationRepository
    {
        private string connectionString;

        public RegistrationRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddRegistration(Registration registration)
        {
            string SQL = "INSERT INTO registration(activityId,customerId,total_cost, status) VALUES(@activityId,@customerId,@totalCost, @status)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    cmd.CommandText = SQL;
                    cmd.Transaction = transaction;
                    cmd.Parameters.AddWithValue("@activityId", registration.Activity.Id);
                    cmd.Parameters.AddWithValue("@customerId", registration.CustomerId);
                    cmd.Parameters.AddWithValue("@totalCost", registration.TotalCost);
                    cmd.Parameters.AddWithValue("@status", 1);
                    int regid = (int)cmd.ExecuteScalar();

                    SQL = "INSERT INTO registrationMember(registrationId,memberId) VALUES(@registrationId,@memberId) ";
                    cmd.CommandText = SQL;
                    foreach (Member member in registration.Members)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@registrationId", regid);
                        cmd.Parameters.AddWithValue("@memberId", member.Id);
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }

        }
    }
}
