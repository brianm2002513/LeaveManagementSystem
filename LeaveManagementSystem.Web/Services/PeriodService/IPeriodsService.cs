using LeaveManagementSystem.Web.Models.Periods;

namespace LeaveManagementSystem.Web.Services.PeriodService
{
    public interface IPeriodsService
    {
        bool CheckIfDatesValid(DateOnly startDate, DateOnly endDate);
        Task<bool> CheckIfPeriodExistsForCreateAsync(DateOnly startDate, DateOnly endDate);
        Task<bool> CheckIfPeriodExistsForEditAsync(DateOnly startDate, DateOnly endDate, int Id);
        Task<bool> CheckIfPeriodNameExistsAsync(string name);
        Task<bool> CheckIfPeriodNameExistsForEditAsync(PeriodEditViewModel periodEdit);
        Task CreatePeriod(PeriodCreateViewModel model);
        Task EditPeriod(PeriodEditViewModel model);
        Task<List<PeriodReadOnlyViewModel>> GetAllPeriodsAsync();
        Task<Period> GetCurrentPeriodAsync();
        Task<T?> GetPeriodById<T>(int id) where T : class;
        bool periodExists(int id);
        Task RemovePeriod(int id);
    }
}