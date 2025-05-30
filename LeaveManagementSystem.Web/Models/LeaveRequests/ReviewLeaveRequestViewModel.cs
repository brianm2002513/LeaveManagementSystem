using System.ComponentModel;

namespace LeaveManagementSystem.Web.Models.LeaveRequests
{
    public class ReviewLeaveRequestViewModel : LeaveRequestReadOnlyViewModel
    {
        public EmployeeListViewModel Employee { get; set; } = new EmployeeListViewModel();

        [DisplayName("Information")]
        public string? RequestComments { get; set; }

    }
}