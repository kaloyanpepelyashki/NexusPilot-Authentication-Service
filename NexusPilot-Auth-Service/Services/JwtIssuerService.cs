using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static NexusPilot_Auth_Service.Controllers.AuthController;

namespace NexusPilot_Auth_Service.Services
{

    /* This class should handle all operations related to JWT
     */
    public class JwtIssuerService
    {
        protected IConfiguration _configuration;

        protected string JWTIssuer;
        protected string JWTAudience;
        protected string JWTSecretKey;

        public JwtIssuerService(IConfiguration configuration)
        {
            _configuration = configuration;

             JWTIssuer = _configuration["JWTConfig:Issuer"];
             JWTAudience = _configuration["JWTConfig:Audience"];
             JWTSecretKey = _configuration["JWTConfig:SecretKey"];
        }

        /* This method is in charge of handling the generation of JWT, when user signs in.
         * The method takes in the SignInObject, in order to extract user email.
         * */
        public string IssueJWT(SignInObject userObject)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTSecretKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                List<Claim> claims =
                [
                    new Claim(JwtRegisteredClaimNames.Sub, userObject.Email),
                    new Claim("email", userObject.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                ];

                var token = new JwtSecurityToken(
                    issuer: JWTIssuer,
                    audience: JWTAudience,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtConfig:ExpiryMinutes"])),
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);

            } catch(Exception e)
            {
                throw;
            }
        }
    }
}
