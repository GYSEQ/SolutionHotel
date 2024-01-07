using HotelProject.BL.Managers;
using HotelProject.BL.Model.HotelActivities;
using HotelProject.UI.ActivityWPF.Model;
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
    /// Interaction logic for OrganizerWindow.xaml
    /// </summary>
    public partial class OrganizerWindow : Window
    {
        private ObservableCollection<OrganizerUI> organizers = new ObservableCollection<OrganizerUI>();
        private OrganizerManager organizerManager;

        public OrganizerWindow()
        {
            InitializeComponent();
            organizerManager = new OrganizerManager(RepositoryFactory.OrganizerRepository);
            foreach(Organizer organizer in organizerManager.GetOrganizers())
            {
                organizers.Add(new OrganizerUI(organizer.Id, organizer.Name, organizer.ContactInfo.Phone, organizer.ContactInfo.Email, organizer.ContactInfo.Address.ToAddressLine()));
            }
            OrganizerDataGrid.ItemsSource = organizers;
        }

        private void OrganizerDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DetailsWindow w = new DetailsWindow((OrganizerUI)OrganizerDataGrid.SelectedItem);
            w.ShowDialog();
        }
    }
}