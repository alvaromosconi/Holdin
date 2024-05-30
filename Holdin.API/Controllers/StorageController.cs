using Holdin.API.Data;
using Holdin.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Holdin.API.Controllers;

[Route("api/storage")]
[ApiController]
public class StorageController : ControllerBase
{
    private readonly StorageService _fileService;
    private ApplicationDbContext _context;

    public StorageController(ApplicationDbContext context,
                             StorageService fileService)
    {
        _fileService = fileService;
        _context = context;
    }

    [HttpGet("{*relativePath}")]
    public  IActionResult GetContent([FromRoute] string relativePath = "")
    {
        var files = _fileService.GetContent(relativePath);

        return Ok(files);
    }

    [HttpPost("upload/{*relativePath}")]
    public async Task<IActionResult> Upload([FromRoute] string relativePath, [FromForm] IFormFile[] file)
    {
        await _fileService.Upload(relativePath, file);

        return Ok();
    }

    [HttpGet("download/{*relativePath}")]
    public FileResult Download([FromRoute] string relativePath = "")
    {
        FileStream file = _fileService.GetFile(relativePath);

        return File(file, "application/octet-stream", Path.GetFileName(file.Name));
    }
}
