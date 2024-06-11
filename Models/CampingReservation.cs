namespace CampBackend.Models
{
    public class CampingReservation
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int CampsiteId { get; set; }

        public int GuestId { get; set; }
    }
}
