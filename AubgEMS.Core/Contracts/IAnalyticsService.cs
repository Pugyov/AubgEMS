using System.Threading;
using System.Threading.Tasks;
using AubgEMS.Core.Models.Admin;

namespace AubgEMS.Core.Contracts
{
    public interface IAnalyticsService
    {
        Task<DashboardKpisDto> GetKpisAsync(
            System.DateTime fromUtc,
            System.DateTime toUtc,
            int? departmentId,
            int? clubId,
            int? eventTypeId,
            CancellationToken ct = default);
    }
}