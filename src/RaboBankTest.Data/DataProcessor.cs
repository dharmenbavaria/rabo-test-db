using System.Data.SqlClient;

using Dapper;

using Microsoft.Extensions.Configuration;

using RaboBankTest.Application.Contracts;
using RaboBankTest.Domain.Data;

namespace RaboBankTest.Data;

public class DataProcessor : IDataProcessor
{
    private readonly string _connectionString;

    public DataProcessor(
        IConfiguration configuration)
    {
        _connectionString = configuration["SqlConnectionString"];
    }

    public async Task<IList<CustomDataModel>> ProcessDataAsync(
        DateTime lastExecutionTime,
        CancellationToken cancellationToken)
    {
        await using var connection = new SqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        var query = "EXEC GetCustomModelData @LastExecutionTime";

        var parameters = new
            {
                LastExecutionTime = lastExecutionTime
            };

        var results = await connection.QueryAsync<CustomDataModel>(
            query,
            parameters);

        return results.ToList();
    }
}
