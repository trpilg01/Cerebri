using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cerebri.Domain.Entities
{
    public class CheckInMoodModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid CheckInId { get; set; }
        
        [JsonIgnore]
        public CheckInModel? CheckIn { get; set; }

        [Required]
        public int MoodId { get; set; }

        [JsonIgnore]
        public MoodModel? Mood { get; set; }

        public CheckInMoodModel() { }

        public CheckInMoodModel(Guid checkInId, int moodId)
        {
            Id = Guid.NewGuid();
            CheckInId = checkInId;
            MoodId = moodId;
        }

        public CheckInMoodModel(int moodId)
        {
            Id = Guid.NewGuid();
            MoodId = moodId;
        }
    }
}
