
using Cerebri.Domain.Entities;

namespace Cerebri.Application.Interfaces
{
    public interface ICheckInService
    {
        Task CreateCheckInAsync(CheckInModel checkIn);
        Task <IEnumerable<CheckInResponseModel?>> GetCheckInByUserIdAsync(Guid userId);
        Task DeleteCheckInAsync(Guid checkInId);
    }
}
