using LeaveManagementSystem.Web.Models.LeaveTypes;

namespace LeaveManagementSystem.Web.Services
{
    public interface ILeaveTypesService
    {
        Task<bool> CheckIfLeaveTypeNameExistsAsync(string name);
        Task<bool> CheckIfLeaveTypeNameExistsForEditAsync(LeaveTypeEditViewModel leaveTypeEdit);
        Task CreateLeaveType(LeaveTypeCreateViewModel model);
        Task EditLeaveType(LeaveTypeEditViewModel model);
        Task<List<LeaveTypeReadOnlyViewModel>> GetAllLeaveTypesAsync();
        Task<T?> GetLeaveTypeById<T>(int id) where T : class;
        bool LeaveTypeExists(int id);
        Task RemoveLeaveType(int id);
    }
}