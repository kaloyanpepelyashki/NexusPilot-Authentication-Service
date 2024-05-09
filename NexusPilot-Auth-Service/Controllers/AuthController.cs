using Microsoft.AspNetCore.Mvc;
using NexusPilot_Auth_Service.Models.ExceptionModels;
using NexusPilot_Auth_Service.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NexusPilot_Auth_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private AuthService _authService;
        private JwtIssuerService _jwtIssuerService;
        public AuthController(JwtIssuerService jwtIssuerService)
        {   
            _jwtIssuerService = jwtIssuerService;
            _authService = AuthService.GetInstance();
        }

        [HttpPost("signup")]
        public async Task<ActionResult> SignUp([FromBody] SignUpObject signUpObject)
        {
            try
            {
                var result = await _authService.SignUp(signUpObject.NickName, signUpObject.Bio, signUpObject.Role, signUpObject.Email, signUpObject.Password);

                if (result)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500, "Server Error");
                }
            } catch(Exception e)
            {
                Console.WriteLine($"{e}");
                return StatusCode(500, $"{e}");
            }
        }

        [HttpPost("signin")]
        public async Task<ActionResult> SignIn([FromBody] SignInObject signInObject)
        {
            try
            {
                var result = await _authService.SignIn(signInObject.Email, signInObject.Password);

                if(result.isSuccess)
                {
                    string jwt = _jwtIssuerService.IssueJWT(signInObject);

                    return Ok(new {token = jwt, email = result.userObject.Email, id = result.userObject.Id });

                } else
                {
                    return StatusCode(500, "Server Error");
                }
            } catch(AuthException e)
            {
                return StatusCode(400, e.Message);

            } catch(Exception e)
            {
                Console.WriteLine($"{e}");
                return StatusCode(500, $"{e}");
            }
        }



        public class SignInObject
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class SignUpObject 
        {
            public string NickName { get; set; }
            public string Bio { get; set; }
            public string Role { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

    }
}
