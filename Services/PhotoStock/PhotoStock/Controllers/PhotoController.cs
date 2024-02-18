using Microservices.Shared;
using Microservices.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using PhotoStock.Dtos;

namespace PhotoStock.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PhotoController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile file, CancellationToken token)
        {
            //While sending photo from postman, make sure that the parameter name in the 'form-data' section has to be the same withe parameter name of the action. !!

            if (file != null && file.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","photos", file.FileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream, token);
                }

                var returnPath = file.FileName;

                PhotoDto photo = new() { URL = returnPath };

                return CreateActionResultInstance(Response<PhotoDto>.Success(photo, 200));
            }

            return CreateActionResultInstance(Response<PhotoDto>.Fail("photo is empty", 400));


        }

        public IActionResult PhotoDelete(string photoName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "photos", photoName);

            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(Response<NoContent>.Fail("photo not found", 404));
            }
            System.IO.File.Delete(path);

            return CreateActionResultInstance(Response<NoContent>.Success(204));
        }
    }
}
