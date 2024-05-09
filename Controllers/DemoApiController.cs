using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiBySijan.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiBySijan.Controllers
{
    [Route("api/demo")]
    [ApiController]
    public class DemoApiController : ControllerBase
    {
        private readonly ApiDemoDbContext _apiDemoDbContext;
        public DemoApiController(ApiDemoDbContext apiDemoDbContext)
        {
            _apiDemoDbContext= apiDemoDbContext;
        }

        [HttpGet]
        [Route("get-users-list")]
        public async Task<IActionResult> GetAsync()
        {
            var users = await _apiDemoDbContext.Users.ToListAsync();
            return Ok(users);
        }
        [HttpGet]
        [Route("get-users-by-id/{UserId}")]
        public async Task<IActionResult> GetUserByIdAsync(int UserId)
        {
            var user = await _apiDemoDbContext.Users.FindAsync(UserId);
            return Ok(user);
        }
        [HttpPost]
        [Route("Create-user")]
        public async Task<IActionResult> PostAsync(Users user)
        {
            _apiDemoDbContext.Users.Add(user);
            await _apiDemoDbContext.SaveChangesAsync();
            return Created($"/get-user-by-id/{user.UserId}",user);
        }
        [HttpPut]
        [Route("update-user")]
        public async Task<IActionResult> PutAsync(Users userToUpdate)
        {
            _apiDemoDbContext.Users.Update(userToUpdate);
            await _apiDemoDbContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        [Route("delete-user/{UserId}")]
        public async Task<IActionResult> DeleteAsync(int UserId)
        {
           var userToDelete=await _apiDemoDbContext.Users.FindAsync(UserId);
            if (userToDelete == null)
            {
                return NotFound();
            }
            _apiDemoDbContext.Users.Remove(userToDelete);
            await _apiDemoDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
