using GIF.Core.Models;
using GIF.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace GIF.Api.Controllers
{
    [Route("api/files")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        readonly Ds0FileService _ds0FileService;
        public FilesController(Ds0FileService ds0FileService)
        {
            _ds0FileService = ds0FileService;
        }
        [HttpPost("ds0")]
        public async Task<IActionResult> GenerateDs0([FromBody] Ds0Request request)
        {
            await _ds0FileService.ExportFileAsync(request);
            return Ok("A file has been generated successfully.");
        }
    }
}
