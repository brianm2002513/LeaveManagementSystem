namespace LeaveManagementSystem.Web.Models.LeaveRequests
{
    // View model for a particular employee to view their leave requests
    public class EmployeeLeaveRequestListViewModel
    {
        [Display(Name = "Total Requests")]
        public int TotalRequests { get; set; }

        [Display(Name = "Approved Requests")]
        public int ApprovedRequests { get; set; }

        [Display(Name = "Pending Requests")]
        public int PendingRequests { get; set; }

        [Display(Name = "Rejected Requests")]
        public int DeclinedRequests { get; set; }

        public List<LeaveRequestReadOnlyViewModel> LeaveRequests { get; set; } = [];
    }
}