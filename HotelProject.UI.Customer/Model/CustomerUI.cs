﻿using HotelProject.BL.Model.Customer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.UI.CustomerWPF.Model
{
    public class CustomerUI : INotifyPropertyChanged
    {
        public CustomerUI(int? id, string name, string email, string phone, string address, int nrOfMembers, List<Member> members)
        {
            this.id = id;
            this.name = name;
            this.email = email;
            this.phone = phone;
            this.address = address;
            this.nrOfMembers = nrOfMembers;
            this.members = members;
        }

        private int? id;
        public int? Id { get { return id; } set { id = value; OnPropertyChanged(); } }
        private string name;
        public string Name { get { return name; } set { name = value; OnPropertyChanged(); } }
        private string email;
        public string Email { get { return email; } set { email = value; OnPropertyChanged(); } }
        private string phone;
        public string Phone { get { return phone; } set { phone = value; OnPropertyChanged(); } }
        private string address;
        public string Address { get { return address; } set { address = value; OnPropertyChanged(); } }
        private int nrOfMembers;
        public int NrOfMembers { get { return nrOfMembers; } set { nrOfMembers = value; OnPropertyChanged(); } }
        private List<Member> members = new List<Member>();
        public List<Member> Members { get { return members; } set { members = value; OnPropertyChanged(); } }

        private void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}