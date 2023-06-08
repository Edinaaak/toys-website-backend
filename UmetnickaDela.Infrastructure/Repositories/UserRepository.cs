using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Contracts.Models.Identity.Request;
using UmetnickaDela.Contracts.Models.Identity.Response;
using UmetnickaDela.Contracts.Models.Masterpiece.Response;
using UmetnickaDela.Data;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure.Interfaces;

namespace UmetnickaDela.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IUmetnickoDelo umetnickoRepository;

        public UserRepository(DataContext context, UserManager<User> manager, IMapper mapper, IUmetnickoDelo umetnickoRepository) : base(context)
        {
            this.userManager = manager;
            this.mapper = mapper;
            this.umetnickoRepository = umetnickoRepository;
        }

        public async Task<bool> acceptJury(int id)
        {
            //var user = await userManager.FindByIdAsync(id.ToString());
            //if (user == null) 
            //    return false;
            //user.EmailConfirmed = true;
            //string to = user.Email;
            //string from = "softnalog@gmail.com";
            //MailMessage message = new MailMessage(from, to);
            //string mailBody = $"Hi {user.Ime}, <br>" + Environment.NewLine + $"You are accepted by the adminstrator. Now, you can use our site and rate many art paintings";
            //message.Body= mailBody;
            //message.BodyEncoding= Encoding.UTF8;
            //message.IsBodyHtml= true;
            //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            //NetworkCredential basicCredential = new NetworkCredential("softnalog@gmail.com", "dcrdxdoztnlmmeae");
            //await userManager.UpdateAsync(user);
            //client.EnableSsl= true;
            //client.UseDefaultCredentials= false;
            //client.Credentials = basicCredential;
            //try
            //{
            //    client.Send(message);
            //}
            //catch(Exception ex)
            //{
            //    throw ex;
            //}
            //return true;
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return false;

            user.EmailConfirmed = true;
            string to = user.Email;
            string from = "softnalog@gmail.com";
            MailMessage message = new MailMessage(from, to);
            string mailBody = $"Hi {user.Ime}, <br>" + Environment.NewLine + $"You are accepted by the administrator. Now, you can use our site and rate many art paintings";
            message.Body = mailBody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("softnalog@gmail.com", "snbastnzdhnmddvy");

            try
            {
                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            await userManager.UpdateAsync(user);
            return true;


        }

        public async Task<bool> declineJury(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return false;
            string to = user.Email;
            string from = "softnalog@gmail.com";
            MailMessage message = new MailMessage(from, to);
            string mailBody = $"Hi {user.Ime}, <br>" + Environment.NewLine + $"You are not accepted by the administrator";
            message.Body = mailBody;
            message.BodyEncoding= Encoding.UTF8;
            message.IsBodyHtml= true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential networkCredential = new NetworkCredential("softnalog@gmail.com", "grqckbbebmgocxzy");
            client.EnableSsl= true;
            client.UseDefaultCredentials= false;
            client.Credentials = networkCredential;
            try
            {
                client.Send(message) ;
            }
            catch(Exception ex) { throw ex; }
            await userManager.DeleteAsync(user);
            return true;

        }


        public async Task<UserResponse> register(RegisterRequest request)
        {

           
            var existUser = await userManager.FindByEmailAsync(request.Email);
            if (existUser != null)
                return new UserResponse { Error = "User with this email already exists" };
            var existUserUMCN = await userManager.Users.Where(x => x.JMBG == request.JMBG).FirstOrDefaultAsync();
            if(existUserUMCN != null)
                return new UserResponse { Error = "This UMCN already exists"};
            var user = mapper.Map<User>(request);
            var result = await userManager.CreateAsync(user, request.Password);
            if(request.Role == 2)
            {
                await userManager.AddToRoleAsync(user, "Slikar");
                user.EmailConfirmed = true;
                await userManager.UpdateAsync(user);
            }
            else if(request.Role == 3)
            {
                await userManager.AddToRoleAsync(user, "Ziri");
            }
            if(!result.Succeeded)
            {
                return null;
            }
            var userMapped = mapper.Map<UserResponse>(user);
            return userMapped;
        }

        public async Task<LoginResponse> login(LoginRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return new LoginResponse { error = "User with this email does not exist" };
            if (await userManager.CheckPasswordAsync(user, request.Password))
            {
                if (!user.EmailConfirmed)
                    return new LoginResponse { error = "You are not accepted by the administrator" };
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
                var artUser = await umetnickoRepository.getMasterpieceByUser(user.Id);
                var mappedArts = mapper.Map<List<GetMasterpieceResponse>>(artUser);
                var role = await userManager.GetRolesAsync(user);
                var mappedUser = mapper.Map<UserResponse>(user);
                foreach (var m in mappedArts)
                {
                    try
                    {
                        m.Ocena = await umetnickoRepository.AverageRating(m.Id);
                    }
                    catch (Exception ex)
                    {
                        m.Ocena = 0;
                    }
                }

                return new LoginResponse
                {
                    expires = DateTime.Now.AddHours(1),
                    token = toReturn,
                    painter = mappedUser,
                    dela = mappedArts,
                    role = role,
                    error = ""
                };

            }
            else
            {
                return new LoginResponse { error = "Password is not correct" };
            }
        }

        public Task<string> forgotPassword(string email)
        {
            throw new NotImplementedException();
        }

        public Task<string> checkToken(string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> resetPassword(ResetPasswordRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponse> googleLogin(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
                return new LoginResponse { error = "User with this email does not exist" };
                if (!user.EmailConfirmed)
                    return new LoginResponse { error = "You are not accepted by the administrator" };
                var signKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("78fUjkyzfLz56gTk"));
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, email)
                };

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(1),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(signKey, SecurityAlgorithms.HmacSha256)
                    );
                var toReturn = new JwtSecurityTokenHandler().WriteToken(token);
                var artUser = await umetnickoRepository.getMasterpieceByUser(user.Id);
                var mappedArts = mapper.Map<List<GetMasterpieceResponse>>(artUser);
                var role = await userManager.GetRolesAsync(user);
                var mappedUser = mapper.Map<UserResponse>(user);
                foreach (var m in mappedArts)
                {
                    try
                    {
                        m.Ocena = await umetnickoRepository.AverageRating(m.Id);
                    }
                    catch (Exception ex)
                    {
                        m.Ocena = 0;
                    }
                }

                return new LoginResponse
                {
                    expires = DateTime.Now.AddHours(1),
                    token = toReturn,
                    painter = mappedUser,
                    dela = mappedArts,
                    role = role,
                    error = ""
                };

            
        }
    }
}
