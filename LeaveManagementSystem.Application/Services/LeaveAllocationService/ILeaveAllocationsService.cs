﻿using LeaveManagementSystem.Application.Models.LeaveAllocations;

namespace LeaveManagementSystem.Application.Services.LeaveAllocationService
{
    public interface ILeaveAllocationsService
    {
        Task AllocateLeave(string employeeId);
        Task<EmployeeAllocationViewModel> GetEmployeeAllocations(string? userId);
        Task<LeaveAllocationEditViewModel> GetEmployeeAllocation(int allocationId);
        Task<List<EmployeeListViewModel>> GetEmployees();
        Task EditAllocation(LeaveAllocationEditViewModel allocationEditViewModel);
        Task<LeaveAllocation> GetCurrentAllocation(int leaveTypeId, string employeeId);
    }
}
 