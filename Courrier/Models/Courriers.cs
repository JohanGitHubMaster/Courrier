namespace Courrier.Models
{
    public class Courriers
    {
        public int Id { get; set; }
        public string? Réferences { get; set; }
        public string? Expediteur { get; set; }
        public string? Objet { get; set; }
        public string? Commentaire { get; set; }
        public int CoursierId { get; set; }
        public int ReceptionisteId { get; set; }
        public int FlagId { get; set; }
        public int StatusId { get; set; }

        public Coursier? Coursier { get; set; }
        public Receptioniste? Receptioniste { get; set;}
        public Flag? Flag { get; set; }
        public Status? Status { get; set; }
        public List<CourrierDestinataire>? CourrierDestinataires { get; set;}
    }
}
