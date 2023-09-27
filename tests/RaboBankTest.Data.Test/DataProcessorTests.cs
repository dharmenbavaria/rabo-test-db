using System.Data;
using System.Data.SqlClient;

using Dapper;

using Microsoft.Extensions.Configuration;

using Moq;

using RaboBankTest.Domain.Data;

namespace RaboBankTest.Data.Test
{
    public class DataProcessorTests
    {
        [Fact]
        public async Task ProcessDataAsync_ValidInput_ReturnsExpectedData()
        {
            // Arrange
            var configurationMock = new Mock<IConfiguration>();

            configurationMock.Setup(config => config["SqlConnectionString"])
                .Returns("YourConnectionString");

            // Mock SqlConnection and Dapper QueryAsync method
            var connectionMock = new Mock<IDbConnection>();

            var dataProcessor = new DataProcessor(configurationMock.Object);

            var lastExecutionTime = DateTime.UtcNow;
            var cancellationToken = CancellationToken.None;

            var dapperResults = new List<CustomDataModel>
                {
                    new CustomDataModel()
                        {
                            DataValue = "Temp",
                            NotificationFlag = true,
                            RecordId = 1,
                            UserEmail = "abc@temp.com",
                            UserId = 1,
                            UserName = "dharmen.bavaria"
                        }
                };

            connectionMock.Setup(conn => conn.Open()).Verifiable();

            connectionMock.Setup(
                    conn => conn.QueryAsync<CustomDataModel>(
                        It.Is<string>(p => p == "EXEC YourStoredProcedure @LastExecutionTime"),
                        It.IsAny<object>(),
                        null,
                        null,
                        CommandType.Text))
                .ReturnsAsync(dapperResults);

            // Act
            var result = await dataProcessor.ProcessDataAsync(
                lastExecutionTime,
                cancellationToken);

            // Assert
            Assert.NotNull(result);

            Assert.Equivalent(
                dapperResults,
                result);
        }
    }
}
