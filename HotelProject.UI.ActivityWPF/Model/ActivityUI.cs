using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProject.UI.ActivityWPF.Model
{
    public class ActivityUI : INotifyPropertyChanged
    {
        private int? _id;
        public int? Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _spots;
        public int Spots
        {
            get => _spots;
            set
            {
                if (_spots != value)
                {
                    _spots = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _priceAdult;
        public decimal PriceAdult
        {
            get => _priceAdult;
            set
            {
                if (_priceAdult != value)
                {
                    _priceAdult = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal _priceChild;
        public decimal PriceChild
        {
            get => _priceChild;
            set
            {
                if (_priceChild != value)
                {
                    _priceChild = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _discount;
        public int Discount
        {
            get => _discount;
            set
            {
                if (_discount != value)
                {
                    _discount = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _location;
        public string Location
        {
            get => _location;
            set
            {
                if (_location != value)
                {
                    _location = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if (_duration != value)
                {
                    _duration = value;
                    OnPropertyChanged();
                }
            }
        }

        public ActivityUI(int? id, string name, string description, DateTime date, int spots, decimal priceAdult, decimal priceChild, int discount, string location, int duration)
        {
            Id = id;
            Name = name;
            Description = description;
            Date = date;
            Spots = spots;
            PriceAdult = priceAdult;
            PriceChild = priceChild;
            Discount = discount;
            Location = location;
            Duration = duration;
        }

        private void OnPropertyChanged(string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
