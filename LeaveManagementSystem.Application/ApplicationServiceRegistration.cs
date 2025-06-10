using LeaveManagementSystem.Application.Services.Email;
using LeaveManagementSystem.Application.Services.LeaveAllocationService;
using LeaveManagementSystem.Application.Services.LeaveRequestService;
using LeaveManagementSystem.Application.Services.LeaveTypeService;
using LeaveManagementSystem.Application.Services.PeriodService;
using LeaveManagementSystem.Application.Services.UserService;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Add AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ILeaveTypesService, LeaveTypesService>();
            services.AddScoped<IPeriodsService, PeriodsService>();
            services.AddScoped<ILeaveAllocationsService, LeaveAllocationsService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ILeaveRequestService, LeaveRequestService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
