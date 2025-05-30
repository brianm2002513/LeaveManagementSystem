

namespace LeaveManagementSystem.Web.Services.UserService
{
    public interface IUserService
    {
        Task<List<ApplicationUser>> GetEmployees();
        Task<ApplicationUser> GetLoggedInUser();
        Task<ApplicationUser> GetUserById(string userId);
    }
}