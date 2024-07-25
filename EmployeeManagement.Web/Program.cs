using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EmployeeManagmentUI;
using EmployeeManagmentUI.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("LoginContextConnection") ?? throw new InvalidOperationException("Connection string 'LoginContextConnection' not found.");
var connectionString1 = builder.Configuration.GetConnectionString("connectionString") ?? throw new InvalidOperationException("Connection string 'LoginContextConnection' not found.");
builder.Services.AddDbContext<LoginContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<EmployeeManagmentUIUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<LoginContext>();
//var connectionString = builder.Configuration.GetConnectionString("EmployeeMangContextConnection") ?? throw new InvalidOperationException("Connection string 'EmployeeMangContextConnection' not found.");
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

// Add services to the container.
//builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
//{
//    options.Conventions.AddAreaPageRoute("Identity", "/Account/Login", "");
//});

builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
// Map default route to Identity area login page
//app.MapControllerRoute(
//    name: "identity",
//    pattern: "Identity/{controller=Account}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboards}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
