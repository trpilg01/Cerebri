using Cerebri.Domain.Entities;

namespace Cerebri.Application.Interfaces
{
    public interface IMoodRepository
    {
        Task<IEnumerable<MoodModel>> GetMoods();
    }
}
