using Cerebri.Domain.Entities;
using Cerebri.Domain.Enums;

namespace Cerebri.API.DTOs
{
    public class UpdateJournalEntryDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public List<MoodModel> Moods { get; set; } = new List<MoodModel>();
    }
}
