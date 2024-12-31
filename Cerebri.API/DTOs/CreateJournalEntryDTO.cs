using Cerebri.Domain.Entities;
using Cerebri.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Cerebri.API.DTOs
{
    public class CreateJournalEntryDTO
    {
        [Required]
        [StringLength(128, MinimumLength = 1, ErrorMessage = "Journal Title does not meet length requirements")]
        public string Title { get; set; } = string.Empty;
        
        [Required]
        [StringLength(1000, MinimumLength = 1, ErrorMessage ="Journal Entry does not meet length requirements")]
        public string Content {  get; set; } = string.Empty;
        public List<MoodModel> Moods { get; set; } = new List<MoodModel>();
    }
}