using System.Runtime.Serialization;

namespace HotelProject.BL.Exceptions
{
    [Serializable]
    internal class RegistrationManagerException : Exception
    {
        public RegistrationManagerException()
        {
        }

        public RegistrationManagerException(string? message) : base(message)
        {
        }

        public RegistrationManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected RegistrationManagerException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}