using Backend.Data;
using Backend.DTOs;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public interface INoteService
{
    Task<List<Note>> GetNotesAsync(decimal latitude, decimal longitude);
    Task<Note> AddNoteAsync(AddNoteRequest request);
}

public class NoteService(NotesDbContext dbContext) : INoteService
{
    private readonly NotesDbContext _dbContext = dbContext;
    private const double radiusKm = 0.01;

    public async Task<List<Note>> GetNotesAsync(decimal latitude, decimal longitude)
    {
        var latDelta = (decimal)(radiusKm / 111.0);
        var lonDelta = (decimal)(radiusKm / (111.0 * Math.Cos(Math.PI * (double)latitude / 180)));

        var minLat = latitude - latDelta;
        var maxLat = latitude + latDelta;
        var minLon = longitude - lonDelta;
        var maxLon = longitude + lonDelta;

        return await _dbContext.Notes
            .Where(n => n.Latitude >= minLat && n.Latitude <= maxLat &&
                       n.Longitude >= minLon && n.Longitude <= maxLon)
            .ToListAsync();
    }

    public async Task<Note> AddNoteAsync(AddNoteRequest request)
    {
        var note = new Note
        {
            Content = request.Note,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
        };
        await _dbContext.Notes.AddAsync(note);
        await _dbContext.SaveChangesAsync();
        return note;
    }
}
