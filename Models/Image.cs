namespace CampBackend.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string url { get; set; }
        public int CampsiteId { get; set; }
    }
}
