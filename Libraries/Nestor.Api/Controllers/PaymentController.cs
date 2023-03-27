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
    public partial class PaymentController : ControllerBase //, IPaymentController
    {

        private readonly ILogger<RegistrationController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IPaymentController _paymentController;

        private readonly Dictionary<Guid, PaymentRequestAnswerWrapper> _paymentRequests = new();

        public PaymentController(ILogger<RegistrationController> logger, IConfiguration configuration, IPaymentController paymentController)
        {
            _logger = logger;
            _configuration = configuration;
            _paymentController = paymentController;
        }

        [HttpPost("[action]")]
        public ActionResult<string> AnnouncePaymentInterest([FromBody] PaymentInterestDTO paymentInterestDto)
        {
            var request = new PaymentRequest() { Guid = paymentInterestDto.PaymentRequestId, DateTime = DateTime.Now, RequestorPublic = paymentInterestDto.PublicKey };
            var answer = _paymentController.AnnouncePaymentInterest(request);
            var rsaCryptoServiceProvider = new RSACryptoServiceProvider(4096); // set generally, not in config file
            answer.NestorPublic = rsaCryptoServiceProvider.ToXmlString(false);
            string privateAndPublicKeyXML = rsaCryptoServiceProvider.ToXmlString(true);
            _paymentRequests.Add(answer.RegistrationGuid, new PaymentRequestAnswerWrapper() { Answer = answer, PrivateKey = privateAndPublicKeyXML });
            var answerSerialized = JsonSerializer.Serialize(answer);
            string encrypted = CryptoManager.Encrypt(answerSerialized, paymentInterestDto.PublicKey);
            return Ok(encrypted);
        }

        [HttpPost("[action]")]
        public ActionResult<string> ExecutePayment([FromBody] PaymentDTO paymentDto)
        {
            if (_paymentRequests.ContainsKey(paymentDto.PaymentId))
            {
                if (!string.IsNullOrEmpty(_paymentRequests[paymentDto.PaymentId]?.PrivateKey))
                {
                    var rsaCryptoServiceProvider = new RSACryptoServiceProvider(4096);
                    rsaCryptoServiceProvider.FromXmlString(_paymentRequests[paymentDto.PaymentId].PrivateKey);
                    var decrypted = CryptoManager.Decrypt(paymentDto.Encrypted, rsaCryptoServiceProvider.ExportParameters(true));
                    Payment payment = JsonSerializer.Deserialize<Payment>(decrypted);
                    var answer = _paymentController.ExecutePayment(payment);
                    // TODO: encrypt
                    return Ok("?");
                }
                return BadRequest("?");
            }
            return NotFound("?");
        }
    }
}