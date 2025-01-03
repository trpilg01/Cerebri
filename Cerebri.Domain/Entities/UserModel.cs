using System.ComponentModel.DataAnnotations;

namespace Cerebri.Domain.Entities
{
    public class UserModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(128, ErrorMessage = "Email cannot exceed 128 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Password cannot exceed 255 characters")]
        public string HashedPassword { get; set; }

        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public string FirstName { get; set; }
        public List<JournalEntryModel> JournalEntries { get; set; }
        public List<ReportModel> Reports { get; set; }
        public List<CheckInModel> CheckIns { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public UserModel() { }
        public UserModel(string email, string hashedPassword)
        {
            Id = Guid.NewGuid();
            Email = email;
            HashedPassword = hashedPassword;
            FirstName = string.Empty;
            JournalEntries = new List<JournalEntryModel>();
            CheckIns = new List<CheckInModel>();
            UpdatedAt = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
        }

        public UserModel(string email, string hashedPassword, string firstName)
        {
            Id = Guid.NewGuid();
            Email = email;
            HashedPassword = hashedPassword;
            FirstName = firstName;
            JournalEntries = new List<JournalEntryModel>();
            UpdatedAt= DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
