using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cerebri.Domain.Entities
{
    public class CheckInResponseModel
    {
        [Required]
        public Guid Id { get; set; }
        public string? Content { get; set; }
        public List<MoodModel>? Moods { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
