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
        readonly Ds4FileService _ds4FileService;
        public FilesController(Ds0FileService ds0FileService, Ds4FileService ds4FileService)
        {
            _ds0FileService = ds0FileService;
            _ds4FileService = ds4FileService;
        }

        [HttpPost("ds0")]
        public async Task<IActionResult> GenerateDs0([FromBody] Ds0Request request)
        {
            await _ds0FileService.GenerateFileAsync(request);
            return Ok("A file has been generated successfully.");
        }

        [HttpPost("ds4")]
        public async Task<IActionResult> GenerateDs4([FromBody] Ds4Request request)
        {
            await _ds4FileService.GenerateBasicFileAsync(request);
            return Ok("A file has been generated successfully.");
        }
    }
}
