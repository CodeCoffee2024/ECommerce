namespace ECommerce.Domain.Dtos.Shared
{
    public class PermissionDetailDTO
    {
        #region Properties

        public PermissionDetailDTO(string permission, string description, IEnumerable<string> dependencies)
        {
            Permission = permission;
            Description = description;
            Dependencies = dependencies;
        }

        public string Permission { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public IEnumerable<string> Dependencies { get; set; } = new List<string>();

        #endregion Properties
    }
}