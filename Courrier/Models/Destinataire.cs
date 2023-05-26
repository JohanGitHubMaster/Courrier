namespace Courrier.Models
{
    public class Destinataire
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public List<Courriers>? Courriers { get; set; }
    }
}
