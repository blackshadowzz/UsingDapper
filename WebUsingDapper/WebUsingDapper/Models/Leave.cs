using System.ComponentModel.DataAnnotations.Schema;

namespace WebUsingDapper.Models
{
    public class Leave
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime? LeaveDate { get; set; }
        public decimal? Amount { get; set; }
        public ReasonType? ReasonType { get; set; }
        public string? Description { get; set; }

    }
    public enum ReasonType
    {
        Sick,
        Urgent,
        Anual,
        Other
    }
}
