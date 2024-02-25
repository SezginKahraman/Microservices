using Microservices.Shared.Dtos;
using Microservices.UI.Models.PhotoStocks;
using Microservices.UI.Services.Interfaces;

namespace Microservices.UI.Services
{
    public class PhotoStockService : IPhotoStockService
    {
        private readonly HttpClient _httpClient;

        public PhotoStockService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<PhotoViewModel> UploadPhoto(IFormFile file)
        {
            if (file == null || file.Length <= 0) return null;

            var randomileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

            using MemoryStream memoryStream = new MemoryStream();

            await file.CopyToAsync(memoryStream);

            var multiPartContent = new MultipartFormDataContent();

            multiPartContent.Add(new ByteArrayContent(memoryStream.ToArray()), "file", randomileName);

            var response = await _httpClient.PostAsync("photo/PhotoSave", multiPartContent);

            if (!response.IsSuccessStatusCode) return null;

            var responsePhoto = await response.Content.ReadFromJsonAsync<Response<PhotoViewModel>>();

            return responsePhoto.Data;
        }


        public async Task<bool> DeletePhoto(string photoUrl)
        {
            var response = _httpClient.DeleteAsync($"photo/PhotoDelete?photoName={photoUrl}");

            return response.IsCompletedSuccessfully;
        }
    }
}
