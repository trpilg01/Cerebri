using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cerebri.Domain.Entities
{
    public class CheckInModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "A UserId is required")]
        public Guid UserId { get; set; }

        [JsonIgnore]
        public UserModel? User { get; set; }

        [StringLength(1000, ErrorMessage = "Content length cannot exceed 1000 characters")]
        public string? Content { get; set; }

        [MaxLength(2, ErrorMessage = "Exceeds maximum of 2 moods attached to a check-in")]
        public ICollection<CheckInMoodModel> MoodTags { get; set; } = new List<CheckInMoodModel>();

        [Required]
        public DateTime UpdatedAt { get; set; }
        
        [Required]
        public DateTime CreatedAt { get; set; }

    }
}
