using Cerebri.Domain.Entities;

namespace Cerebri.API.DTOs
{
    public class JournalsResponseDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<JournalEntryMoodModel> Moods { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
