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
    public class OrganizerManager
    {
        private IOrganizerRepository _organizerRepository;

        public OrganizerManager(IOrganizerRepository organizerRepository)
        {
            _organizerRepository = organizerRepository;
        }

        public List<Organizer> GetOrganizers()
        {
            try
            {
                return _organizerRepository.GetOrganizers();
            }
            catch(Exception ex)
            {
                throw new OrganizerManagerException("GetAllOrganizer",ex);
            }
        }
    }
}
