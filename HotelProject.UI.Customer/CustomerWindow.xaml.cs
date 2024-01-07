using HotelProject.BL.Managers;
using HotelProject.BL.Model;
using HotelProject.BL.Model.Customer;
using HotelProject.UI.CustomerWPF.Model;
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

namespace HotelProject.UI.CustomerWPF
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public CustomerUI customerUI;
        private CustomerManager customerManager;
        private bool isUpdate;
        public CustomerWindow(bool isUpdate,CustomerUI customerUI, CustomerManager cm)
        {
            InitializeComponent();
            this.customerUI = customerUI;
            this.isUpdate = isUpdate;
            this.customerManager = cm;
            if (customerUI != null )
            {
                IdTextBox.Text = customerUI.Id.ToString();
                NameTextBox.Text = customerUI.Name;
                EmailTextBox.Text = customerUI.Email;
                PhoneTextBox.Text = customerUI.Phone;
                string[] parts = customerUI.Address.Split(new char[] { '|' });
                HouseNumberTextBox.Text = parts[3];
                StreetTextBox.Text = parts[2];
                CityTextBox.Text = parts[0];
                ZipTextBox.Text = parts[1];
                MemberDataGrid.ItemsSource = customerUI.Members;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (isUpdate)
            {
                customerUI.Name = NameTextBox.Text;
                customerUI.Email = EmailTextBox.Text;
                customerUI.Phone = PhoneTextBox.Text;
                customerUI.Address = $"{CityTextBox.Text}|{StreetTextBox.Text}|{ZipTextBox.Text}|{HouseNumberTextBox.Text}";
                customerManager.UpdateCustomer(new Customer(customerUI.Name, (int)customerUI.Id, new ContactInfo(customerUI.Email, customerUI.Phone, new Address(customerUI.Address))));
            }
            else
            {
                Customer c = new Customer(NameTextBox.Text, new ContactInfo(EmailTextBox.Text, PhoneTextBox.Text, new Address(CityTextBox.Text, ZipTextBox.Text, HouseNumberTextBox.Text, StreetTextBox.Text)));
                int id = customerManager.AddCustomer(c);
                customerUI = new CustomerUI(id, c.Name, c.ContactInfo.Email, c.ContactInfo.Phone, c.ContactInfo.Address.ToString(), c.GetMembers().Count, new List<Member>(c.GetMembers()));
            }
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
