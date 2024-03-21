using Medication_Request.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static Medication_Request.Models.MedicationRequest;

namespace Medication_Request.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicationRequestController : ControllerBase
    {
        private readonly ILogger<MedicationRequestController> _logger;
        private readonly MedicationRequestDBContext _dbContext;

        public MedicationRequestController(MedicationRequestDBContext medicationDbContext, ILogger<MedicationRequestController> logger)
        {
            _dbContext = medicationDbContext;
            _logger = logger;
        }

        //public MedicationRequestController(ILogger<MedicationRequestController> logger)
        //{
        //    _logger = logger;
        //}

        [HttpPost]
        [Route("/medicationRequest")]
        public async Task<HttpResponseMessage> PostMedicationRequest(MedicationRequest medicationRequest)
        {
            if(medicationRequest == null || string.IsNullOrEmpty(medicationRequest.PatientReference))
            {
                _logger.LogError("Medication request is not defined.");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            try
            {
                medicationRequest.Status = RequestStatus.active;
                _dbContext.MedicationRequest.Add(medicationRequest);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation("Medication request is created.");

            }catch(Exception ex)
            {
                _logger.LogError("Exception raised while saving the request. Exception details : {0}", ex.Message);
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}
