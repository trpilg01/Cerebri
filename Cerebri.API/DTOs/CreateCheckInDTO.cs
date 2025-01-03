using Cerebri.Domain.Entities;

namespace Cerebri.API.DTOs
{
    public class CreateCheckInDTO
    {
        public string Content { get; set; } = string.Empty;
        public List<MoodModel> Moods { get; set; } = new List<MoodModel>();
    }
}
