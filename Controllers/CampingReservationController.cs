using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CampBackend.Data;
using CampBackend.Models;
using System.Collections.Generic;
using System.Linq;

namespace CampBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CampingReservationController : ControllerBase
    {
        private DataContext _data;

        public CampingReservationController()
        {
            string connectionString = "server=localhost;port=3306;user=root;password=root;database=airbnb";
            _data = new DataContext(connectionString);
        }

        [HttpGet]
        public ActionResult<IEnumerable<CampingReservation>> Get()
        {
            return Ok(_data.GetReservation());
        }

        [HttpGet("ByGuest")]
        public ActionResult<IEnumerable<CampingReservation>> GetByGuestId([FromQuery] int guestId)
        {
            if (guestId <= 0)
            {
                return BadRequest("Invalid guest ID.");
            }

            var reservations = _data.GetReservation().Where(r => r.GuestId == guestId);
            return Ok(reservations);
        }

        [HttpPost]
        [Authorize(Policy = "BasicAuthentication")]
        public ActionResult Post([FromBody] CampingReservation reservation)
        {
            _data.AddReservation(reservation);
            return Ok("Reservation added");
        }

        [HttpGet("{id}")]
        public ActionResult<CampingReservation> Get(int id)
        {
            CampingReservation r = _data.GetReservation(id);
            if (r != null) return Ok(r);
            return NotFound("Reservation not found");
        }
    }
}
