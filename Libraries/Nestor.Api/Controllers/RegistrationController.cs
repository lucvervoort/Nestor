using Microsoft.AspNetCore.Mvc;
using Nestor.Api.Controllers.DTO;
using Nestor.Domain;
using Nestor.Domain.Controllers.Interfaces;
using Nestor.Infrastructure;
using System.Security.Cryptography;
using System.Text.Json;

namespace Nestor.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class RegistrationController : ControllerBase //, IRegistrationController
    {

        private readonly ILogger<RegistrationController> _logger;
        private readonly IConfiguration _configuration;   
        private readonly IRegistrationController _registrationController;

        private readonly Dictionary<Guid, RegistrationRequestAnswerWrapper> _registrationRequests = new();

        public RegistrationController(ILogger<RegistrationController> logger, IConfiguration configuration, IRegistrationController registrationController)
        {
            _logger = logger;
            _configuration = configuration;
            _registrationController = registrationController;
        }

        [HttpPost("[action]")]
        public ActionResult<string> EnregisterRegistrationInterest([FromBody] RegistrationInterestDTO registrationInterestDto)
        {
            var request = new RegistrationRequest() { Guid = registrationInterestDto.RegistrationRequestId, DateTime = DateTime.Now, RequestorPublic = registrationInterestDto.PublicKey };
            var answer = _registrationController.AnnounceRegistrationInterest(request);
            var rsaCryptoServiceProvider = new RSACryptoServiceProvider(4096); // set generally, not in config file
            answer.NestorPublic = rsaCryptoServiceProvider.ToXmlString(false);
            string privateAndPublicKeyXML = rsaCryptoServiceProvider.ToXmlString(true);
            _registrationRequests.Add(answer.RegistrationGuid, new RegistrationRequestAnswerWrapper() { Answer = answer, PrivateKey = privateAndPublicKeyXML });
            var answerSerialized = JsonSerializer.Serialize(answer);
            string encrypted = CryptoManager.Encrypt(answerSerialized, registrationInterestDto.PublicKey);
            return Ok(encrypted);
        }

        [HttpPost("[action]")]
        public ActionResult<string> PerformRegistration([FromBody] RegistrationDTO registrationDto)
        {
            if(_registrationRequests.ContainsKey(registrationDto.RegistrationId))
            {
                if (!string.IsNullOrEmpty(_registrationRequests[registrationDto.RegistrationId]?.PrivateKey))
                {
                    var rsaCryptoServiceProvider = new RSACryptoServiceProvider(4096);
                    rsaCryptoServiceProvider.FromXmlString(_registrationRequests[registrationDto.RegistrationId].PrivateKey);
                    var decrypted = CryptoManager.Decrypt(registrationDto.Encrypted, rsaCryptoServiceProvider.ExportParameters(true));
                    Registration registration = JsonSerializer.Deserialize<Registration>(decrypted);
                    var answer = _registrationController.ExecuteRegistration(registration);
                    // TODO: encrypt
                    return Ok("?");
                }
                return BadRequest("?");
            }
            return NotFound("?");
        }
    }
}