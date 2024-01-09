using HotelProject.BL.Managers;
using HotelProject.BL.Model.Customer;
using HotelProject.BL.Model.HotelActivities;
using HotelProject.UI.ActivityWPF.Model;
using HotelProject.UI.CustomerWPF.Model;
using HotelProject.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace HotelProject.UI.ActivityWPF
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private ActivityManager _activityManager;
        private CustomerManager _customerManager;
        private OrganizerManager _organizerManager;
        private RegistrationManager _registrationManager;
        private List<OrganizerUI> organizers = new List<OrganizerUI>();
        private OrganizerUI _selectedOrganizer;
        private ActivityUI _selectedActivity;
        private List<Member> _notSelectedMembers = new List<Member>();
        private Registration _registration = new Registration();
        private ObservableCollection<CustomerUI> customersUIs = new ObservableCollection<CustomerUI>();
        public RegisterWindow()
        {
            InitializeComponent();
            _activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            _customerManager = new CustomerManager(RepositoryFactory.CustomerRepository);
            _organizerManager = new OrganizerManager(RepositoryFactory.OrganizerRepository);
            _registrationManager = new RegistrationManager(RepositoryFactory.RegistrationRepository);

            foreach (Organizer organizer in _organizerManager.GetOrganizers())
            {
                organizers.Add(new OrganizerUI(organizer.Id, organizer.Name, organizer.ContactInfo.Phone, organizer.ContactInfo.Email, organizer.ContactInfo.Address.ToAddressLine()));
            }
            DataGridOrganizers.ItemsSource = organizers;

            customersUIs = new ObservableCollection<CustomerUI>(_customerManager.GetCustomers(null).Where(x => x.Id == 1).Select(x => new CustomerUI(x.Id, x.Name, x.ContactInfo.Email, x.ContactInfo.Phone, x.ContactInfo.Address.ToAddressLine(), x.GetMembers().Count, new List<Member>(x.GetMembers()))));
            _notSelectedMembers = customersUIs[0].Members;
            DataGridMembers.Items.Clear();
            DataGridMembers.ItemsSource = _notSelectedMembers;
            DataGridSelectedMembers.ItemsSource = _registration.Members;
        }

        private void DataGridOrganizers_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DataGridActivities.ItemsSource = new ObservableCollection<ActivityUI>(_activityManager.GetActivitiesByOrganizerId(((OrganizerUI)DataGridOrganizers.SelectedItem).Id).Select(x => new ActivityUI(x.Id, x.Name, x.Description, x.Date, x.Spots, x.PriceAdult, x.PriceChild, x.Discount, x.Location, x.Duration)));
            _selectedOrganizer = ((OrganizerUI)DataGridOrganizers.SelectedItem);
            txtOrganisatie.Content = _selectedOrganizer.Name;
            _selectedActivity = null;
            txtActiviteit.Content = "";
        }

        private void DataGridActivities_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _selectedActivity  = ((ActivityUI)DataGridActivities.SelectedItem);
            txtActiviteit.Content = _selectedActivity.Name;
            _registration.Activity = new Activity(_selectedActivity.Name, _selectedActivity.Description, _selectedActivity.Date, _selectedActivity.Spots, _selectedActivity.PriceAdult, _selectedActivity.PriceChild, _selectedActivity.Discount, _selectedActivity.Location, _selectedActivity.Duration, _selectedOrganizer.Id);
            _registration.Activity.Id = _selectedActivity.Id;
        }

        private void DataGridMembers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _registration.Members.Add((Member)DataGridMembers.SelectedItem);
            _notSelectedMembers.Remove((Member)DataGridMembers.SelectedItem);
            DataGridMembers.Items.Refresh();
            DataGridSelectedMembers.Items.Refresh();
            _registration.CalculateTotalCost();
            txtTotaal.Content = _registration.TotalCost;
        }

        private void DataGridSelectedMembers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _registration.Members.Remove((Member)DataGridSelectedMembers.SelectedItem);
            _notSelectedMembers.Add((Member)DataGridSelectedMembers.SelectedItem);
            DataGridMembers.Items.Refresh();
            DataGridSelectedMembers.Items.Refresh();
            _registration.CalculateTotalCost();
            txtTotaal.Content = _registration.TotalCost;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (_registration.Activity == null)
            {
                MessageBox.Show("Selecteer een activiteit.");
                return;
            }
            if (_registration.Members.Count == 0)
            {
                MessageBox.Show("Selecteer minstens 1 lid.");
                return;
            }
            if (MessageBox.Show("Weet u zeker dat u wilt registreren?", "Registreren", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _registration.CustomerId = 1;
                _registrationManager.AddRegistration(_registration);
                MessageBox.Show("Registratie succesvol.");
                this.Close();
            }
        }
    }
}
