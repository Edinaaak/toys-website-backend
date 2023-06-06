using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Data;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure.Interfaces;

namespace UmetnickaDela.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly UserManager<User> userManager;

        public UserRepository(DataContext context, UserManager<User> manager) : base(context)
        {
            this.userManager = manager;
        }

        public async Task<bool> acceptJury(int id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());
            if (user == null) 
                return false;
            user.EmailConfirmed = true;
            string to = user.Email;
            string from = "softnalog@gmail.com";
            MailMessage message = new MailMessage(from, to);
            string mailBody = $"Hi {user.Ime}, <br>" + Environment.NewLine + $"You are accepted bytthe adminstrator. Now, you can use our site and rate many art paintings";
            message.Body= mailBody;
            message.BodyEncoding= Encoding.UTF8;
            message.IsBodyHtml= true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential basicCredential = new NetworkCredential("softnalog@gmail.com", "jragifaviyjzvdcf");
            client.EnableSsl= true;
            client.UseDefaultCredentials= false;
            client.Credentials = basicCredential;
            await userManager.UpdateAsync(user);
            try
            {
                client.Send(message);
            }
            catch(Exception ex)
            {
                throw ex;
            }
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
            NetworkCredential networkCredential = new NetworkCredential("softnalog@gmail.com", "jragifaviyjzvdcf");
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
    }
}
