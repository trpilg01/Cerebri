
using Cerebri.Domain.Entities;

namespace Cerebri.Application.Interfaces
{
    public interface ICheckInService
    {
        Task CreateCheckIn(CheckInModel checkIn);
        Task <IEnumerable<CheckInResponseModel?>> GetChecksInByUserId(Guid userId);
        Task DeleteCheckIn(Guid checkInId);
    }
}
