using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.UI.ActivityWPF.Model
{
    public class OrganizerUI
    {
        public OrganizerUI(int id, string name, string phone, string email, string address)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set;}
    }
}
