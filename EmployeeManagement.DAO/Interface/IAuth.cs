using EmployeeManagement.Core.Common;


namespace EmployeeManagement.DAO.Interface
{
    public interface IAuth
    {
         AuthResponse AuthRegister(AuthRegisterViewModel authRegisterViewModel);
        AuthResponseLoginModel Login(LoginModel loginViewModel);
    }
}
