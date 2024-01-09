using HotelProject.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Model.HotelActivities
{
    public class Activity
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => _name = !string.IsNullOrWhiteSpace(value) ? value : throw new ActivityException("Name cannot be empty.");
        }

        private int? _id;
        public int? Id
        {
            get => _id;
            set => _id = value > 0 ? value : throw new ActivityException("ID must be greater than 0.");
        }

        private string _description;
        public string Description
        {
            get => _description;
            set => _description = !string.IsNullOrWhiteSpace(value) ? value : throw new ActivityException("Description cannot be empty.");
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => _date = value > DateTime.MinValue ? value : throw new ActivityException("Invalid date.");
        }

        private int _spots;
        public int Spots
        {
            get => _spots;
            set => _spots = value >= 0 ? value : throw new ActivityException("Spots cannot be negative.");
        }

        private decimal _priceAdult;
        public decimal PriceAdult
        {
            get => _priceAdult;
            set => _priceAdult = value >= 0 ? value : throw new ActivityException("Adult price cannot be negative.");
        }

        private decimal _priceChild;
        public decimal PriceChild
        {
            get => _priceChild;
            set => _priceChild = value >= 0 ? value : throw new ActivityException("Child price cannot be negative.");
        }

        private int _discount;
        public int Discount
        {
            get => _discount;
            set => _discount = value >= 0 ? value : throw new ActivityException("Discount cannot be negative.");
        }

        private string _location;
        public string Location
        {
            get => _location;
            set => _location = !string.IsNullOrWhiteSpace(value) ? value : throw new ActivityException("Location cannot be empty.");
        }

        private int _duration;
        public int Duration
        {
            get => _duration;
            set => _duration = value >= 0 ? value : throw new ActivityException("Duration cannot be negative.");
        }

        public int _organizerId;
        public int OrganizerId
        {
            get => _organizerId;
            set => _organizerId = value > 0 ? value : throw new ActivityException("Organizer ID must be greater than 0.");
        }

        public Activity(string name, int id, string description, DateTime date, int spots, decimal priceAdult, decimal priceChild, int discount , string location, int duration, int organizerId)
        {
            Name = name;
            Id = id;
            Description = description;
            Date = date;
            Spots = spots;
            PriceAdult = priceAdult;
            PriceChild = priceChild;
            Discount = discount;
            Location = location;
            Duration = duration;
            OrganizerId = organizerId;
        }

        public Activity(string name, string description, DateTime date, int spots, decimal priceAdult, decimal priceChild, int discount, string location, int duration, int organizerId)
        {
            Name = name;
            Description = description;
            Date = date;
            Spots = spots;
            PriceAdult = priceAdult;
            PriceChild = priceChild;
            Discount = discount;
            Location = location;
            Duration = duration;
            OrganizerId = organizerId;
        }
    }
}
