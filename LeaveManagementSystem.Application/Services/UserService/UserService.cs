﻿using Microsoft.AspNetCore.Http;

namespace LeaveManagementSystem.Application.Services.UserService
{
    public class UserService(IHttpContextAccessor _httpContextAccessor, UserManager<ApplicationUser> _userManager) : IUserService
    {
        public async Task<ApplicationUser> GetLoggedInUser()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext?.User);
            return user;
        }
        public async Task<ApplicationUser> GetUserById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user;
        }

        public async Task<List<ApplicationUser>> GetEmployees()
        {
            var employees = await _userManager.GetUsersInRoleAsync(Roles.Employee);
            return employees.ToList();
        }
    }
}
