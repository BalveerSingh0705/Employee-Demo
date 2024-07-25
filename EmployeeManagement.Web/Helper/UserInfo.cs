
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Web.Helper
{
    public class UserInfo
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserInfo(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserPassword()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("UserPassword");
        }

        public string GetServiceToken()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("ServiceToken");
        }

        public string GetUserName()
        {
            return _httpContextAccessor.HttpContext.Session.GetString("UserName");
        }
    }
}
