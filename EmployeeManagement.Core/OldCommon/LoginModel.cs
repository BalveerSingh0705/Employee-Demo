using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Core.Common
{
    public class LoginModel
    {
        public string emailUsername { get; set; }  // Corresponds to emailUsername in the formData
        public string password { get; set; }       // Corresponds to password in the formData
        public bool rememberMe { get; set; }       // Corresponds to rememberMe in the formData
    }


    //public class LoginModel
    //{
    //    public string Email { get; set; }
    //    public string Password { get; set; }
    //}

    public class AuthResponseLoginModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public DateTime? Expiration { get; set; }
    }




}
