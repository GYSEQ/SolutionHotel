using HotelProject.BL.Interfaces;
using HotelProject.BL.Model.Customer;
using HotelProject.BL.Model;
using HotelProject.DL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HotelProject.BL.Model.HotelActivities;
using System.Xml.Linq;

namespace HotelProject.DL.Repositories
{
    public class OrganizerRepositoryADO : IOrganizerRepository
    {
        private string connectionString;

        public OrganizerRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Organizer> GetOrganizers()
        {
            try
            {
                List<Organizer> organizers = new List<Organizer>();

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM Organizer WHERE status = 1", connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        Organizer organizer = new Organizer((int)reader["Id"], (string)reader["Name"], new ContactInfo((string)reader["Email"], (string)reader["Phone"], new Address((string)reader["Address"])));
                        organizers.Add(organizer);
                    }
                }

                return organizers;
            }
            catch (Exception ex)
            {
                throw new OrganizerRepositoryException("GetOrganizers", ex);
            }
        }
    }
}
