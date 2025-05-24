using AutoMapper;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveAllocations;
using LeaveManagementSystem.Web.Models.LeaveTypes;
using LeaveManagementSystem.Web.Models.Periods;

namespace LeaveManagementSystem.Web.MappingProfiles
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
                
        }
    }
}