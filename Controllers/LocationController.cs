using CampBackend.Data;
using CampBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocationController : ControllerBase
    {
        private DataContext _data;
        public LocationController()
        {
            string connectionString = "server=localhost;port=3306;user=root;password=root;database=airbnb";
            _data = new DataContext(connectionString);
        }
        


        [HttpGet]
        public ActionResult<IEnumerable<Location>> Get()
        {
            return Ok(_data.GetLocation());
        }

        [HttpPost]
        [Authorize(Policy = "BasicAuthentication")]
        public ActionResult Post([FromBody] Location location)
        {
            _data.AddLocations(location);
            return Ok("Location was added");
        }
        [HttpGet("{id}")]
        public ActionResult<Location> Get(int id) 
        {
            Location l = _data.GetLocation(id);
            if (l != null) return Ok(l);
            return NotFound("Location not found");
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "BasicAuthentication")]
        public ActionResult Delete(int id)
        {
            Location location = _data.GetLocation(id);
            if (location == null) return NotFound("Location not found");

            _data.DeleteLocation(id);

            return Ok("Location deleted");
        }

    }
}
