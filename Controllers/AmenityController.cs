using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CampBackend.Data;
using CampBackend.Models;
using System.Collections.Generic;
using LiteDB;

namespace CampBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AmenityController : ControllerBase
    {


        private DataContext _data;
        public AmenityController()
        {
            string connectionString = "server=localhost;port=3306;user=root;password=root;database=airbnb";
            _data = new DataContext(connectionString);
        }



        [HttpGet]
        public ActionResult<IEnumerable<Amenity>> Get()
        {
            return Ok(_data.GetAmenity());
        }

        [HttpPost]
        [Authorize(Policy = "BasicAuthentication")]
        public ActionResult Post([FromBody] Amenity amenity)
        {
            _data.AddAmenity(amenity);
            return Ok("Amenity added");
        }
        [HttpGet("{id}")]
        public ActionResult<Amenity> Get(int id)
        {
            Amenity m = _data.GetAmenity(id);
            if (m != null) return Ok(m);
            return NotFound("Amenity not found");
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "BasicAuthentication")]
        public ActionResult Delete(int id)
        {
            Amenity amenity = _data.GetAmenity(id);
            if (amenity == null) return NotFound("Amenity not found");

            _data.DeleteAmenity(id);

            return Ok("Amenity deleted");
        }



    }

}
