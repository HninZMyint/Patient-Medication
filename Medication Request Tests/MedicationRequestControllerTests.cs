using Medication_Request.Controllers;
using Medication_Request.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System.Net;
using static Medication_Request.Models.MedicationRequest;

namespace Medication_Request_Tests
{
    public class MedicationRequestControllerTests
    {
        [Fact]
        public async void ExpectedTheMedicationRequestCreatedAfterPostSuccessfulAsync()
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

            mockDBContext.Setup(x => x.MedicationRequest).Returns(mockMedicationRequestSet.Object);

            //Act
            MedicationRequestController medicationRequest = new MedicationRequestController(mockDBContext.Object, mockLogger.Object);
            HttpResponseMessage response = await medicationRequest.PostMedicationRequest(medRequest);

            //Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            mockDBContext.Verify(x => x.MedicationRequest.Add(It.IsAny<MedicationRequest>()), Times.Once());
            //mockDBContext.Verify(x => x.SaveChangesAsync(), Times.Once());
        }

        [Fact]
        public async void ExpectedPostMedicationRequestReturnsBadRequestWhenMedicationRequestObjectIsNull()
        {
            // Arrange
           
            var mockDBContext = new Mock<MedicationRequestDBContext>();
            var mockMedicationRequestSet = new Mock<DbSet<MedicationRequest>>();
            var mockLogger = new Mock<ILogger<MedicationRequestController>>();

            mockDBContext.Setup(x => x.MedicationRequest).Returns(mockMedicationRequestSet.Object);

            //Act
            MedicationRequestController medicationRequest = new MedicationRequestController(mockDBContext.Object, mockLogger.Object);
            HttpResponseMessage response = await medicationRequest.PostMedicationRequest(null);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            mockDBContext.Verify(x => x.MedicationRequest.Add(It.IsAny<MedicationRequest>()), Times.Never());
            //mockDBContext.Verify(x => x.SaveChangesAsync(), Times.Never());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("  ")]
        public async void ExpectedPostMedicationRequestReturnsBadRequestWhenPatientReferenceIsNullOrEmpty(string? patientReference)
        {
            // Arrange

            MedicationRequest medRequest = new MedicationRequest()
            {
                PatientReference = patientReference,
                ClinicianReference = "67567",
                MedicationReference = "23233",
                ReasonText = "Post request test 2",
                PrescribedDate = new DateTime(2018, 06, 22).Date.ToString(),
                StartDate = new DateTime(2018, 06, 25).Date.ToString(),
                EndDate = null,
                Frequency = 3,
                Status = RequestStatus.active
            };

            var mockDBContext = new Mock<MedicationRequestDBContext>();
            var mockMedicationRequestSet = new Mock<DbSet<MedicationRequest>>();
            var mockLogger = new Mock<ILogger<MedicationRequestController>>();

            mockDBContext.Setup(x => x.MedicationRequest).Returns(mockMedicationRequestSet.Object);

            //Act
            MedicationRequestController medicationRequest = new MedicationRequestController(mockDBContext.Object, mockLogger.Object);
            HttpResponseMessage response = await medicationRequest.PostMedicationRequest(medRequest);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            mockDBContext.Verify(x => x.MedicationRequest.Add(It.IsAny<MedicationRequest>()), Times.Never());
            //mockDBContext.Verify(x => x.SaveChangesAsync(), Times.Never());
        }

        [Fact]
        public async void ExpectedPostTheMedicationRequestReturnsInternalServerErrorWhenExceptionIsReaised()
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

            mockDBContext.Setup(x => x.MedicationRequest.Add(medRequest)).Throws(new NullReferenceException("The object provided is null."));

            //Act
            MedicationRequestController medicationRequest = new MedicationRequestController(mockDBContext.Object, mockLogger.Object);
            HttpResponseMessage response = await medicationRequest.PostMedicationRequest(medRequest);

            //Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
            mockDBContext.Verify(x => x.MedicationRequest.Add(It.IsAny<MedicationRequest>()), Times.Once());
            //mockDBContext.Verify(x => x.SaveChangesAsync(), Times.Once());
        }
    }
}