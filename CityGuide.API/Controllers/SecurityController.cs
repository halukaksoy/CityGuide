using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CityGuide.Domain.Dtos;
using CityGuide.Domain.Entity;
using CityGuide.Domain.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CityGuide.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private ISecurityBusiness _securityBusiness;
        private IConfiguration _configuration;

        public SecurityController(ISecurityBusiness securityBusiness, IConfiguration configuration)
        {
            _securityBusiness = securityBusiness;
            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterDto model)
        {
            if (await _securityBusiness.IsUserExists(model.UserName))
            {
                ModelState.AddModelError("UserName", "Username already exists");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new User()
            {
                UserName = model.UserName
            };
            var createdUser = await _securityBusiness.Register(user, model.Password);
            return StatusCode(201);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto model)
        {
            var user = await _securityBusiness.Login(model.UserName, model.Password);
            if (user == null)
            {
                return Unauthorized();
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("AppSettings:Token").Value);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.UserName)
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new { tokenString = tokenString });
        }
    }
}