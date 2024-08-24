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
         .HasKey(x => x.Id);

            modelBuilder.Entity<UserDelo>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.userDelo)
                .HasForeignKey(bc => bc.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<UserDelo>()
                .HasOne(bc => bc.UmetnickoDelo)
                .WithMany(c => c.userDelo)
                .HasForeignKey(bc => bc.DeloId)
                .IsRequired()
                .OnDelete(DeleteBehavior.SetNull);

        }



        public DbSet<Mesto> mesta { get; set; }

        public DbSet<Sala> sale { get; set; }
        public DbSet<TematskaCelina> celine { get; set; }
        public DbSet<UmetnickoDelo> umetnickaDela { get; set; }
        public DbSet<UserDelo> userDela { get; set; }
        public DbSet<Rasprodaja> rasprodaja { get; set; }

        public DbSet<Korpa> korpe { get; set; }
    }

}
