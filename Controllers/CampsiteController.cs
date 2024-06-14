using CampBackend.Data;
using CampBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CampBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CampsiteController : ControllerBase
    {
        private readonly DataContext _data;

        public CampsiteController()
        {
            string connectionString = "server=localhost;port=3306;user=root;password=root;database=airbnb";
            _data = new DataContext(connectionString);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Campsite>> Get()
        {
            return Ok(_data.GetCampsite());
        }

        [HttpPost]
        [Authorize(Policy = "BasicAuthentication")]
        public ActionResult Post([FromBody] Campsite campsite)
        {
            _data.AddCampsites(campsite);
            return Ok("Campsite was added!");
        }

        [HttpGet("{id}")]
        public ActionResult<Campsite> Get(int id)
        {
            Campsite c = _data.GetCampsite(id);
            if (c != null) return Ok(c);
            return NotFound("Campsite not found!");
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "BasicAuthentication")]
        public ActionResult Put(int id, [FromBody] Campsite updatedCampsite)
        {
            // Ensure the IDs match
            if (id != updatedCampsite.Id)
            {
                return BadRequest("Campsite ID mismatch");
            }

            // Check if the campsite exists
            Campsite existingCampsite = _data.GetCampsite(id);
            if (existingCampsite == null)
            {
                return NotFound("Campsite not found");
            }

            // Update the campsite
            _data.UpdateCampsite(id, updatedCampsite);

            return Ok("Campsite updated");
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "BasicAuthentication")]
        public ActionResult Delete(int id)
        {
            Campsite campsite = _data.GetCampsite(id);
            if (campsite == null) return NotFound("Campsite not found");

            _data.DeleteCampsite(id);

            return Ok("Campsite deleted");
        }
    }
}
