namespace Courrier.Models
{
    public class Coursier
    {
        public int Id { get; set; }
        public string? Nom { get; set; }

        public List<Courriers>? Courriers { get; set; }
        public List<MouvementCourrier>? MouvementCourriers { get; set; }
    }
}
