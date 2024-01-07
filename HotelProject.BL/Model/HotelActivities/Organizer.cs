using HotelProject.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.BL.Model.HotelActivities
{
    public class Organizer
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => _name = !string.IsNullOrWhiteSpace(value) ? value : throw new OrganizerException("Name cannot be empty.");
        }

        private int _id;
        public int Id
        {
            get => _id;
            set => _id = value > 0 ? value : throw new OrganizerException("ID must be greater than 0.");
        }

        private ContactInfo _contactInfo;
        public ContactInfo ContactInfo
        {
            get => _contactInfo;
            set => _contactInfo = value ?? throw new OrganizerException("ContactInfo cannot be null.");
        }

        public Organizer(int id, string name, ContactInfo contactInfo)
        {
            Id = id;
            Name = name;
            ContactInfo = contactInfo;
        }
    }
}