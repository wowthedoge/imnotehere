using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NotesDbContext>(options => options.UseNpgsql("Host=localhost;Database=notesdb;Username=postgres;Password=yourpassword"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/api/notes", async (NotesDbContext dbContext) =>
{
    return Results.Ok("ABC");
});

app.Run();
internal class AddNoteRequest
{
    public required string Note { get; set; }
    public required LocationData Location { get; set; }
}

internal class LocationData
{
    public required decimal Latitude { get; set; }
    public required decimal Longitude { get; set; }
}


