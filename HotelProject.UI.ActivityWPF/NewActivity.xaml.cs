using HotelProject.BL.Managers;
using HotelProject.BL.Model.Customer;
using HotelProject.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HotelProject.BL.Model.HotelActivities;

namespace HotelProject.UI.ActivityWPF
{
    /// <summary>
    /// Interaction logic for NewActivity.xaml
    /// </summary>
    public partial class NewActivity : Window
    {
        public NewActivity()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Activity a = new Activity();
            int id = customerManager.AddCustomer(c);
            customerUI = new CustomerUI(id, c.Name, c.ContactInfo.Email, c.ContactInfo.Phone, c.ContactInfo.Address.ToString(), c.GetMembers().Count, new List<Member>(c.GetMembers()));
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
