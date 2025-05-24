
using AutoMapper;
using LeaveManagementSystem.Web.Models.LeaveAllocations;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace LeaveManagementSystem.Web.Services.LeaveAllocationService
{
    public class LeaveAllocationsService(ApplicationDbContext _context, IHttpContextAccessor _httpContextAccessor, UserManager<ApplicationUser> _userManager, IMapper _mapper) : ILeaveAllocationsService
    {
        public async Task AllocateLeave(string? employeeId)
        {
            // get all the leave types not used for allocation for the employee
            var leaveTypes =  await _context.LeaveTypes
                .Where(q => !q.leaveAllocations.Any(x => x.EmployeeId == employeeId))
                .ToListAsync();

            // get the current period based on the year
            var currentDate = DateTime.Now;
            var period = await _context.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);
            var monthsRemaining = period.EndDate.Month - currentDate.Month;
            // calculate leave based on number of months left in the period
            // foreach leave type, create an allocation entry
            foreach (var leaveType in leaveTypes)
            {
                //Works, but not best practice
                //var allocationExists = await AllocationExists(employeeId, period.Id, leaveType.Id);
                //if (allocationExists)
                //{
                //    continue;
                //}
                var accrualRate = decimal.Divide(leaveType.NumberOfDays, 12);
                var leaveAllocation = new LeaveAllocation
                {
                    EmployeeId = employeeId,
                    LeaveTypeId = leaveType.Id,
                    PeriodId = period.Id,
                    NumberOfDays = (int) Math.Ceiling(accrualRate * monthsRemaining)
                };

                _context.LeaveAllocations.Add(leaveAllocation);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeAllocationViewModel> GetEmployeeAllocations(string? userId)
        {
            var user = string.IsNullOrEmpty(userId) 
                ? await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User) 
                : await _userManager.FindByIdAsync(userId);

            var allocations = await GetAllocations(user.Id);
            var allocationsViewModelList = _mapper.Map<List<LeaveAllocation>, List<LeaveAllocationViewModel>>(allocations);
            var leaveTypesCount = await _context.LeaveTypes.CountAsync();

            var employeeAllocationViewModel = new EmployeeAllocationViewModel
            {
                DateOfBirth = user.DateOfBirth,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                LeaveAllocations = allocationsViewModelList,
                isCompletedAllocation = leaveTypesCount == allocations.Count
            };

            return employeeAllocationViewModel;
        }

        public async Task<List<EmployeeListViewModel>> GetEmployees()
        {
            var users = await _userManager.GetUsersInRoleAsync(Roles.Employee);
            var employees = _mapper.Map<List<ApplicationUser>, List<EmployeeListViewModel>>([.. users]);

            return employees;
        }

        public async Task<LeaveAllocationEditViewModel> GetEmployeeAllocation(int allocationId)
        {
            var allocation = await _context.LeaveAllocations
                .Include(q => q.LeaveType)
                .Include(q => q.Employee)
                .FirstOrDefaultAsync(q => q.Id == allocationId);

            var model = _mapper.Map<LeaveAllocationEditViewModel>(allocation);

            return model;
        }

        public async Task EditAllocation(LeaveAllocationEditViewModel allocationEditViewModel)
        {
            //var leaveAllocation = await GetEmployeeAllocation(allocationEditViewModel.Id);
            //if (leaveAllocation == null)
            //{
            //    throw new Exception($"Leave allocation with ID {allocationEditViewModel.Id} not found.");
            //}
            //leaveAllocation.NumberOfDays = allocationEditViewModel.NumberOfDays;
            //option1 _context.Update(leaveAllocation);
            //option2 _context.Entry(leaveAllocation).State = EntityState.Modified;
            //await _context.SaveChangesAsync();

            await _context.LeaveAllocations
                .Where(q => q.Id == allocationEditViewModel.Id)
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.NumberOfDays, allocationEditViewModel.NumberOfDays));
        }

        private async Task<List<LeaveAllocation>> GetAllocations(string? userId)
        {
            var currentDate = DateTime.Now;
            var leaveAllocations = await _context.LeaveAllocations
                .Include(q => q.LeaveType)
                .Include(q => q.Period)
                .Where(q => q.EmployeeId == userId && q.Period.EndDate.Year == currentDate.Year)
                .ToListAsync();

            return leaveAllocations;
        }

        //private async Task<bool> AllocationExists(string userId, int periodId, int leaveTypeId)
        //{
        //    var exists = await _context.LeaveAllocations.AnyAsync(q =>
        //        q.EmployeeId == userId &&
        //        q.PeriodId == periodId &&
        //        q.LeaveTypeId == leaveTypeId
        //    );

        //    return exists;
        //}
    } 
}
