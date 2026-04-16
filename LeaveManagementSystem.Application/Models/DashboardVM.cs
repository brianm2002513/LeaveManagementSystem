using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Application.Models
{
    public class DashboardVM
    {
        [Display(Name = "Total Requests")]
        public int TotalRequests { get; set; }

        [Display(Name = "Approved Requests")]
        public int ApprovedRequests { get; set; }

        [Display(Name = "Pending Requests")]
        public int PendingRequests { get; set; }

        [Display(Name = "Rejected Requests")]
        public int DeclinedRequests { get; set; }
    }
}
