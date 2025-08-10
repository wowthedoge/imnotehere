namespace Backend.DTOs
{
    public class AddNoteRequest
    {
        public required string Note { get; set; }
        public required decimal Latitude { get; set; }
        public required decimal Longitude { get; set; }
    }
}
