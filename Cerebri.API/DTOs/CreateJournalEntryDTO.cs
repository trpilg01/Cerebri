using Cerebri.Domain.Entities;
using Cerebri.Domain.Enums;

namespace Cerebri.API.DTOs
{
    public class CreateJournalEntryDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Content {  get; set; } = string.Empty;
        public List<eMoods> Moods { get; set; } = new List<eMoods>();
    }
}