using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.IdentityModel.Tokens;
using NBP_projekat.Mediator.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using UmetnickaDela.Contracts.Models.Identity.Request;
using UmetnickaDela.Contracts.Models.Identity.Response;
using UmetnickaDela.Contracts.Models.Masterpiece.Response;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure;

namespace NBP_projekat.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IMediator mediator;

        public AuthController(UserManager<User> userManager, IMapper mapper, IUnitOfWork unitOfWork, IMediator mediator)
        {
            this._userManager = userManager;
            this.mediator = mediator;
        }
     


        [HttpPost("forgot-password")]
        public async Task<IActionResult> forgotPassword([FromBody]string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest(new { error = "User with this email does not exist" });
            string to = user.Email;
            string from = "softnalog@gmail.com";
            MailMessage message = new MailMessage(from, to);
            string mailbody = $"Hi {user.Ime}, \n" + Environment.NewLine + $"Click here to change your password: http://localhost:4200/change-password/{user.SecurityStamp}";
            message.Body = mailbody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential basicCredential = new NetworkCredential("softnalog@gmail.com", "jragifaviyjzvdcf");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = basicCredential;
            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(true);




        }

        [HttpPost("check-token")]
        public async Task<IActionResult> checkToken([FromBody] string token)
        {
            var user = await _userManager.Users.Where( x => x.SecurityStamp == token ).FirstOrDefaultAsync();
            if (user == null)
                return BadRequest(new { error = "User with this token does not exist" });
            return Ok(true);
                
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> resetPassword(ResetPasswordRequest request)
        {
            var user =  await _userManager.Users.Where(x => x.SecurityStamp == request.token).FirstOrDefaultAsync();
            var tokenReset = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await  _userManager.ResetPasswordAsync(user, tokenReset, request.newPassword);
            return Ok(result);
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> register(RegisterRequest request)
        {
            var result = await mediator.Send(new UserRegisterCommand(request));
            if (!result.IsSucces)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> login (LoginRequest request)
        {
            
            var result = await mediator.Send(new UserLoginCommand(request));
            if(!result.IsSucces)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

        [HttpPost]
        [Route("google-login")]
        public async Task<IActionResult> googleLogin([FromBody]string email)
        {

            var result = await mediator.Send(new UserGoogleLoginCommand(email));
            if (!result.IsSucces)
                return BadRequest(result.Errors.FirstOrDefault());
            return Ok(result.Data);
        }

    }
}
