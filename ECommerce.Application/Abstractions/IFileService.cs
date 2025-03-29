using Microsoft.AspNetCore.Http;

namespace ECommerce.Application.Abstractions
{
    public interface IFileService
    {
        #region Public Methods

        Task<string> UploadImage(IFormFile file);

        //string UploadFile(IFormFile file);

        #endregion Public Methods
    }
}