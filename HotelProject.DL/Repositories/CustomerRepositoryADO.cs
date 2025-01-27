﻿using HotelProject.BL.Interfaces;
using HotelProject.BL.Model;
using HotelProject.BL.Model.Customer;
using HotelProject.DL.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.DL.Repositories
{
    public class CustomerRepositoryADO : ICustomerRepository
    {
        private string connectionString;

        public CustomerRepositoryADO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Customer> GetCustomers(string filter)
        {
            try
            {
                Dictionary<int, Customer> customers = new Dictionary<int, Customer>();
                string sql;
                if (string.IsNullOrEmpty(filter))
                {
                    sql = "select t1.id,t1.email,t1.name customername,t1.address,t1.phone, t2.id AS memberid, t2.name membername,t2.birthday\r\nfrom customer t1 \r\nleft join (select * from member where status=1) t2 on t1.id=t2.customerId\r\nwhere t1.status=1";
                }
                else
                {
                    sql = "select t1.id,t1.email,t1.name customername,t1.address,t1.phone, t2.id AS memberid, t2.name membername,t2.birthday\r\nfrom customer t1 \r\nleft join (select * from member where status=1) t2 on t1.id=t2.customerId\r\nwhere t1.status=1 and (t1.id like @filter or t1.name like @filter or t1.email like @filter)";
                }
                using(SqlConnection conn = new SqlConnection(connectionString)) 
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@filter", $"%{filter}%");
                    SqlDataReader reader=cmd.ExecuteReader();
                    if (reader.HasRows)
                        while (reader.Read())
                        {
                            int id= Convert.ToInt32(reader["ID"]);
                            if (!customers.ContainsKey(id)) //member toevoegen
                            {
                               customers.Add(id, new Customer((string)reader["customername"], (int)reader["id"], new ContactInfo((string)reader["email"], (string)reader["phone"], new Address((string)reader["address"]))));                              
                            }
                            if (!reader.IsDBNull(reader.GetOrdinal("membername")))
                            {
                                customers[id].AddMember(new Member((int)reader["memberid"], (string)reader["membername"], DateOnly.FromDateTime((DateTime)reader["birthday"])));
                            }                            
                        }
                    return customers.Values.ToList();
                }
            }
            catch(Exception ex)
            {
                throw new CustomerRepositoryException("GetCustomer",ex);
            }
        }
        public int AddCustomer(Customer customer)
        {
            try
            {
                string SQL = "INSERT INTO Customer(name,email,phone,address,status) output INSERTED.ID VALUES(@name,@email,@phone,@address,@status) ";
                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    SqlTransaction transaction=conn.BeginTransaction();
                    try
                    {
                        //write customer table
                        cmd.CommandText = SQL;
                        cmd.Transaction = transaction;
                        cmd.Parameters.AddWithValue("@name", customer.Name);
                        cmd.Parameters.AddWithValue("@email", customer.ContactInfo.Email);
                        cmd.Parameters.AddWithValue("@phone", customer.ContactInfo.Phone);
                        cmd.Parameters.AddWithValue("@address", customer.ContactInfo.Address.ToAddressLine());
                        cmd.Parameters.AddWithValue("@status", 1);
                        int id=(int)cmd.ExecuteScalar();
                        //write members table
                        SQL = "INSERT INTO member(name,birthday,customerid,status) VALUES(@name,@birthday,@customerid,@status) ";
                        cmd.CommandText=SQL;
                        
                        foreach(Member member in customer.GetMembers())
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@name",member.Name);
                            cmd.Parameters.AddWithValue("@birthday", member.BirthDay.ToDateTime(TimeOnly.MinValue));
                            cmd.Parameters.AddWithValue("@customerid", id);
                            cmd.Parameters.AddWithValue("@status", 1);
                            cmd.ExecuteNonQuery();
                        }
                        transaction.Commit();
                        return id;
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                        return 0;
                    }
                }
            }
            catch(Exception ex)
            {
                throw new CustomerRepositoryException("AddCustomer", ex);
            }
        }
        public void UpdateCustomer(Customer customer)
        {
            try
            {
                string customerSQL = "UPDATE Customer SET name=@name, email=@email, phone=@phone, address=@address WHERE id=@id";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(customerSQL, conn))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();
                    cmd.Transaction = transaction;

                    try
                    {
                        cmd.Parameters.AddWithValue("@name", customer.Name);
                        cmd.Parameters.AddWithValue("@email", customer.ContactInfo.Email);
                        cmd.Parameters.AddWithValue("@phone", customer.ContactInfo.Phone);
                        cmd.Parameters.AddWithValue("@address", customer.ContactInfo.Address.ToAddressLine());
                        cmd.Parameters.AddWithValue("@id", customer.Id);
                        cmd.ExecuteNonQuery();

                        // Update member table
                        // You may want to consider different strategies here depending on your requirements.
                        // For simplicity, I'm deleting all existing members and adding them again.
                        string deleteMembersSQL = "DELETE FROM Member WHERE customerId=@customerId";
                        cmd.CommandText = deleteMembersSQL;
                        cmd.Parameters.AddWithValue("@customerId", customer.Id);
                        cmd.ExecuteNonQuery();

                        string addMemberSQL = "INSERT INTO Member(name, birthday, customerId, status) VALUES (@name, @birthday, @customerId, @status)";
                        cmd.CommandText = addMemberSQL;

                        foreach (Member member in customer.GetMembers())
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@name", member.Name);
                            cmd.Parameters.AddWithValue("@birthday", member.BirthDay.ToDateTime(TimeOnly.MinValue));
                            cmd.Parameters.AddWithValue("@customerId", customer.Id);
                            cmd.Parameters.AddWithValue("@status", 1);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("UpdateCustomer", ex);
            }
        }
        public void DeleteCustomer(int customerId)
        {
            try
            {
                string sql = "UPDATE Customer SET status = 0 WHERE id = @id; UPDATE Member SET status = 0 WHERE customerId = @id;";

                using (SqlConnection conn = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", customerId);
                    int affectedRows = cmd.ExecuteNonQuery();

                    if (affectedRows == 0)
                    {
                        throw new CustomerRepositoryException("No customer was found with the specified ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("DeleteCustomer", ex);
            }
        }
    }
}
