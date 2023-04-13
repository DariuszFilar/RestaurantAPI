using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;

namespace RestaurantAPI.Controllers
{
    public class FileController : Controller
    {
        [Route("file")]
        [HttpGet]
        [ResponseCache(Duration = 1200, VaryByQueryKeys = new[] {"fileName"})]
        public ActionResult GetFile([FromQuery] string fileName)
        {
            var rootPath = Directory.GetCurrentDirectory();

            var filePath = $"{rootPath}/PrivateFiles/{fileName}";

            var fileExists = System.IO.File.Exists(filePath);

            if (!fileExists)
            {
                return NotFound();
            }

            var fileContents = System.IO.File.ReadAllBytes(filePath);
            var contentProvider = new FileExtensionContentTypeProvider();

            contentProvider.TryGetContentType(fileName, out string contentType);

            return File(fileContents, contentType, fileName);
            
        }

        [HttpPost]
        public ActionResult Upload([FromForm]IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var rootPath = Directory.GetCurrentDirectory();
                var fileName = file.Name;
                var fullPath = $"{rootPath}/PrivateFiles/{fileName}";

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);               
                }

                return Ok();
            }

            return BadRequest();
        }
    }
}
