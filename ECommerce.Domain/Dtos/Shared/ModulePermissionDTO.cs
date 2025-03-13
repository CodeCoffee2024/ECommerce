namespace ECommerce.Domain.Dtos.Shared
{
    public class ModulePermissionDTO
    {
        #region Properties

        public string Module { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IEnumerable<PermissionDetailDTO> Permissions { get; set; } = new List<PermissionDetailDTO>();

        #endregion Properties
    }
}