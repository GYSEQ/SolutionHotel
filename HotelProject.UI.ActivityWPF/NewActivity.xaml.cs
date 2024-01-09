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
using HotelProject.UI.ActivityWPF.Model;

namespace HotelProject.UI.ActivityWPF
{
    /// <summary>
    /// Interaction logic for NewActivity.xaml
    /// </summary>
    public partial class NewActivity : Window
    {
        private ActivityManager am;
        public ActivityUI _activityUI;
        private OrganizerUI _organizerUI;
        public NewActivity(ActivityManager am, OrganizerUI organizerUI)
        {
            InitializeComponent();
            txtActivityDate.SelectedDate = DateTime.Today;
            this.am = am;
            _organizerUI = organizerUI;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Activity a = new Activity(txtActivityName.Text, txtActivityDescription.Text, (DateTime)txtActivityDate.SelectedDate, int.Parse(txtActivitySpots.Text), decimal.Parse(txtActivityPriceAdult.Text), decimal.Parse(txtActivityPriceChild.Text), int.Parse(txtActivityDiscount.Text), txtActivityLocation.Text, int.Parse(txtActivityDuration.Text), _organizerUI.Id);
            int id = am.AddActivity(a);
            _activityUI = new ActivityUI(id, a.Name, a.Description, a.Date, a.Spots, a.PriceAdult, a.PriceChild, a.Discount, a.Location, a.Duration);
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
