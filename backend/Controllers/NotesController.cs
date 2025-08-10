using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/notes")]
public class NotesController(INoteService noteService, ILogger<NotesController> logger)
    : ControllerBase
{
    private readonly INoteService _noteService = noteService;
    private readonly ILogger _logger = logger;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Note>>> GetNotes(
        [FromQuery] decimal latitude = 0,
        [FromQuery] decimal longitude = 0
    )
    {
        _logger.LogInformation($"Get Notes for {latitude},{longitude}");
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
