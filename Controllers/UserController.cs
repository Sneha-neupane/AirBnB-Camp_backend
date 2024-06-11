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
    public class UserController : ControllerBase
    {
        
       
        private DataContext _data;
        public UserController()
        {
            string connectionString = "server=localhost;port=3306;user=root;password=root;database=airbnb";
            _data = new DataContext(connectionString);
        }



        [HttpGet]
        public ActionResult<IEnumerable<User>> Get()
        {
            return Ok(_data.GetUsers());
        }

        [HttpPost]
        [Authorize(Policy = "BasicAuthentication")]
        public ActionResult Post([FromBody] User user)
        {
            try
            {
                var existingUser = _data.GetUsers().FirstOrDefault(u => u.Username == user.Username || u.Email == user.Email);
                if (existingUser != null)
                {
                    return Conflict("Username or Email already exists");
                }

                _data.AddUser(user);
                return Ok("User added successfully");
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error adding user: " + ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            User u = _data.GetUser(id);
            if (u != null) return Ok(u);
            return NotFound("User not found");
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "BasicAuthentication")]
        public ActionResult Put(int id, [FromBody] User updatedUser)
        {
            User existingUser = _data.GetUser(id);
            if (existingUser == null) return NotFound("User not found");

            _data.UpdateUser(id, updatedUser);
            return Ok("User updated");
        }




    }

}
