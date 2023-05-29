namespace Courrier.Models
{
    public class Receptioniste
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public List<Courriers>? Courriers { get; set; }
        public List<MouvementCourrier>? MouvementCourriers { get; set; }
    }
}
