using Medication_Request.Controllers;
using Medication_Request.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using static Medication_Request.Models.MedicationRequest;

namespace Medication_Request_Tests
{
    public class MedicationRequestControllerTests
    {
        [Fact]
        public async void ExpextedTheMedicationRequestCreatedAfterPostSuccessfulAsync()
        {
            // Arrange

            MedicationRequest medRequest = new MedicationRequest()
            {
                PatientReference = "00111",
                ClinicianReference = "003432",
                MedicationReference = "3223434",
                ReasonText = "Post request test",
                PrescribedDate = new DateTime(2018, 06, 22).Date.ToString(),
                StartDate = new DateTime(2018, 06, 25).Date.ToString(),
                EndDate = null,
                Frequency = 2,
                Status = RequestStatus.active
            };

            var mockDBContext = new Mock<MedicationRequestDBContext>();
            var mockMedicationRequestSet = new Mock<DbSet<MedicationRequest>>();
            var mockLogger = new Mock<ILogger<MedicationRequestController>>();

            //mockDBContext.Setup(x => x.MedicationRequest.Add(medRequest)).Returns(mockMedicationRequestSet.Object);
            //var result = mockDBContext.Setup(x => x.SaveAsyncChanges()).Returns(Task.FromResult(medRequest));

            //Act
            MedicationRequestController medicationRequest = new MedicationRequestController(mockDBContext.Object, mockLogger.Object);
            HttpResponseMessage response = await medicationRequest.PostMedicationRequest(medRequest);

            //Assert
            Assert.True(response.IsSuccessStatusCode);
        }
    }
}