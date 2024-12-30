using Cerebri.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace Cerebri.Domain.Entities
{
    public class JournalEntryMoodModel
    {
        public Guid Id { get; set; }
        public Guid JournalEntryId { get; set; }

        [JsonIgnore]
        public JournalEntryModel? JournalEntry { get; set; }

        [Required]
        [Range(1, 102)]
        public int MoodId { get; set; }
        
        [JsonIgnore]
        public MoodModel? Mood { get; set; }

        public JournalEntryMoodModel() { }

        public JournalEntryMoodModel(int moodId)
        {
            Id = Guid.NewGuid();
            MoodId = moodId;
        }

        public JournalEntryMoodModel(Guid journalId, int moodId)
        {
            Id = Guid.NewGuid();
            JournalEntryId = journalId;
            MoodId = moodId;
        }
    }
}