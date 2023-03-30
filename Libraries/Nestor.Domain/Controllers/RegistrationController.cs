using Nestor.Domain.Controllers.Interfaces;
using Nestor.Shared;

namespace Nestor.Domain.Controllers
{
    public class RegistrationController : IRegistrationController
    {
       
        public RegistrationRequestResult AnnounceRegistrationInterest(RegistrationRequest request)
        {
            var answer = new RegistrationRequestResult()
            {
                RequestorGuid = request.Guid,
                RegistrationGuid = Guid.NewGuid(),
                //NestorPublic
                DateTime = DateTime.Now
            };
            return answer;
        }

        public RegistrationResult ExecuteRegistration(Registration registration)
        {
            return new RegistrationResult() { };
        }
    }
}