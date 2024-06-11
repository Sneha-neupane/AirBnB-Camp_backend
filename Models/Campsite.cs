namespace CampBackend.Models
{
    public class Campsite
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        
        public int UserId { get; set; }
        
        public int LocationId { get;  set; }

        public string status { get; set; }
        

    }
}
