using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DataObject
{
    [Serializable]
    public class RoleCodeSessionModel
    {
        public string RoleCode { get; set; }
        public string RoleDescription { get; set; }
    }

    [Serializable]
    public class UserIdSessionModel
    {
        public Guid UserId { get; set; }
    }

    [Serializable]
    public class LoggedInUserSessionModel
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string ImageName { get; set; }
    }

    [Serializable]
    public class TenantIdSessionModel
    {
        public Guid? TenantId { get; set; }
    }
}
