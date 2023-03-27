using Nestor.Shared;

namespace Nestor.Domain.Controllers.Interfaces
{
    public interface IRegistrationController
    {
        RegistrationRequestResult AnnounceRegistrationInterest(RegistrationRequest request);
        RegistrationResult ExecuteRegistration(Registration registration);
    }
}