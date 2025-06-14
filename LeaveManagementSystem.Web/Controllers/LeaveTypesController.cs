﻿using LeaveManagementSystem.Application.Models.LeaveTypes;
using LeaveManagementSystem.Application.Services.LeaveTypeService;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class LeaveTypesController(ILeaveTypesService _leaveTypesService, ILogger<LeaveTypesController> _logger) : Controller
    {
        private const string NameExistsValidationMessage = "This leave type already exists.";

        // GET: LeaveTypes
        public async Task<IActionResult> Index()
        {
            _logger.LogInformation("Fetching all leave types.");
            var viewData = await _leaveTypesService.GetAllLeaveTypesAsync();
            // then return the viewmodel to the view
            return View(viewData);
        }

        // GET: LeaveTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // SELECT * FROM LeaveTypes WHERE Id = @id
            var viewData = await _leaveTypesService.GetLeaveTypeById<LeaveTypeReadOnlyViewModel>(id.Value);

            if (viewData == null)
            {
                return NotFound();
            }

            return View(viewData);
        }

        // GET: LeaveTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LeaveTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Bind only to Name and NumberOfDays and do not include any attempts to add an ID entry
        public async Task<IActionResult> Create(LeaveTypeCreateViewModel leaveTypeCreate)
        {
            // Adding custom validation and adding model state error
            if (await _leaveTypesService.CheckIfLeaveTypeNameExistsAsync(leaveTypeCreate.Name))
            {
                ModelState.AddModelError(nameof(leaveTypeCreate.Name), NameExistsValidationMessage);
            }

            if (ModelState.IsValid)
            {
                await _leaveTypesService.CreateLeaveType(leaveTypeCreate);
                return RedirectToAction(nameof(Index));
            }
            _logger.LogWarning("leave type attempt failed due to invalidity.");
            return View(leaveTypeCreate);
        }

        // GET: LeaveTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewData = await _leaveTypesService.GetLeaveTypeById<LeaveTypeEditViewModel>(id.Value);

            if (viewData == null)
            {
                return NotFound();
            }

            return View(viewData);
        }

        // POST: LeaveTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveTypeEditViewModel leaveTypeEdit)
        {
            if (id != leaveTypeEdit.Id)
            {
                return NotFound();
            }

            if (await _leaveTypesService.CheckIfLeaveTypeNameExistsForEditAsync(leaveTypeEdit))
            {
                ModelState.AddModelError(nameof(leaveTypeEdit.Name), NameExistsValidationMessage);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _leaveTypesService.EditLeaveType(leaveTypeEdit);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_leaveTypesService.LeaveTypeExists(leaveTypeEdit.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(leaveTypeEdit);
        }

        // GET: LeaveTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewData = await _leaveTypesService.GetLeaveTypeById<LeaveTypeReadOnlyViewModel>(id.Value);
            if (viewData == null)
            {
                return NotFound();
            }

            return View(viewData);
        }

        // POST: LeaveTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _leaveTypesService.RemoveLeaveType(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
