namespace CampBackend.Models
{
    public class Location
    {
        public int Id { get;  set; }
        public string Address { get; set; }

        public string City { get; set; }
        public string Country { get; set; }

        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
    }
}
