using AutoMapper;
using LeaveManagementSystem.Application.Models.LeaveAllocations;
using LeaveManagementSystem.Application.Models.LeaveRequests;
using LeaveManagementSystem.Application.Models.LeaveTypes;

namespace LeaveManagementSystem.Application.MappingProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<LeaveType, LeaveTypeReadOnlyViewModel>();
            //.ForMember(dest => dest.Days, opt => opt.MapFrom(src => src.NumberOfDays));
            CreateMap<LeaveTypeCreateViewModel, LeaveType>();
            CreateMap<LeaveTypeEditViewModel, LeaveType>().ReverseMap();
            CreateMap<Period, PeriodReadOnlyViewModel>();
            CreateMap<PeriodCreateViewModel, Period>();
            CreateMap<PeriodEditViewModel, Period>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationViewModel>();
            CreateMap<ApplicationUser, EmployeeListViewModel>();
            CreateMap<LeaveAllocation, LeaveAllocationEditViewModel>();
            CreateMap<LeaveRequestCreateViewModel, LeaveRequest>();
        }
    }
}