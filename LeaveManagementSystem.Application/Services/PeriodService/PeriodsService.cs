using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LeaveManagementSystem.Application.Services.PeriodService
{
    public class PeriodsService(ApplicationDbContext context, IMapper mapper) : IPeriodsService
    {

        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task<List<PeriodReadOnlyViewModel>> GetAllPeriodsAsync()
        {
            var data = await _context.Periods.ToListAsync();
            var viewData = _mapper.Map<List<PeriodReadOnlyViewModel>>(data);
            // then return the viewmodel to the view
            return viewData;
        }

        public async Task<Period> GetCurrentPeriodAsync() {
            var currentDate = DateTime.Now;
            var period = await _context.Periods
                .SingleAsync(q => q.EndDate.Year == currentDate.Year);
            return period;
        }

        public async Task<T?> GetPeriodById<T>(int id) where T : class
        {
            var period = await _context.Periods.FirstOrDefaultAsync(m => m.Id == id);
            if (period == null)
            {
                return null;
            }
            var viewData = _mapper.Map<T>(period);
            return viewData;
        }

        public async Task RemovePeriod(int id)
        {
            var period = await _context.Periods.FirstOrDefaultAsync(m => m.Id == id);
            if (period != null)
            {
                _context.Remove(period);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditPeriod(PeriodEditViewModel model)
        {
            var period = _mapper.Map<Period>(model);
            _context.Update(period);
            await _context.SaveChangesAsync();
        }

        public async Task CreatePeriod(PeriodCreateViewModel model)
        {
            var period = _mapper.Map<Period>(model);
            _context.Add(period);
            await _context.SaveChangesAsync();
        }

        public bool periodExists(int id)
        {
            return _context.Periods.Any(e => e.Id == id);
        }

        public async Task<bool> CheckIfPeriodNameExistsAsync(string name)
        {
            var lowercaseName = name.ToLower();
            return await _context.Periods
                .AnyAsync(p => p.Name.ToLower() == lowercaseName);
        }

        public async Task<bool> CheckIfPeriodNameExistsForEditAsync(PeriodEditViewModel periodEdit)
        {
            var lowercaseName = periodEdit.Name.ToLower();
            return await _context.Periods
                .AnyAsync(q => q.Name.ToLower().Equals(lowercaseName) && q.Id != periodEdit.Id);
        }

        public async Task<bool> CheckIfPeriodExistsForCreateAsync(DateOnly startDate, DateOnly endDate)
        {
            return await _context.Periods
                .AnyAsync(q => q.StartDate == startDate && q.EndDate == endDate);
        }

        public async Task<bool> CheckIfPeriodExistsForEditAsync(DateOnly startDate, DateOnly endDate, int Id)
        {
            return await _context.Periods
                .AnyAsync(q => q.StartDate == startDate && q.EndDate == endDate && q.Id != Id);
        }

        public bool CheckIfDatesValid(DateOnly startDate, DateOnly endDate)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            Debug.WriteLine(startDate > today && endDate > today && startDate < endDate);
            return startDate > today && endDate > today && startDate < endDate;
        }
    }
}
