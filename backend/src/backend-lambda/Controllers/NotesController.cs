using Backend.Data;
using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/notes")]
public class NotesController(
    INoteService noteService,
    ILogger<NotesController> logger,
    NotesDbContext dbContext
) : ControllerBase
{
    private readonly INoteService _noteService = noteService;
    private readonly ILogger _logger = logger;
    private readonly NotesDbContext _dbContext = dbContext;

    [HttpPost("migrate")]
    public async Task<IActionResult> RunMigration()
    {
        try
        {
            await _dbContext.Database.MigrateAsync();
            _logger.LogInformation("✅ Database migration completed successfully");
            return Ok(new { message = "Migration completed successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "❌ Migration failed");
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Note>>> GetNotes(
        [FromQuery] decimal latitude = 0,
        [FromQuery] decimal longitude = 0
    )
    {
        var notes = await _noteService.GetNotesAsync(latitude, longitude);
        return Ok(notes);
    }

    [HttpPost]
    public async Task<ActionResult<Note>> CreateNote(AddNoteRequest request)
    {
        await _noteService.AddNoteAsync(request);
        return Created();
    }
}
