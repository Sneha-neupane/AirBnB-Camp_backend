using CampBackend.Data;
using CampBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CampBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
        private DataContext _data;
        public ReviewController()
        {
            string connectionString = "server=localhost;port=3306;user=root;password=root;database=airbnb";
            _data = new DataContext(connectionString);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Review>> Get()
        {
            return Ok(_data.GetReview());
        }

        [HttpPost]
        [Authorize(Policy = "BasicAuthentication")]
        public ActionResult Post([FromBody] Review review)
        {
            _data.AddReview(review);
            return Ok("Review was added");
        }
        [HttpGet("{id}")]
        public ActionResult<Review> Get(int id)
        {
            Review r = _data.GetReview(id);
            if (r != null) return Ok(r);
            return NotFound("Review not found");
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "BasicAuthentication")]
        public ActionResult Delete(int id)
        {
            Review review = _data.GetReview(id);
            if (review == null) return NotFound("Review not found");

            _data.DeleteReview(id);

            return Ok("Review deleted");
        }
    }
}
