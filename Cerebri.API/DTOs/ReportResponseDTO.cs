namespace Cerebri.API.DTOs
{
    public class ReportResponseDTO
    {
        public Guid Id { get; set; }
        public string ReportName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
