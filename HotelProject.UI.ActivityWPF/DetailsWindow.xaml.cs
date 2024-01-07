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
    /// Interaction logic for DetailsWindow.xaml
    /// </summary>
    public partial class DetailsWindow : Window
    {
        private ActivityManager activityManager;
        private ObservableCollection<ActivityUI> activities = new ObservableCollection<ActivityUI>();
        public DetailsWindow(OrganizerUI org)
        {
            InitializeComponent();
            activityManager = new ActivityManager(RepositoryFactory.ActivityRepository);
            foreach (Activity activity in activityManager.GetActivitiesByOrganizerId(org.Id))
            {
                activities.Add(new ActivityUI(activity.Id, activity.Name, activity.Description, activity.Date, activity.Spots, activity.PriceAdult, activity.PriceChild, activity.Discount, activity.Location, activity.Duration));
            }
            ActivityDataGrid.ItemsSource = activities;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NewActivity w = new NewActivity();
            w.ShowDialog();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
