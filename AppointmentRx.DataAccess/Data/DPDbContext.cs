using AppointmentRx.DataAccess.Entitites;
using AppointmentRx.DataAccess.Entitites.AuthModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentRx.DataAccess.Data
{
    public class DPDbContext : IdentityDbContext<IdentityUser>
    {
        public DPDbContext(DbContextOptions<DPDbContext> options) : base(options)
        {

        }

        DbSet<DoctorProfile> Profile { get; set; }
        DbSet<DoctorAddress> Addresse { get; set; }
        DbSet<DoctorAppointment> Appointment { get; set; }
        DbSet<DoctorChamber> Chamber { get; set; }
        DbSet<DoctorFavorite> Favorite { get; set; }
        DbSet<DoctorScheudle> Scheudle { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DoctorChamber>()
                .HasOne(dp => dp.Profile)
                .WithMany(dc => dc.Chamber)
                .HasForeignKey(dpi => dpi.ProfileId);

            modelBuilder.Entity<DoctorProfile>()
                .HasMany(dp => dp.Chamber)
                .WithOne(dc => dc.Profile);

            modelBuilder.Entity<DoctorChamber>()
                .HasOne(da => da.Address)
                .WithOne(dc => dc.Chamber);

            modelBuilder.Entity<DoctorAddress>()
                .HasOne(dc => dc.Chamber)
                .WithOne(ds => ds.Address);

            modelBuilder.Entity<DoctorChamber>()
                .HasOne(ds => ds.Scheudle)
                .WithOne(dc => dc.Chamber);

            modelBuilder.Entity<DoctorScheudle>()
                .HasOne(dc => dc.Chamber)
                .WithOne(ds => ds.Scheudle);

            modelBuilder.Entity<DoctorAppointment>()
                 .HasOne(dp => dp.Profile)
                 .WithMany(da => da.Appointment)
                 .HasForeignKey(f => f.ProfileId);

            modelBuilder.Entity<DoctorProfile>()
                .HasMany(da => da.Appointment)
                .WithOne(dp => dp.Profile);

            modelBuilder.Entity<DoctorProfile>()
                .HasOne(df => df.Favorite)
                .WithMany(dp => dp.Profile)
                .HasForeignKey(f => f.FavoriteId);

            modelBuilder.Entity<DoctorFavorite>()
                .HasMany(dp => dp.Profile)
                .WithOne(df => df.Favorite);

            modelBuilder.Entity<DoctorProfile>()
                .HasOne(u => u.Login)
                .WithOne(k => k.Profile);

            modelBuilder.Entity<Registration>()
.HasOne(b => b.Login)
.WithOne(i => i.Registration)
.HasForeignKey<Login>(b => b.RId);


            modelBuilder.Entity<Login>()
            .HasOne(b => b.Registration)
            .WithOne(i => i.Login)
            .HasForeignKey<Registration>(b => b.LId);




        }

    }
}