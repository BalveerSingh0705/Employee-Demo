using System;

namespace E_Commerce.Shared.DataObject
{
    public interface IBaseDataObject
    {
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
        DateTime CreatedDate { get; }
        DateTime ModifiedDate { get; }
        string RoleCode { get; set; }
        Guid? TenantId { get; set; }
        Guid UserId { get; set; }
        string UserName { get; set; }
        string SubDomain { get; set; }
        string SyncType { get; set; }
    }

#if BACKEND
    public class BaseDataObject : EntityData, IBaseDataObject
    {
        public BaseDataObject()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
#else
    public class BaseDataObject : IBaseDataObject
    {
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string RoleCode { get; set; }
        public Guid? TenantId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
        public string UserName { get; set; }
        public string SubDomain { get; set; }
        public string SyncType { get; set; }
    }
#endif
}
