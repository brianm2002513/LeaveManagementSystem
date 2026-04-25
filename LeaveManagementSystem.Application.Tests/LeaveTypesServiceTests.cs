using LeaveManagementSystem.Application.Services.LeaveTypeService;
using LeaveManagementSystem.Data;
using LeaveManagementSystem.Application.MappingProfiles;
using LeaveManagementSystem.Application.Models.LeaveTypes;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Shouldly;
using System.Threading.Tasks;
using Xunit;
using System;

namespace LeaveManagementSystem.Application.Tests
{
    public class LeaveTypesServiceTests
    {
        private readonly LeaveTypesService _service;
        private readonly ApplicationDbContext _context;

        public LeaveTypesServiceTests()
        {
            // Set up EF Core In-Memory Database
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);

            // Set up AutoMapper
            var mapperConfig = new MapperConfiguration(c => 
            {
                c.AddProfile<AutoMapperProfile>(); 
            });
            var mapper = mapperConfig.CreateMapper();

            // Set up Moq for ILogger
            var loggerMock = new Mock<ILogger<LeaveTypesService>>();

            // Instantiate Service with In-Memory DB and Mocked Dependencies
            _service = new LeaveTypesService(_context, mapper, loggerMock.Object);
        }

        [Fact]
        public async Task CreateLeaveType_Successfully_Adds_To_Database()
        {
            // Arrange
            var model = new LeaveTypeCreateViewModel { Name = "Vacation", NumberOfDays = 10 };

            // Act
            await _service.CreateLeaveType(model);

            // Assert
            var leaveType = await _context.LeaveTypes.FirstOrDefaultAsync(q => q.Name == "Vacation");
            leaveType.ShouldNotBeNull();
            leaveType.NumberOfDays.ShouldBe(10);
        }

        [Fact]
        public async Task GetAllLeaveTypesAsync_Returns_Mapped_ViewModels()
        {
            // Arrange
            _context.LeaveTypes.Add(new LeaveManagementSystem.Data.LeaveType { Id = 1, Name = "Sick", NumberOfDays = 5 });
            _context.LeaveTypes.Add(new LeaveManagementSystem.Data.LeaveType { Id = 2, Name = "Maternity", NumberOfDays = 90 });
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.GetAllLeaveTypesAsync();

            // Assert
            result.ShouldNotBeNull();
            result.Count.ShouldBe(2);
            result.ShouldContain(q => q.Name == "Sick");
            result.ShouldContain(q => q.Name == "Maternity");
        }

        [Fact]
        public async Task DaysExceedMaximum_Returns_True_When_Limit_Exceeded()
        {
            // Arrange
            _context.LeaveTypes.Add(new LeaveManagementSystem.Data.LeaveType { Id = 10, Name = "Paternity", NumberOfDays = 14 });
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.DaysExceedMaximum(10, 15);

            // Assert
            result.ShouldBeTrue();
        }

        [Fact]
        public async Task DaysExceedMaximum_Returns_False_When_Under_Limit()
        {
            // Arrange
            _context.LeaveTypes.Add(new LeaveManagementSystem.Data.LeaveType { Id = 11, Name = "Bereavement", NumberOfDays = 5 });
            await _context.SaveChangesAsync();

            // Act
            var result = await _service.DaysExceedMaximum(11, 3);

            // Assert
            result.ShouldBeFalse();
        }
    }
}
