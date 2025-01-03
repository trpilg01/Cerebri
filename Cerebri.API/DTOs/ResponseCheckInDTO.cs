using Cerebri.Domain.Entities;

namespace Cerebri.API.DTOs
{
    public class ResponseCheckInDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public List<MoodModel> Moods { get; set; } = new List<MoodModel>();
        public DateTime CreatedAt { get; set; }
    }
}
