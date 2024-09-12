using EmployeeManagement.DAO.Interface;
using EmployeeManagement.Core.Common;
using EmployeeManagement.Business.Base;

namespace EmployeeManagement.Business
{
    public class AuthBO : BaseBO<IAuth>
    {

        //public AuthResponse AuthRegister(AuthRegisterViewModel authRegisterViewModel)
        //{
        //    return DAO.AuthRegister(authRegisterViewModel);
        //}
        public async Task<AuthResponse> AuthRegister(AuthRegisterViewModel authRegisterViewModel)
        {
            return DAO.AuthRegister(authRegisterViewModel);
        }

        public async Task<AuthResponseLoginModel> Login(LoginModel loginViewModel)
        {
            return DAO.Login(loginViewModel);
        }

    }
}
