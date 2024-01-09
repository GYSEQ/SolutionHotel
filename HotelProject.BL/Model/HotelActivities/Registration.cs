using HotelProject.BL.Exceptions;
using HotelProject.BL.Model.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Model.HotelActivities
{
    public class Registration
    {
        private int _id;
        public int Id
        {
            get => _id;
            set => _id = value > 0 ? value : throw new RegistrationException("ID must be greater than 0.");
        }

        private int _activityId;
        public int ActivityId
        {
            get => _activityId;
            set => _activityId = value > 0 ? value : throw new RegistrationException("ActivityId must be greater than 0.");
        }

        private int _customerId;
        public int CustomerId
        {
            get => _customerId;
            set => _customerId = value > 0 ? value : throw new RegistrationException("CustomerId must be greater than 0.");
        }

        private decimal _totalCost;
        public decimal TotalCost
        {
            get => _totalCost;
            set => _totalCost = value >= 0 ? value : throw new RegistrationException("TotalCost cannot be negative.");
        }

        private List<Member> _members = new List<Member>();
        public List<Member> Members
        {
            get => _members;
            set => _members = value ?? throw new RegistrationException("Members cannot be null.");
        }
        private Activity _activity;
        public Activity Activity
        {
            get => _activity;
            set => _activity = value ?? throw new RegistrationException("Activity cannot be null.");
        }

        public void CalculateTotalCost()
        {
            TotalCost = Members.Sum(m => m.GetAge() >= 12 ? Activity.PriceAdult : Activity.PriceChild);
        }
    }
}
