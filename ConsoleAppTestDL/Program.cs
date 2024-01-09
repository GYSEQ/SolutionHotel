using HotelProject.BL.Model;
using HotelProject.BL.Model.Customer;
using HotelProject.DL.Repositories;

namespace ConsoleAppTestDL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            string connectionString = "Data Source=NB21-6CDPYD3\\SQLEXPRESS;Initial Catalog=HotelWoensdag;Integrated Security=True";
            CustomerRepositoryADO repo = new CustomerRepositoryADO(connectionString);
            //var x=repo.GetCustomers("jo");
            Customer customer = new Customer("Fred", new ContactInfo("fred@gmail","0123456789",new Address("gent","9000","12f","kerkstraat")));
            repo.AddCustomer(customer);

        }
    }
}