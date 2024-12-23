using Cerebri.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cerebri.Domain.Entities
{
    public class JournalEntryMoodModel
    {
        public Guid? JournalEntryId { get; set; }

        [JsonIgnore]
        public JournalEntryModel? JournalEntry { get; set; }
        public eMoods MoodId { get; set; }
        public JournalEntryMoodModel() { }

        public JournalEntryMoodModel(eMoods mood)
        {
            MoodId = mood;
        }
    }
}