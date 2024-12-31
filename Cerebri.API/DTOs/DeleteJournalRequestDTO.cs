using System.ComponentModel.DataAnnotations;

namespace Cerebri.API.DTOs
{
    public class DeleteJournalRequestDTO
    {
        [Required]
        public Guid EntryId { get; set; }
    }
}
