namespace Courrier.Models
{
    public class MouvementCourrier
    {
        public int Id { get; set; }
        public int? CoursierId { get; set; }
        public int? CourriersId { get; set; }
        public int? StatusId { get; set; }
        public int? ReceptionisteId { get; set; }

        public Status? Status { get; set; }
        public DateTime DatedeMouvement { get; set; }

        public Receptioniste? Receptioniste { get; set; }
        public Courriers? Courriers { get; set; }
        public Coursier? Coursier { get; set; }


    

    }
}
