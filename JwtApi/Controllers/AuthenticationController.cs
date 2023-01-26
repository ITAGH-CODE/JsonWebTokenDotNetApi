using JwtApi.Models;
using JwtApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JwtApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IJwtAuthenticationService _jwtAuthenticationService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(IJwtAuthenticationService jwtAuthenticationService, IConfiguration config)
        {
            _jwtAuthenticationService = jwtAuthenticationService;
            _configuration = config;
        }

        [HttpPost]
        public IActionResult Login([FromBody]LoginModel model)
        {
            var user = _jwtAuthenticationService.Authenticate(model.Email, model.Password);
            if(user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Username)
                };
                var token = _jwtAuthenticationService.GenereToken(_configuration["Jwt:key"], claims);
                return Ok(token);
            }

            return Unauthorized();
        }
    }
}
