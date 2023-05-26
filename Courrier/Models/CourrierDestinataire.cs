namespace Courrier.Models
{
    public class CourrierDestinataire
    {
        public int Id { get; set; }
        public int DestinataireId { get; set; }
        public int CourrierId { get; set; }
        public Destinataire? Destinataire { get; set; }
        public Courriers? Courrier { get; set; }
    }
}
