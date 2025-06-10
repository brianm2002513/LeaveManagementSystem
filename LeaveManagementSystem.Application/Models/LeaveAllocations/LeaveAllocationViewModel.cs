using LeaveManagementSystem.Application.Models.LeaveTypes;

namespace LeaveManagementSystem.Application.Models.LeaveAllocations
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
