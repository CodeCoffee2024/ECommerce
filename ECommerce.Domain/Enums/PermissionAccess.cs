using System.ComponentModel;

namespace ECommerce.Domain.Enums
{
    public enum PermissionAccess
    {
        [Description("View")]
        View,

        [Description("Modify")]
        Modify,

        [Description("Delete")]
        Delete
    }
}