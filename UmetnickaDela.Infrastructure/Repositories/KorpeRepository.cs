using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Contracts.Models.Korpa.Request;
using UmetnickaDela.Data;
using UmetnickaDela.Data.Models;
using UmetnickaDela.Infrastructure.Interfaces;

namespace UmetnickaDela.Infrastructure.Repositories
{
    public class KorpeRepository : Repository<Korpa>, IKorpeRepository
    {
        private readonly DataContext context;
        private readonly IMapper mapper;
        public KorpeRepository(DataContext context, IMapper mapper) : base(context, mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<bool> AddProizvodToKorpa(AddKorpaRequest addKorpaRequest)
        {
            var korpaToAdd = mapper.Map<Korpa>(addKorpaRequest);
            context.korpe.Add(korpaToAdd);
            var res = await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> ChangeQuantity(int korpaId, int quantity)
        {
            var korpaFromDb = context.korpe.Where(x => x.Id == korpaId).FirstOrDefault();
            korpaFromDb.Quantity = quantity;
            context.korpe.Update(korpaFromDb);
            var res = await context.SaveChangesAsync();
            if(res > 0)
                return true;
            return false;
        }

        public async Task<bool> DeleteKopra(int korpaId)
        {
            var korpaFromDb = await context.korpe.Where(x => x.Id == korpaId).FirstOrDefaultAsync();
            context.korpe.Remove(korpaFromDb);
            var res = await context.SaveChangesAsync();
            if (res > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<Korpa>> GetKorpaWithIncludeByUserId(int userId)
        {
            
            var products = await context.korpe.Where(x => x.UserId == userId).Include(x => x.UmetnickoDelo).ToListAsync();
            return products;
        }

        public async Task<bool> IsprazniKorpu(int userId)
        {
            var proizvodiUKorpit = await context.korpe.Where(x => x.UserId == userId).ToListAsync();
            var user = await context.Users.FindAsync(userId);
            foreach (var proizvod in proizvodiUKorpit)
                 context.korpe.Remove(proizvod);
            var res = await context.SaveChangesAsync();
            string to = user.Email;
            string from = "softnalog@gmail.com";
            MailMessage message = new MailMessage(from, to);
            string mailBody = $"Hi {user.Ime}, <br>" + Environment.NewLine + $"You ordered successfully products!";
            message.Body = mailBody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            NetworkCredential networkCredential = new NetworkCredential("edinakucevic26@gmail.com", "enevpcomkyurqudu");
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = networkCredential;
            try
            {
                client.Send(message);
            }
            catch (Exception ex) { throw ex; }
            return res > 0;
        }
    }
}
