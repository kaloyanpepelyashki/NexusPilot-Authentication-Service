using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NexusPilot_Auth_Service.Services;

namespace NexusPilot_Auth_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetrievalController : ControllerBase
    {
        private readonly UserService _userService;

        public RetrievalController(UserService userService)
        {
            _userService = userService;
        }

        [Authorize]
        [HttpGet("getUser/{userUUID}")]
        public async Task<ActionResult> GetUserAccount(string userUUID)
        {
            try
            {
                var result = await _userService.GetUserAccount(userUUID);

                if (result.isSuccess)
                {
                    return Ok(result.userAccount);
                }

                return StatusCode(404, "No record found");

            }catch (Exception e) 
            {
                Console.WriteLine("Error in controller. Error getting user account");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
