using FluentValidation;
using FluentValidation.AspNetCore;
using EmployeeManagement.API;
using EmployeeManagement.API.Filters;
using EmployeeManagement.API.Middleware;
using EmployeeManagement.DataAccess;
using EmployeeManagement.DataAccess.Persistence;
using EmployeeManagement.Applictaion.Models.Validators;
using EmployeeManagement.Applictaion;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(
    config => config.Filters.Add(typeof(ValidateModelAttribute))
);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining(typeof(IValidationsMarker));

builder.Services.AddSwagger();

builder.Services.AddDataAccess(builder.Configuration)
    .AddApplication(builder.Environment);

builder.Services.AddJwt(builder.Configuration);

builder.Services.AddEmailConfiguration(builder.Configuration);

var app = builder.Build();

using var scope = app.Services.CreateScope();

await AutomatedMigration.MigrateAsync(scope.ServiceProvider);

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "N-Tier V1"); });

app.UseHttpsRedirection();

app.UseCors(corsPolicyBuilder =>
    corsPolicyBuilder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
);

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<PerformanceMiddleware>();

app.UseMiddleware<TransactionMiddleware>();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();

namespace EmployeeManagement.API
{
    public partial class Program { }
}
