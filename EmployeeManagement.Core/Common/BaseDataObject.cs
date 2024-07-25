using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagement.Common
{
  
        public interface IBaseDataObject
        {
            string CreatedBy { get; set; }
            string ModifiedBy { get; set; }
            DateTime CreatedDate { get; }
            DateTime ModifiedDate { get; }
            string RoleCode { get; set; }

            Guid UserId { get; set; }
            string UserName { get; set; }


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
            /// <summary>
            /// The CreatedBy is the logged in user's id.
            /// </summary>
            public string CreatedBy { get; set; }

            /// <summary>
            /// The ModifiedBy is the logged in user's id.
            /// </summary>
            public string ModifiedBy { get; set; }

            /// <summary>
            /// The RoleCode is the rolecode of the logged in user.
            /// </summary>
            public string RoleCode { get; set; }



            /// <summary>
            /// The UserId is the id of user.
            /// </summary>
            public Guid UserId { get; set; }

            /// <summary>
            /// The CreatedDate is the date when new record added.
            /// </summary>
            public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

            /// <summary>
            /// The ModifiedDate is the date when the record get updated.
            /// </summary>
            public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

            public string Location { get; set; }

            public string UserName { get; set; }



        }
#endif
    

}
