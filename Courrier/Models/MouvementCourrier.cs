namespace Courrier.Models
{
    public class MouvementCourrier
    {
        public int Id { get; set; }

        public Status? Status { get; set; }

        public Destinataire? Destinataire { get; set; }

        public Receptioniste? Receptioniste { get; set; }

        public DateTime DatedeMouvement { get; set; }

    }
}
