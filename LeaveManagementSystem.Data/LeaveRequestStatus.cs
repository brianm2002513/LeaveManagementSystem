using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Data
{
    public class LeaveRequestStatus: BaseEntity
    {
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}