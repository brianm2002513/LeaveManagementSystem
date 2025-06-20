﻿using LeaveManagementSystem.Application.Models.LeaveRequests;
using LeaveManagementSystem.Application.Services.LeaveRequestService;
using LeaveManagementSystem.Application.Services.LeaveTypeService;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LeaveManagementSystem.Web.Controllers
{
    [Authorize]
    public class LeaveRequestsController(ILeaveTypesService _leaveTypesService, ILeaveRequestService _leaveRequestService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var model = await _leaveRequestService.GetEmployeeLeaveRequests();
            return View(model);
        }

        public async Task<IActionResult> Create(int? leaveTypeId)
        {
            var  leaveTypes = await _leaveTypesService.GetAllLeaveTypesAsync();
            var leaveTypeList = new SelectList(leaveTypes, "Id", "Name", leaveTypeId);
            var model = new LeaveRequestCreateViewModel
            {
                StartDate = DateOnly.FromDateTime(DateTime.Now),
                EndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                LeaveTypes = leaveTypeList,
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveRequestCreateViewModel model)
        {
            // Validate the the days don't exceed the allocation 
            if(await _leaveRequestService.RequestDatesExceedAllocation(model))
            {
                ModelState.AddModelError(string.Empty, "The requested dates exceed your leave allocation for this leave type.");
            }

            if (ModelState.IsValid)
            {
                await _leaveRequestService.CreateLeaveRequest(model);
                return RedirectToAction(nameof(Index));
            }
            var leaveTypes = await _leaveTypesService.GetAllLeaveTypesAsync();
            model.LeaveTypes = new SelectList(leaveTypes, "Id", "Name");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id)
        {
            await _leaveRequestService.CancelLeaveRequest(id);
            return RedirectToAction(nameof(Index));
        }

        // Admin/Super review leave request
        [Authorize(Policy = "AdminSupervisorOnly")]
        public async Task<IActionResult> ListRequests()
        {
            var model = await _leaveRequestService.AdminGeAllLeaveRequests();
            return View(model);
        }

        public async Task<IActionResult> Review(int id)
        {
            var model = await _leaveRequestService.GetLeaveRequestForReview(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Review(int id, bool approved)
        {
            await _leaveRequestService.ReviewLeaveRequest(id, approved);
            return RedirectToAction(nameof(ListRequests));
        }
    }
}
