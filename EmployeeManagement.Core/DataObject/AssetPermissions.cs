using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.DataObject
{
    public class AssetPermissions : BaseDataObject
    {
        public Guid? AssetId { get; set; }
        public string AssetName { get; set; }
        public string Access { get; set; }
        public string AssetTypeName { get; set; }
        public bool? IsActiveAsset { get; set; }

        public Guid? GuidUserAsset { get; set; }

        public string AssetPermissionRW { get; set; }

        public string UserRole { get; set; }
        public string Email { get; set; }

        public Guid? ParentAssetId { get; set; }
    }
}
