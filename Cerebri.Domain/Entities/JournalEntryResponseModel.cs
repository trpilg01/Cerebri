using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerebri.Domain.Entities
{
    public class JournalEntryResponseModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }        
        public string Content { get; set; }
        public List<MoodModel> Moods { get; set; }
        public DateTime CreatedAt { get; set; }
        public JournalEntryResponseModel()
        {
            Id = Guid.Empty;
            Title = string.Empty;
            Content = string.Empty;
            Moods = new List<MoodModel>();
            CreatedAt = DateTime.MinValue;
        }
    }
}
