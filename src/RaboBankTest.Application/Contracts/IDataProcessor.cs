using RaboBankTest.Domain.Data;

namespace RaboBankTest.Application.Contracts
{
    public interface IDataProcessor
    {
        Task<IList<CustomDataModel>> ProcessDataAsync(
            DateTime lastExecutionTime,
            CancellationToken cancellationToken);
    }
}
