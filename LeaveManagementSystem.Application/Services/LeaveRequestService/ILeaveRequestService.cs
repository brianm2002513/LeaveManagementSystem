using LeaveManagementSystem.Application.Models.LeaveRequests;

namespace LeaveManagementSystem.Application.Services.LeaveRequestService
{
    public interface ILeaveRequestService
    {
        Task CreateLeaveRequest(LeaveRequestCreateViewModel model);
        Task<List<LeaveRequestReadOnlyViewModel>> GetEmployeeLeaveRequests();
        Task<EmployeeLeaveRequestListViewModel> AdminGeAllLeaveRequests();
        Task CancelLeaveRequest(int leaveRequestId);
        Task ReviewLeaveRequest(int leaveRequestId, bool approved);
        Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateViewModel model);
        Task<ReviewLeaveRequestViewModel> GetLeaveRequestForReview(int id);
    }
}