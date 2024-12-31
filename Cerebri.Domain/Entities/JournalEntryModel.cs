using Cerebri.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cerebri.Domain.Entities
{
    public class JournalEntryModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A UserId is required")]
        public Guid UserId { get; set; }

        [JsonIgnore]
        public UserModel? User { get; set; }

        [Required(ErrorMessage = "A Title is required")]
        [StringLength(100, ErrorMessage = "Title cannot exceed length of 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Journal content is required")]
        [StringLength(1000, ErrorMessage = "Content length cannot exceed 1000 characters")]
        public string Content { get; set; }
        public ICollection<JournalEntryMoodModel> MoodTags { get; set; } = new List<JournalEntryMoodModel>();
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public JournalEntryModel() { }

        public JournalEntryModel(Guid userId, string title, string content)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Title = title;
            Content = content;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public JournalEntryModel(Guid userId, string title, string content, List<JournalEntryMoodModel> moodTags)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Title = title;
            Content = content;
            MoodTags = moodTags;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
