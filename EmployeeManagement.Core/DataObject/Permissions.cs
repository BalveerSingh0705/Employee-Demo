using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DataObject
{
    public class Permissions
    {
        public string Category { get; set; }


        public string PermissionName { get; set; }

        public string PermissionDescription { get; set; }

        public bool Granted { get; set; }
    }
}
