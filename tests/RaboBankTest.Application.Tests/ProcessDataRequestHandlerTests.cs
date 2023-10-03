using AutoMapper;

using Moq;

using RaboBankTest.Application.Contracts;
using RaboBankTest.Application.Feature;
using RaboBankTest.Domain.Data;
using RaboBankTest.Domain.Message;

namespace RaboBankTest.Application.Tests;

public class ProcessDataRequestHandlerTests
{
    [Fact]
    public async Task Handle_ValidRequest_ProcessesDataAndSendsMessage()
    {
        var cancellationToken = CancellationToken.None;

        var request = new ProcessDataRequest
            {
                ProcessDateTime = DateTime.UtcNow
            };

        var dataProcessorMock = new Mock<IDataProcessor>();
        var messageSenderMock = new Mock<IMessageSender>();
        var mapperMock = new Mock<IMapper>();
        var recordId = Guid.NewGuid();
        var userId = Guid.NewGuid();

        var mockData = new List<CustomDataModel>
            {
                new CustomDataModel()
                    {
                        DataValue = "Temp",
                        NotificationFlag = true,
                        RecordId = recordId,
                        UserEmail = "abc@temp.com",
                        UserId = userId.ToString(),
                        UserName = "dharmen.bavaria"
                    }
            };

        dataProcessorMock
            .Setup(
                dp => dp.ProcessDataAsync(
                    request.ProcessDateTime,
                    cancellationToken))
            .ReturnsAsync(mockData);

        // Set up mapperMock to return a CustomModelNotification
        var mockNotification = new CustomModelNotification
            {
                RecordId = recordId,
                UserEmail = "abc@temp.com",
                UserId = userId.ToString(),
                UserName = "dharmen.bavaria",
                DataValue = "Temp"
            };

        mapperMock
            .Setup(m => m.Map<CustomModelNotification>(It.IsAny<CustomDataModel>()))
            .Returns(mockNotification);

        var handler = new ProcessDataRequestHandler(
            dataProcessorMock.Object,
            messageSenderMock.Object,
            mapperMock.Object);

        // Act
        await handler.Handle(
            request,
            cancellationToken);

        // Assert
        // Verify that ProcessDataAsync was called with the correct parameters
        dataProcessorMock.Verify(
            dp => dp.ProcessDataAsync(
                request.ProcessDateTime,
                cancellationToken),
            Times.Once);

        // Verify that Map method was called with the correct data and CustomModelNotification was sent
        mapperMock.Verify(
            m => m.Map<CustomModelNotification>(It.Is<CustomDataModel>(p => p.RecordId == recordId && p.UserId == userId.ToString() && p.UserEmail == "abc@temp.com" && p.UserName == "dharmen.bavaria")),
            Times.Exactly(mockData.Count));

        // Verify that Publish method was called with the CustomModelNotification
        messageSenderMock.Verify(
            ms => ms.Publish(
                mockNotification,
                cancellationToken),
            Times.Exactly(mockData.Count));
    }
}
