using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmetnickaDela.Data.Models;

namespace UmetnickaDela.Data
{
    public class DataContext : IdentityDbContext<User, AppRole, int>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserDelo>()
         .HasKey(x => new {x.UserId, x.DeloId});

            modelBuilder.Entity<UserDelo>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.UserDelo)
                .HasForeignKey(bc => bc.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<UserDelo>()
                .HasOne(bc => bc.UmetnickoDelo)
                .WithMany(c => c.UserDelo)
                .HasForeignKey(bc => bc.DeloId)
                .OnDelete(DeleteBehavior.ClientSetNull); 

        }



        public DbSet<Mesto> mesta { get; set; }

        public DbSet<Sala> sale { get; set; }
        public DbSet<TematskaCelina> celine { get; set; }
        public DbSet<UmetnickoDelo> umetnickaDela { get; set; }
        public DbSet<UserDelo> userDelo { get; set; }

    }

}
