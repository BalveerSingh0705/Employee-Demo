using EmployeeManagement.Applictaion.Common.Email;

namespace EmployeeManagement.Applictaion.Services;

public interface IEmailService
{
    Task SendEmailAsync(EmailMessage emailMessage);
}
