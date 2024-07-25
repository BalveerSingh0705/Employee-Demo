using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EmployeeManagement.Applictaion.Common.Email;
using EmployeeManagement.Applictaion.Services;
using EmployeeManagement.Applictaion.Services.DevImpl;
using EmployeeManagement.Applictaion.Services.Impl;

namespace EmployeeManagement.Applictaion;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IWebHostEnvironment env)
    {


        return services;
    }

  

 

    public static void AddEmailConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration.GetSection("SmtpSettings").Get<SmtpSettings>());
    }
}
