﻿using LeaveManagementSystem.Application.Models.LeaveTypes;

namespace LeaveManagementSystem.Application.Services.LeaveTypeService
{
    public interface ILeaveTypesService
    {
        Task<bool> CheckIfLeaveTypeNameExistsAsync(string name);
        Task<bool> CheckIfLeaveTypeNameExistsForEditAsync(LeaveTypeEditViewModel leaveTypeEdit);
        Task CreateLeaveType(LeaveTypeCreateViewModel model);
        Task<bool> DaysExceedMaximum(int leaveTypeId, int days);
        Task EditLeaveType(LeaveTypeEditViewModel model);
        Task<List<LeaveTypeReadOnlyViewModel>> GetAllLeaveTypesAsync();
        Task<T?> GetLeaveTypeById<T>(int id) where T : class;
        bool LeaveTypeExists(int id);
        Task RemoveLeaveType(int id);
    }
}