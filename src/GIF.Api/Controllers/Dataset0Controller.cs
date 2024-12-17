using GIF.Core.Models;
using GIF.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace GIF.Api.Controllers
{
    [Route("api/dataset0")]
    [ApiController]
    public class Dataset0Controller(Ds0FileService ds0FileService) : ControllerBase
    {
        [HttpPost("adm")]
        public async Task<IActionResult> CreateFile([FromBody] Ds0AdmRequest request)
        {
            var sql = await ds0FileService.GenerateAdmFileAsync(request);
            return Ok(sql);
        }
    }
}
