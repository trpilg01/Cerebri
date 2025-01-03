using System.Text.Json.Serialization;


namespace Cerebri.Domain.Entities
{
    public class ReportModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        
        [JsonIgnore]
        public UserModel User { get; set; }
        public string ReportName { get; set; } = string.Empty;
        public byte[] ReportData { get; set; } = Array.Empty<byte>();
        public DateTime CreatedAt { get; set; }
        public ReportModel() { }
    }
}