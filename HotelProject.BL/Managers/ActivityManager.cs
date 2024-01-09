using HotelProject.BL.Exceptions;
using HotelProject.BL.Interfaces;
using HotelProject.BL.Model.HotelActivities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Managers
{
    public class ActivityManager
    {
        private IActivityRepository _activityRepository;

        public ActivityManager(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public List<Activity> GetActivitiesByOrganizerId(int id)
        {
            try
            {
                return _activityRepository.GetActivitiesByOrganizerId(id);
            }
            catch(Exception ex)
            {
                throw new ActivityManagerException("GetActivitiesByOrganizerId",ex);
            }
        }

        public int AddActivity(Activity activity)
        {
            try
            {
                return _activityRepository.AddActivity(activity);
            }
            catch(Exception ex)
            {
                throw new ActivityManagerException("AddActivity", ex);
            }
        }
    }
}
