using CampBackend.Models;
namespace CampBackend.Data
{
    public class DataList: DataBase
    {
        

 
        static private List<User> users = new List<User>();
        public void AddUser(User user)
        {
            
        }

        public IEnumerable<User> GetUsers()
        {
            return users;
        }
        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }

        ///
        static private List<CampingReservation> reservations = new List<CampingReservation> ();
        public void AddReservation(CampingReservation reservation)
        {
            reservations.Add(reservation);
        }

        public IEnumerable<CampingReservation> GetReservation()
        {
            return reservations;
        }
        public CampingReservation GetReservation(int id)
        {
            throw new NotImplementedException();
        }

        ///
        static private List<Campsite> campsites = new List<Campsite>();
        public void AddCampsites(Campsite campsite)
        {
            campsites.Add(campsite);
        }
        public IEnumerable<Campsite> GetCampsite()
        {
            return campsites;
        }
        public Campsite GetCampsite(int id)
        {
            throw new NotImplementedException ();
        }

        ///
        static private List<Location> locations = new List<Location>();
        public void AddLocations(Location location)
        {
            locations.Add(location);
        }
        public IEnumerable<Location> GetLocation()
        {
            return locations;
        }
        public Location GetLocation(int id)
        {
            throw new NotImplementedException();
        }

        ///
        static private List<Image> images = new List<Image>();
        public void AddImages(Image image)
        {
            images.Add(image);
        }
        public IEnumerable<Image> GetImage()
        {
            return images;
        }
        public Image Getimage(int id)
        {
            throw new NotImplementedException();
        }

        ///
        static private List<Amenity> amenities = new List<Amenity>();
        public void AddAmenity(Amenity amenity)
        {
            amenities.Add(amenity);
        }
        public IEnumerable<Amenity> GetAmenity()
        {
            return amenities;
        }
        public Amenity GetAmenity(int id)
        {
            throw new NotImplementedException();
        }

        ///
        static private List<CampingAmenity> campingAmenities = new List<CampingAmenity>();
        public void AddCampingAmenity(CampingAmenity CampingAmenity)
        {
            campingAmenities.Add(CampingAmenity);
        }
        public IEnumerable<CampingAmenity> GetCampingAmenity()
        {
            return campingAmenities;
        }
        public CampingAmenity GetCampingAmenity(int id)
        {
            throw new NotImplementedException();
        }

        ///
        static private List<Review> reviews = new List<Review>();
        public void AddReview(Review review)
        {
            reviews.Add(review);
        }
        public IEnumerable<Review> GetReview()
        {
            return reviews;
        }
        public Review GetReview(int id)
        {
            throw new NotImplementedException();
        }
    }
}
