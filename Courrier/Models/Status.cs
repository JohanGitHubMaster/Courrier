namespace Courrier.Models
{
    public class Status
    {
        public int Id { get; set; }
        public string? Type { get; set; }

        public List<Courriers>? Courriers { get; set; }
    }
}
