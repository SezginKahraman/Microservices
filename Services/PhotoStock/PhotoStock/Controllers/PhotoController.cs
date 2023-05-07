using Microservices.Shared;
using Microservices.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using PhotoStock.Dtos;

namespace PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile file, CancellationToken token)
        {
            if (file != null && file.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream, token);
                }

                var returnPath = "photos/" + file.FileName;

                PhotoDto photo = new() { URL = returnPath };

                return CreateActionResultInstance(Response<PhotoDto>.Success(photo, 200));
            }

            return CreateActionResultInstance(Response<PhotoDto>.Fail("photo is empty", 400));


        }

        [HttpPost]
        public IActionResult PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", photoUrl);

            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(Response<NoContent>.Fail("photo not found", 404));
            }
            System.IO.File.Delete(path);

            return CreateActionResultInstance(Response<NoContent>.Success(204));
        }
    }
}
