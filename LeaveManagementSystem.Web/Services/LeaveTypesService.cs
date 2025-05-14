using AutoMapper;
using LeaveManagementSystem.Web.Data;
using LeaveManagementSystem.Web.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Services;

public class LeaveTypesService(ApplicationDbContext context, IMapper mapper) : ILeaveTypesService
{
    private readonly ApplicationDbContext _context = context;
    private readonly IMapper _mapper = mapper;

    public async Task<List<LeaveTypeReadOnlyViewModel>> GetAllLeaveTypesAsync()
    {
        // var data = SELECT * FROM LeaveTypes
        var data = await _context.LeaveTypes.ToListAsync();

        // We create a View Model so that the Index view stops using the direct data
        // So we convert the datamodel into a viewmodel
        //var viewData = data.Select(q => new IndexViewModel
        //{
        //    Id = q.Id,
        //    Name = q.Name,
        //    NumberOfDays = q.NumberOfDays
        //}); --use AutoMapper
        var viewData = _mapper.Map<List<LeaveTypeReadOnlyViewModel>>(data);
        // then return the viewmodel to the view
        return viewData;
    }

    public async Task<T?> GetLeaveTypeById<T>(int id) where T : class
    {
        var leaveType = await _context.LeaveTypes.FirstOrDefaultAsync(m => m.Id == id);

        if (leaveType == null)
        {
            return null;
        }

        var viewData = _mapper.Map<T>(leaveType);

        return viewData;
    }

    public async Task RemoveLeaveType(int id)
    {
        var leaveType = await _context.LeaveTypes.FirstOrDefaultAsync(m => m.Id == id);
        if (leaveType != null)
        {
            _context.LeaveTypes.Remove(leaveType);
            await _context.SaveChangesAsync();
        }
    }

    public async Task EditLeaveType(LeaveTypeEditViewModel model)
    {
        var leaveType = _mapper.Map<LeaveType>(model);
        _context.Update(leaveType);
        await _context.SaveChangesAsync();
    }

    public async Task CreateLeaveType(LeaveTypeCreateViewModel model)
    {
        var leaveType = _mapper.Map<LeaveType>(model);
        _context.Add(leaveType);
        await _context.SaveChangesAsync();
    }

    public bool LeaveTypeExists(int id)
    {
        return _context.LeaveTypes.Any(e => e.Id == id);
    }

    public async Task<bool> CheckIfLeaveTypeNameExistsAsync(string name)
    {
        var lowercaseName = name.ToLower();
        return await _context.LeaveTypes.AnyAsync(q => q.Name.ToLower().Equals(name));
    }

    public async Task<bool> CheckIfLeaveTypeNameExistsForEditAsync(LeaveTypeEditViewModel leaveTypeEdit)
    {
        var lowercaseName = leaveTypeEdit.Name.ToLower();
        return await _context.LeaveTypes.AnyAsync(q => q.Name.ToLower().Equals(lowercaseName) && q.Id != leaveTypeEdit.Id);
    }

}
