using EmployeeManagement.Applictaion.Models;
using EmployeeManagement.Applictaion.Models.User;

namespace EmployeeManagement.Applictaion.Services;

public interface IUserService
{
    Task<BaseResponseModel> ChangePasswordAsync(Guid userId, ChangePasswordModel changePasswordModel);

    Task<ConfirmEmailResponseModel> ConfirmEmailAsync(ConfirmEmailModel confirmEmailModel);

    Task<CreateUserResponseModel> CreateAsync(CreateUserModel createUserModel);

    Task<LoginResponseModel> LoginAsync(LoginUserModel loginUserModel);
}
