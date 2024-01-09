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
    public class RegistrationManager
    {
        IRegistrationRepository _registrationrepository;

        public RegistrationManager(IRegistrationRepository registrationrepository)
        {
            _registrationrepository = registrationrepository;
        }

        public void AddRegistration(Registration registration)
        {
            try
            {
                _registrationrepository.AddRegistration(registration);
            }
            catch(Exception ex)
            {
                throw new RegistrationManagerException("AddRegistration",ex);
            }
        }
    }
}
