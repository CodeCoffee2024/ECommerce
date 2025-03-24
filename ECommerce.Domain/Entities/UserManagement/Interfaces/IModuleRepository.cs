using ECommerce.Domain.Commons;
using ECommerce.Domain.Dtos.Commons;

namespace ECommerce.Domain.Entities.UserManagement.Interfaces
{
    public interface IModuleRepository
    {
        #region Public Methods

        Task<UnpagedResult<Module>> GetAllModules(DefaultFilterBaseDto request);

        #endregion Public Methods
    }
}