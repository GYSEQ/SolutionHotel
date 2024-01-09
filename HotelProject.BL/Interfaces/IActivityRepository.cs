using HotelProject.BL.Model.HotelActivities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Interfaces
{
    public interface IActivityRepository
    {
        public List<Activity> GetActivitiesByOrganizerId(int id);
        public int AddActivity(Activity activity);
    }
}
