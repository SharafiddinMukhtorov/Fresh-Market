using Fresh_Market.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FreshMarket.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthentocationController : ControllerBase
    {

        [HttpPost("login")]
        public ActionResult<string> Login(LoginRequest request)
        {
            var user = Authenticate(request.Login, request.Password);

            if(user is null)
            {
                return Unauthorized();
            }

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("login sharafiddin_m_secret_key1234"));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user?.Phone ?? "phone"));
            claimsForToken.Add(new Claim("name", user?.Name ?? "admin"));

            var jwtSecurityToken = new JwtSecurityToken(
                "MarketUz-api", 
                "MarketUz", 
                claimsForToken, 
                DateTime.UtcNow, 
                DateTime.UtcNow.AddHours(2), 
                signingCredentials);

            var token = new JwtSecurityTokenHandler()
                .WriteToken(jwtSecurityToken);

            return Ok(token);
        }
        static User Authenticate(string login, string password)
        {
            return new User()
            {
                Login = login,
                Password = password,
                Name = "Sharafiddin",
                Phone = "+998 (88) 712 00 89"
            };
        }
    }
    class User
    {
        public string? Name { get; set; }    
        public string? Phone { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
    }
}
