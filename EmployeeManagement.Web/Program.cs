using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmployeeManagmentUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// If you are using Identity with Razor Pages, you should register Razor Pages services
builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
{
    options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "");
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddControllersWithViews();

// Configure the HTTP request pipeline.
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages(); // This ensures Razor Pages are mapped

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=RegisterBasic}/{id?}");

app.Run();
