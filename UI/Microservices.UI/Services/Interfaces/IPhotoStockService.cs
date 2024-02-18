using Microservices.UI.Models.PhotoStocks;

namespace Microservices.UI.Services.Interfaces
{
    public interface IPhotoStockService
    {
        Task<PhotoViewModel> UploadPhoto(IFormFile file);

        Task<bool> DeletePhoto(string photoUrl);
    }
}
