using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Data.Entities;
using SimpleAPI.Data.Repositories;
using System.Net;

namespace SimpleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserRepository _user;

        public UserController(IUserRepository user)
        {
            _user = user;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = new ObjectResult(_user.GetAll())  {
                StatusCode = (int) HttpStatusCode.OK
            };
            //Add data to Response headers
            Request.HttpContext.Response.Headers.Add("X-Name", "Hossein khakpoor");

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (await _user.IsExists(id))
            {

                var customer = await _user.Find(id);
                return Ok(customer);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> PostCustomer([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _user.Add(user);
            //return the posted User using getuser method(action name,route value,value)
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer([FromRoute] int id, [FromBody] User user)
        {
            await _user.Update(user);
            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            await _user.Remove(id);
            return Ok();
        }
    }
}
