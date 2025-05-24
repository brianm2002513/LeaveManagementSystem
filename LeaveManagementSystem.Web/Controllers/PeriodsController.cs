using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.Periods;
using LeaveManagementSystem.Web.Services.PeriodService;

namespace LeaveManagementSystem.Web.Controllers
{
    [Authorize(Roles = Roles.Administrator)]
    public class PeriodsController(IPeriodsService periodsService) : Controller
    {
        private const string NameExistsValidationMessage = "This period name already exists.";
        private const string PeriodExistsValidationMessage = "This time period already exists.";
        private const string InvalidDatesValidationMessage = "The start date must be before the end date and after today.";
        private readonly IPeriodsService _periodsService = periodsService;

        // GET: Periods
        public async Task<IActionResult> Index()
        {
            var viewData = await _periodsService.GetAllPeriodsAsync();
            return View(viewData);
        }

        // GET: Periods/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewData = await _periodsService.GetPeriodById<PeriodReadOnlyViewModel>(id.Value);
            if (viewData == null)
            {
                return NotFound();
            }

            return View(viewData);
        }

        // GET: Periods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Periods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PeriodCreateViewModel periodCreate)
        {

            if (await _periodsService.CheckIfPeriodNameExistsAsync(periodCreate.Name))
            {
                ModelState.AddModelError(string.Empty, NameExistsValidationMessage);
            }

            if (await _periodsService.CheckIfPeriodExistsForCreateAsync(periodCreate.StartDate, periodCreate.EndDate))
            {
                ModelState.AddModelError(string.Empty, PeriodExistsValidationMessage);
            }

            if (!_periodsService.CheckIfDatesValid(periodCreate.StartDate, periodCreate.EndDate))
            {
                ModelState.AddModelError(string.Empty, InvalidDatesValidationMessage);
            }

            if (ModelState.IsValid)
            {
                await _periodsService.CreatePeriod(periodCreate);
                return RedirectToAction(nameof(Index));
            }
            return View(periodCreate);
        }

        // GET: Periods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewData = await _periodsService.GetPeriodById<PeriodEditViewModel>(id.Value);
            if (viewData == null)
            {
                return NotFound();
            }
            return View(viewData);
        }

        // POST: Periods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PeriodEditViewModel periodEdit)
        {
            if (id != periodEdit.Id)
            {
                return NotFound();
            }

            // Adding custom validation and adding model state error
            if (await _periodsService.CheckIfPeriodNameExistsForEditAsync(periodEdit))
            {
                ModelState.AddModelError(string.Empty, NameExistsValidationMessage);
            }

            if (await _periodsService.CheckIfPeriodExistsForEditAsync(periodEdit.StartDate, periodEdit.EndDate, periodEdit.Id))
            {
                ModelState.AddModelError(string.Empty, PeriodExistsValidationMessage);
            }

            if (!_periodsService.CheckIfDatesValid(periodEdit.StartDate, periodEdit.EndDate))
            {
                ModelState.AddModelError(string.Empty, InvalidDatesValidationMessage);
            }


            if (ModelState.IsValid)
            {
                try
                {
                    await _periodsService.EditPeriod(periodEdit);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_periodsService.periodExists(periodEdit.Id))
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
            return View(periodEdit);
        }

        // GET: Periods/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewData = await _periodsService.GetPeriodById<PeriodReadOnlyViewModel>(id.Value);
            if (viewData == null)
            {
                return NotFound();
            }

            return View(viewData);
        }

        // POST: Periods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _periodsService.RemovePeriod(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
