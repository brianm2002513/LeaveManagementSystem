using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LeaveManagementSystem.Web.Data;
using System.Reflection;
using LeaveManagementSystem.Web.Services.Email;
using LeaveManagementSystem.Web.Services.LeaveTypeService;
using LeaveManagementSystem.Web.Services.PeriodService;
using LeaveManagementSystem.Web.Services.LeaveAllocationService;
using LeaveManagementSystem.Web.Services.LeaveRequestService;
using LeaveManagementSystem.Web.Services.UserService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddScoped<ILeaveTypesService, LeaveTypesService>();
builder.Services.AddScoped<IPeriodsService, PeriodsService>();
builder.Services.AddScoped<ILeaveAllocationsService, LeaveAllocationsService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<ILeaveRequestService, LeaveRequestService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminSupervisorOnly", policy =>
    {
        policy.RequireRole(Roles.Administrator, Roles.Supervisor);
    });
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();
  
// Configure the HTTP request pipeline. 
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
