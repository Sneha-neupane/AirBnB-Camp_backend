using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CampBackend.Data;
using CampBackend.Models;
using System.Collections.Generic;
using LiteDB;
using MySql.Data.MySqlClient;

namespace CampBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CampingAmenityController : ControllerBase
    {


        private DataContext _data;
        
        public CampingAmenityController()
        {
            string connectionString = "server=localhost;port=3306;user=root;password=root;database=airbnb";
            _data = new DataContext(connectionString);
        }
       



        [HttpGet]
        public ActionResult<IEnumerable<CampingAmenity>> Get()
        {
            return Ok(_data.GetCampingAmenity());
        }

        [HttpPost]
        [Authorize(Policy = "BasicAuthentication")]
        public ActionResult Post([FromBody] CampingAmenity campingAmenity)
        {
            _data.AddCampingAmenity(campingAmenity);
            return Ok("CampingAmenity added");
        }
        [HttpGet("{id}")]
        public ActionResult<CampingAmenity> Get(int id)
        {
            CampingAmenity c = _data.GetCampingAmenity(id);
            if (c != null) return Ok(c);
            return NotFound("CampingAmenity not found");
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "BasicAuthentication")]
        public ActionResult DeleteCampsiteAmenities(int id)
        {
            CampingAmenity campingAmenity = _data.GetCampingAmenity(id);
            _data.DeleteCampsiteAmenities(id);
            return Ok("campingamenity was deleted");

        }
    }

}
