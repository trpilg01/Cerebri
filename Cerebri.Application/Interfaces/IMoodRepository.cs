using Cerebri.Domain.Entities;

namespace Cerebri.Application.Interfaces
{
    public interface IMoodRepository
    {
        Task<IEnumerable<MoodModel>> GetMoods();
        Task<List<MoodModel>> GetMoodsByIdAsync(List<int> ids);
    }
}
