using BLLayer.Interfaces;
using BLLayer.Services;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ITIDbContext>(options =>
    options.UseSqlServer("Server=.;Database=DeptInstructorDb;Trusted_Connection=True;TrustServerCertificate=True;"));

builder.Services.AddScoped<IDepartmentBl, DepartmentBL>();
builder.Services.AddScoped<IInstructorBl, InstructorBL>();
builder.Services.AddScoped<ICourseBl, CourseBL>();
builder.Services.AddScoped<ITraneeBl, TraneeBL>();
builder.Services.AddScoped<ITraneeCourseBl, TraneeCourseBL>();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();