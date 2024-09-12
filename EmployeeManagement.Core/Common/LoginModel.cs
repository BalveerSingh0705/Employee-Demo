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


        public class AuthLoginResponse
        {
            public bool IsSuccess { get; set; }
            public string Token { get; set; }
            public string ErrorMessage { get; set; }
        }

    public class AuthResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public int? UserId { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // Store hashed passwords
    }



}
