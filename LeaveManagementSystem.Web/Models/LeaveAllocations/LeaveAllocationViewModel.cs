using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveTypes;

namespace LeaveManagementSystem.Web.Models.LeaveAllocations
{
    public class LeaveAllocationViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Number Of Days")]
        public int NumberOfDays { get; set; }

        [Display(Name = "Allocation Period")]
        public PeriodReadOnlyViewModel Period { get; set; } = new PeriodReadOnlyViewModel();

        public LeaveTypeReadOnlyViewModel LeaveType { get; set; } = new LeaveTypeReadOnlyViewModel();
    }
}
