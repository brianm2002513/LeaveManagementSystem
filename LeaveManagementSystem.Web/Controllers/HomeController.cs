using LeaveManagementSystem.Application.Models;
using LeaveManagementSystem.Application.Services.LeaveRequestService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LeaveManagementSystem.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ILeaveRequestService _leaveRequestService;

    public HomeController(ILogger<HomeController> logger, ILeaveRequestService leaveRequestService)
    {
        _logger = logger;
        _leaveRequestService = leaveRequestService;
    }

    public async Task<IActionResult> Index()
    {
        DashboardVM model = new DashboardVM();
        
        if (User.Identity?.IsAuthenticated == true)
        {
            if (User.IsInRole("Administrator"))
            {
                var adminStats = await _leaveRequestService.AdminGeAllLeaveRequests();
                model.TotalRequests = adminStats.TotalRequests;
                model.ApprovedRequests = adminStats.ApprovedRequests;
                model.PendingRequests = adminStats.PendingRequests;
                model.DeclinedRequests = adminStats.DeclinedRequests;
            }
            else
            {
                var userRequests = await _leaveRequestService.GetEmployeeLeaveRequests();
                model.TotalRequests = userRequests.Count;
                model.ApprovedRequests = userRequests.Count(q => q.LeaveRequestStatus == LeaveRequestStatusEnum.Approved);
                model.PendingRequests = userRequests.Count(q => q.LeaveRequestStatus == LeaveRequestStatusEnum.Pending);
                model.DeclinedRequests = userRequests.Count(q => q.LeaveRequestStatus == LeaveRequestStatusEnum.Declined);
            }
        }
        
        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        var model = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        };
        return View(model);
    }
}
