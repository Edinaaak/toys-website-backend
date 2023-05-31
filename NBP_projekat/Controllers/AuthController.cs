using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UmetnickaDela.Contracts.Models.Identity.Request;
using UmetnickaDela.Contracts.Models.Identity.Response;
using UmetnickaDela.Data.Models;

namespace NBP_projekat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper mapper;

        public AuthController(UserManager<User>  userManager, IMapper mapper)
        {
            this._userManager = userManager;
            this.mapper = mapper;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var user = mapper.Map<User>(request);
                var result = await _userManager.CreateAsync(user, request.Password);
                if (request.Role == 2)
                    await _userManager.AddToRoleAsync(user, "Slikar");
                else if (request.Role == 3)
                    await _userManager.AddToRoleAsync(user, "Ziri");
                if (!result.Succeeded)
                {
                    return BadRequest(new { message = "something went wrong" });
                }
                var userResponse = mapper.Map<UserResponse>(user);
                return Ok(userResponse);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequest request)
        {
            if(!ModelState.IsValid) 
            { 
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return BadRequest();
            if(await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("78fUjkyzfLz56gTk"));
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, request.Email)
                };

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256)
                    );
                var toReturn = new JwtSecurityTokenHandler().WriteToken(token);
                var obj = new
                {
                    expires = DateTime.Now.AddHours(1),
                    token = toReturn
                };
                return Ok(obj);

            }
            else
            {
                return BadRequest(new {msg = "Username or password is not corrected"});
            }
        }


        
    }
}
