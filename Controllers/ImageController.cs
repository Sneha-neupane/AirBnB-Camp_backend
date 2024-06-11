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
    public class ImageController : ControllerBase
    {


        private DataContext _data;
        public ImageController()
        {
            string connectionString = "server=localhost;port=3306;user=root;password=root;database=airbnb";
            _data = new DataContext(connectionString);
        }



        [HttpGet]
        public ActionResult<IEnumerable<Image>> Get()
        {
            return Ok(_data.GetImage());
        }

        [HttpPost]
        [Authorize(Policy = "BasicAuthentication")]
        public ActionResult Post([FromBody] Image image)
        {
            _data.AddImages(image);
            return Ok("Image added");
        }
        [HttpGet("{id}")]
        public ActionResult<Image> Get(int id)
        {
            Image i = _data.GetImage(id);
            if (i != null) return Ok(i);
            return NotFound("Image not found");
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "BasicAuthentication")]
        public ActionResult Delete(int id)
        {
            Image image = _data.GetImage(id);
            if (image == null) return NotFound("Image not found");

            _data.DeleteImage(id);

            return Ok("Image deleted");
        }



    }

}
