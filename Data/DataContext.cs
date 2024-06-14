using CampBackend.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
namespace CampBackend.Data

{
    public class DataContext
    {
        private readonly string _connectionString;

        public DataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<User> GetUsers()
        {
            List<User> users = new List<User>();

            using var connection = new MySqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT * FROM user";
            using var cmd = new MySqlCommand(sql, connection);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = reader.GetInt32("Id"),
                    Username = reader.GetString("Username"),
                    FirstName = reader.GetString("Email"),
                    LastName = reader.GetString("FirstName"),
                    Email = reader.GetString("LastName"),
                    Password = reader.GetString("Password"),
                    Type = reader.GetString("Type"),
                });
            }

            return users;
        }
        public void AddUser(User user)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {

                connection.Open();
                string query = "INSERT INTO user (Username, Email, FirstName, LastName, Type, Password) VALUES (@Username, @Email, @FirstName, @LastName, @Type, @Password)";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@FirstName", user.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", user.LastName);
                    cmd.Parameters.AddWithValue("@Password", user.Password);
                    cmd.Parameters.AddWithValue("@Type", user.Type);


                    cmd.ExecuteNonQuery();
                }
            }
        }



        public User GetUser(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM User WHERE Id = @Id";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                Id = reader.GetInt32("Id"),
                                Username = reader.GetString("Username"),
                                Email = reader.GetString("Email"),
                                FirstName = reader.GetString("FirstName"),
                                LastName = reader.GetString("LastName"),
                                Password = reader.GetString("Password"),
                                Type = reader.GetString("Type")
                            };
                        }
                        else
                        {
                            return null; // User not found
                        }
                    }
                }
            }


        }


        public void UpdateUser(int id, User updatedUser)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
            string query = "UPDATE user SET Username = @Username, Email = @Email, FirstName= @FirstName, LastName = @LastName,Password = @Password, Type= @Type WHERE Id = @Id";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Username", updatedUser.Username);
            cmd.Parameters.AddWithValue("@Email", updatedUser.Email);
            cmd.Parameters.AddWithValue("@FirstName", updatedUser.FirstName);
            cmd.Parameters.AddWithValue("@LastName", updatedUser.LastName);
            cmd.Parameters.AddWithValue("@Password", updatedUser.Password);
            cmd.Parameters.AddWithValue("@Type", updatedUser.Type);
            cmd.Parameters.AddWithValue("@Id", updatedUser.Id);
            cmd.ExecuteNonQuery();
        }
        ///

        public int AddLocations(Location location)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Location (Address, City, Country, Latitude, Longitude) VALUES (@Address, @City, @Country, @Latitude, @Longitude); SELECT LAST_INSERT_ID();";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Address", location.Address);
                    cmd.Parameters.AddWithValue("@City", location.City);
                    cmd.Parameters.AddWithValue("@Country", location.Country);
                    cmd.Parameters.AddWithValue("@Latitude", location.latitude);
                    cmd.Parameters.AddWithValue("@Longitude", location.longitude);
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }

        public IEnumerable<Location> GetLocation()
        {
            List<Location> locations = new List<Location>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Location";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            locations.Add(new Location
                            {
                                Id = reader.GetInt32("Id"),
                                Address = reader.GetString("Address"),
                                City = reader.GetString("City"),
                                Country = reader.GetString("Country"),
                                latitude = reader.GetDecimal("Latitude"),
                                longitude = reader.GetDecimal("Longitude"),
                            });
                        }
                    }
                }
            }
            return locations;
        }
        public Location GetLocation(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Location WHERE Id = @Id";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Location
                            {
                                Id = reader.GetInt32("Id"),
                                Address = reader.GetString("Address"),
                                City = reader.GetString("City"),
                                Country = reader.GetString("Country"),
                                latitude = reader.GetDecimal("Latitude"),
                                longitude = reader.GetDecimal("Longitude")
                            };
                        }
                        else
                        {
                            return null; // Location not found
                        }
                    }
                }
            }
        }

        public void DeleteLocation(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("DELETE FROM location WHERE Id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        ///
        public void AddReservation(CampingReservation reservation)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "INSERT INTO CampingReservation (StartDate, EndDate, CampsiteId, GuestId) VALUES (@StartDate, @EndDate, @CampsiteId, @GuestId)";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@StartDate", reservation.StartDate);
                    cmd.Parameters.AddWithValue("@EndDate", reservation.EndDate);
                    cmd.Parameters.AddWithValue("@CampsiteId", reservation.CampsiteId);
                    cmd.Parameters.AddWithValue("@GuestId", reservation.GuestId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public IEnumerable<CampingReservation> GetReservation()
        {
            List<CampingReservation> campingReservations = new List<CampingReservation>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM CampingReservation";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            campingReservations.Add(new CampingReservation
                            {
                                Id = reader.GetInt32("Id"),
                                StartDate = reader.GetDateTime("StartDate"),
                                EndDate = reader.GetDateTime("EndDate"),
                                CampsiteId = reader.GetInt32("CampsiteId"),
                                GuestId = reader.GetInt32("GuestId"),
                            });
                        }
                    }
                }
            }
            return campingReservations;
        }
        public CampingReservation GetReservation(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM CampingReservation WHERE Id = @Id";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CampingReservation
                            {
                                Id = reader.GetInt32("Id"),
                                StartDate = reader.GetDateTime("StartDate"),
                                EndDate = reader.GetDateTime("EndDate"),
                                CampsiteId = reader.GetInt32("CampsiteId"),
                                GuestId = reader.GetInt32("GuestId"),
                            };
                        }
                        else
                        {
                            return null; // Reservation not found
                        }
                    }
                }
            }
        }
        //

        public void AddCampsites(Campsite campsite)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "INSERT INTO campsite (Name, Description, PricePerNight, LocationId, UserId, status) VALUES (@Name, @Description, @PricePerNight, @LocationId, @UserId, @status)";
                    using (var cmd = new MySqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Name", campsite.Name);
                        cmd.Parameters.AddWithValue("@Description", campsite.Description);
                        cmd.Parameters.AddWithValue("@PricePerNight", campsite.Price);
                        cmd.Parameters.AddWithValue("@LocationId", campsite.LocationId); // Ensure this line is included
                        cmd.Parameters.AddWithValue("@UserId", campsite.UserId); // Include UserId as well
                        cmd.Parameters.AddWithValue("@status", campsite.status);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    // Log the exception or rethrow it
                    throw new Exception("An error occurred while adding the campsite: " + ex.Message, ex);
                }
            }
        }


        public IEnumerable<Campsite> GetCampsite()
        {
            List<Campsite> campsites = new List<Campsite>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Campsite";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            campsites.Add(new Campsite
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name"),
                                Description = reader.GetString("Description"),
                                Price = reader.GetDecimal("PricePerNight"),

                                LocationId = reader.GetInt32("LocationId"),
                                UserId = reader.GetInt32("UserId"),
                                status = reader.GetString("status")

                            });
                        }
                    }
                }
            }
            return campsites;
        }
        public Campsite GetCampsite(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Campsite WHERE Id = @Id";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Campsite
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name"),
                                Description = reader.GetString("Description"),
                                Price = reader.GetDecimal("PricePerNight"),
                                LocationId = reader.GetInt32("LocationId"),
                                UserId = reader.GetInt32("UserId"),
                                status = reader.GetString("status")
                            };
                        }
                        else
                        {
                            return null; // Campsite not found
                        }
                    }
                }
            }


        }

        public void UpdateCampsite(int id, Campsite updatedCampsite)
        {
            using var connection = new MySqlConnection(_connectionString);
            connection.Open();
            string query = "UPDATE campsite SET Name = @Name, Description = @Description, PricePerNight = @PricePerNight, Status = @Status WHERE Id = @Id";
            using var cmd = new MySqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Name", updatedCampsite.Name);
            cmd.Parameters.AddWithValue("@Description", updatedCampsite.Description);
            cmd.Parameters.AddWithValue("@PricePerNight", updatedCampsite.Price);
            cmd.Parameters.AddWithValue("@Status", updatedCampsite.status);
            cmd.Parameters.AddWithValue("@Id", updatedCampsite.Id);
            cmd.ExecuteNonQuery();
        }


        public void DeleteCampsite(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("DELETE FROM campsite WHERE Id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }



        ///
        public void AddImages(Image image)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO image (Url, CampsiteId) VALUES (@Url, @CampsiteId)";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Url", image.url);
                    cmd.Parameters.AddWithValue("@CampsiteId", image.CampsiteId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public IEnumerable<Image> GetImage()
        {
            List<Image> campsites = new List<Image>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM image";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            campsites.Add(new Image
                            {
                                Id = reader.GetInt32("Id"),
                                url = reader.GetString("Url"),
                                CampsiteId = reader.GetInt32("CampsiteId"),

                            });
                        }
                    }
                }
            }
            return campsites;
        }
        public Image GetImage(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM image WHERE Id = @Id";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Image
                            {
                                Id = reader.GetInt32("Id"),
                                url = reader.GetString("Url"),
                                CampsiteId = reader.GetInt32("CampsiteId"),
                            };
                        }
                        else
                        {
                            return null; // Image not found
                        }
                    }
                }
            }

        }

        public void DeleteImage(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("DELETE FROM image WHERE Id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        ///
        public void AddAmenity(Amenity amenity)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO amenity (Name) VALUES (@Name)";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", amenity.Name);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public IEnumerable<Amenity> GetAmenity()
        {
            List<Amenity> amenities = new List<Amenity>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM amenity";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            amenities.Add(new Amenity
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name"),
                            });
                        }
                    }
                }
            }
            return amenities;
        }
        public Amenity GetAmenity(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM amenity WHERE Id = @Id";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Amenity
                            {
                                Id = reader.GetInt32("Id"),
                                Name = reader.GetString("Name"),
                            };
                        }
                        else
                        {
                            return null; // Image not found
                        }
                    }
                }
            }

        }
        public void DeleteAmenity(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("DELETE FROM amenity WHERE Id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
        ///
        public void AddCampingAmenity(CampingAmenity campingAmenity)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO campsiteamenity(CampsiteID,AmenityId) VALUES (@CampsiteId, @AmenityId)";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@CampsiteId", campingAmenity.CampsiteId);
                    cmd.Parameters.AddWithValue("@AmenityId", campingAmenity.AmenityId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
        public IEnumerable<CampingAmenity> GetCampingAmenity()
        {
            List<CampingAmenity> campingAmenities = new List<CampingAmenity>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM campsiteamenity";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            campingAmenities.Add(new CampingAmenity
                            {
                                CampsiteId = reader.GetInt32("CampsiteId"),
                                AmenityId = reader.GetInt32("AmenityId"),
                            });
                        }
                    }
                }
            }
            return campingAmenities;
        }
        public CampingAmenity GetCampingAmenity(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM campsiteamenity";
                using (var cmd = new MySqlCommand(query, connection))
                {

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new CampingAmenity
                            {
                                CampsiteId = reader.GetInt32("CampsiteId"),
                                AmenityId = reader.GetInt32("AmenityId"),
                            };
                        }
                        else
                        {
                            return null; // campingamenity not found
                        }
                    }
                }
            }

        }
        public void DeleteCampsiteAmenities(int campsiteId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("DELETE FROM campsiteamenity WHERE CampsiteId = @CampsiteId", connection))
                {
                    command.Parameters.AddWithValue("@CampsiteId", campsiteId);
                    command.ExecuteNonQuery();
                }
            }
        }
        ///
        public void AddReview(Review review)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();

                string query = "INSERT INTO review (UserId, CampsiteId,Comment, Rating) VALUES (@UserId, @CampsiteId, @comment, @rating)";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@UserId", review.UserId);
                    cmd.Parameters.AddWithValue("@CampsiteId", review.CampsiteId);
                    cmd.Parameters.AddWithValue("@comment", review.Comment);
                    cmd.Parameters.AddWithValue("@rating", review.Rating);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public IEnumerable<Review> GetReview()
        {
            List<Review> reviews = new List<Review>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM review";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            reviews.Add(new Review
                            {
                                Id = reader.GetInt32("Id"),
                                UserId = reader.GetInt32("userId"),
                                CampsiteId = reader.GetInt32("campsiteId"),
                                Comment = reader.GetString("comment"),
                                Rating = reader.GetInt32("rating"),
                            });
                        }
                    }
                }
            }
            return reviews;
        }
        public Review GetReview(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM review WHERE Id = @Id";
                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Review
                            {
                                Id = reader.GetInt32("Id"),
                                UserId = reader.GetInt32("userId"),
                                CampsiteId = reader.GetInt32("campsiteId"),
                                Comment = reader.GetString("comment"),
                                Rating = reader.GetInt32("rating"),
                            };
                        }
                        else
                        {
                            return null; // review not found
                        }
                    }
                }
            }

        }

        public void DeleteReview(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new MySqlCommand("DELETE FROM review WHERE Id = @id", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }

}