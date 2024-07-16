using AzuriteDemo.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AzuriteDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private readonly BlobService _blobService;

        public BlobController(BlobService blobService)
        {
            _blobService = blobService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadBlob([FromQuery] string containerName, [FromQuery] string blobName, IFormFile file)
        {
            using var stream = file.OpenReadStream();
            await _blobService.UploadBlobAsync(containerName, blobName, stream);
            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteBlob([FromQuery] string containerName, [FromQuery] string blobName)
        {
            await _blobService.DeleteBlobAsync(containerName, blobName);
            return Ok();
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetBlob([FromQuery] string containerName, [FromQuery] string blobName)
        {
            var stream = await _blobService.GetBlobAsync(containerName, blobName);
            return File(stream, "application/octet-stream", blobName);
        }
    }
}
