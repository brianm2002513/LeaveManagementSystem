using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Web.Models.LeaveTypes
{
    public class LeaveTypeReadOnlyViewModel : BaseModel
    {
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Maximum allocation of Days")]
        public int NumberOfDays { get; set; }
    }
}
