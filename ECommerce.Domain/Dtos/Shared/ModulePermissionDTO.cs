namespace ECommerce.Domain.Dtos.Shared
{
    public class ModulePermissionDTO
    {
        #region Properties

        public ModulePermissionDTO(string name, string description, int order, IEnumerable<PermissionDetailDTO> permissions)
        {
            Name = name;
            Order = order;
            Description = description;
            Permissions = permissions;
        }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Order { get; set; }
        public IEnumerable<PermissionDetailDTO> Permissions { get; set; } = new List<PermissionDetailDTO>();

        #endregion Properties
    }
}