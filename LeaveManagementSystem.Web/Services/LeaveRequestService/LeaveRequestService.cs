using AutoMapper;
using LeaveManagementSystem.Web.Models.LeaveRequests;
using LeaveManagementSystem.Web.Services.LeaveAllocationService;
using LeaveManagementSystem.Web.Services.UserService;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services.LeaveRequestService
{
    public class LeaveRequestService(IMapper _mapper, ApplicationDbContext _context, ILeaveAllocationsService _leaveAllocationsService, IUserService _userService) : ILeaveRequestService
    {
        public async Task CancelLeaveRequest(int leaveRequestId)
        {
           var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
           leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Cancelled;

            // restore allocation days based on request
            await UpdateAllocationDays(leaveRequest, false);
            await _context.SaveChangesAsync();
        }

        public async Task CreateLeaveRequest(LeaveRequestCreateViewModel model)
        {
            // map data to leave request data model
            var leaveRequest = _mapper.Map<LeaveRequest>(model);

            // get logged in employee id
            var user = await _userService.GetLoggedInUser();
            leaveRequest.EmployeeId = user.Id;

            // set the LeaveRequestStatusId to pending
            leaveRequest.LeaveRequestStatusId = (int)LeaveRequestStatusEnum.Pending;

            // save leave request to database
            _context.LeaveRequests.Add(leaveRequest);

            // deduct allocation days based on request
            await UpdateAllocationDays(leaveRequest, true);

            await _context.SaveChangesAsync();
        }   

        public async Task<EmployeeLeaveRequestListViewModel> AdminGeAllLeaveRequests()
        {
            var leaveRequests = await _context.LeaveRequests
                .Include(q => q.LeaveType)
                .ToListAsync();

            var leaveRequestModels = leaveRequests.Select(q => new LeaveRequestReadOnlyViewModel
            {
                StartDate = q.StartDate,
                EndDate = q.EndDate,
                Id = q.Id,
                LeaveType = q.LeaveType.Name,
                LeaveRequestStatus = (LeaveRequestStatusEnum)q.LeaveRequestStatusId,
                NumberOfDays = (q.EndDate.DayNumber - q.StartDate.DayNumber)
            }).ToList();

            var model = new EmployeeLeaveRequestListViewModel
            {
                ApprovedRequests = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Approved),
                PendingRequests = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Pending),
                DeclinedRequests = leaveRequests.Count(q => q.LeaveRequestStatusId == (int)LeaveRequestStatusEnum.Declined),
                TotalRequests = leaveRequests.Count,
                LeaveRequests = leaveRequestModels
            };

            return model;
        }

        public async Task<List<LeaveRequestReadOnlyViewModel>> GetEmployeeLeaveRequests()
        {
            var user = await _userService.GetLoggedInUser();
            var leaveRequests = await _context.LeaveRequests
                .Include(q => q.LeaveType)
                .Where(q => q.EmployeeId == user.Id)
                .ToListAsync();
            var model = leaveRequests.Select(q => new LeaveRequestReadOnlyViewModel
            {
                StartDate = q.StartDate,
                EndDate = q.EndDate,
                Id = q.Id,
                LeaveType = q.LeaveType.Name,
                LeaveRequestStatus = (LeaveRequestStatusEnum)q.LeaveRequestStatusId,
                NumberOfDays = (q.EndDate.DayNumber - q.StartDate.DayNumber)
            }).ToList();

            return model;
        }

        public async Task<bool> RequestDatesExceedAllocation(LeaveRequestCreateViewModel model)
        {
            var user = await _userService.GetLoggedInUser();
            var currentDate = DateTime.Now;
            var period = await _context.Periods.SingleAsync(q => q.EndDate.Year == currentDate.Year);
            var NumberOfDays = (model.EndDate.DayNumber - model.StartDate.DayNumber);
            var allocation = await _context.LeaveAllocations
                .FirstAsync(a => a.EmployeeId == user.Id 
                && a.LeaveTypeId == model.LeaveTypeId
                && a.PeriodId == period.Id);

            return allocation.NumberOfDays < NumberOfDays;
        }

        public async Task<ReviewLeaveRequestViewModel> GetLeaveRequestForReview(int id)
        {
            var leaveRequest = await _context.LeaveRequests
                .Include(q => q.LeaveType)
                .FirstAsync(q => q.Id == id);
            var user = await _userService.GetUserById(leaveRequest.EmployeeId);

            var model = new ReviewLeaveRequestViewModel
            {
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                NumberOfDays = (leaveRequest.EndDate.DayNumber - leaveRequest.StartDate.DayNumber),
                Id = leaveRequest.Id,
                LeaveType = leaveRequest.LeaveType.Name,
                RequestComments = leaveRequest.RequestComments,
                Employee = new EmployeeListViewModel
                {
                    Id = leaveRequest.EmployeeId,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                }
            };

            return model;
        }

        public async Task ReviewLeaveRequest(int leaveRequestId, bool approved)
        {
            var user = await _userService.GetLoggedInUser();
            var leaveRequest = await _context.LeaveRequests.FindAsync(leaveRequestId);
            leaveRequest.LeaveRequestStatusId = approved 
                ? (int)LeaveRequestStatusEnum.Approved 
                : (int)LeaveRequestStatusEnum.Declined;

            leaveRequest.ReviewerId = user.Id;

            if (!approved) {
                await UpdateAllocationDays(leaveRequest, false);
            }

            await _context.SaveChangesAsync();
        }

        private async Task UpdateAllocationDays(LeaveRequest leaveRequest, bool deductDays)
        {
            var allocation = await _leaveAllocationsService.GetCurrentAllocation(leaveRequest.LeaveTypeId, leaveRequest.EmployeeId);
            var numberOfDays = CalculateDays(leaveRequest.StartDate, leaveRequest.EndDate);

            if (deductDays)
            {
                allocation.NumberOfDays -= numberOfDays;
            }
            else
            {
                allocation.NumberOfDays += numberOfDays;
            }
            _context.Entry(allocation).State = EntityState.Modified;
        }

        private int CalculateDays(DateOnly start, DateOnly end)
        {
            return (end.DayNumber - start.DayNumber);//+ 1;
        }
    }
}
