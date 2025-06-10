using LeaveManagementSystem.Application.Models.LeaveAllocations;
using LeaveManagementSystem.Application.Services.LeaveAllocationService;
using LeaveManagementSystem.Application.Services.LeaveTypeService;

namespace LeaveManagementSystem.Web.Controllers
{
    [Authorize]
    public class LeaveAllocationController(ILeaveAllocationsService _leaveAllocationsService, ILeaveTypesService _leaveTypesService) : Controller
    {
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Index()
        {
            var employees = await _leaveAllocationsService.GetEmployees();
            return View(employees);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllocateLeave(string? id)
        {
            await _leaveAllocationsService.AllocateLeave(id);
            return RedirectToAction(nameof(Details), new { userId = id });
        }

        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> EditAllocation(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var allocation = await _leaveAllocationsService.GetEmployeeAllocation(id.Value);
            if (allocation == null)
            {
                return NotFound();
            }
            return View(allocation);
        }

        [Authorize(Roles = Roles.Administrator)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAllocation(LeaveAllocationEditViewModel allocationEditViewModel)
        {
            if (await _leaveTypesService.DaysExceedMaximum(allocationEditViewModel.LeaveType.Id, allocationEditViewModel.NumberOfDays))
            {
                ModelState.AddModelError("NumberOfDays", "The number of days exceeds the maximum allowed for this leave type.");
            }

            if (ModelState.IsValid)
            {
                await _leaveAllocationsService.EditAllocation(allocationEditViewModel);
                return RedirectToAction(nameof(Details), new { userId = allocationEditViewModel.Employee.Id });
            }

            return View(allocationEditViewModel);
        }

        public async Task<IActionResult> Details(string? userId)
        {
            var employeeViewModel = await _leaveAllocationsService.GetEmployeeAllocations(userId);
            return View(employeeViewModel);
        }
    }
}
