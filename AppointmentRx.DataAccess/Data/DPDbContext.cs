using AppointmentRx.DataAccess.Entitites;
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
    internal class DPDbContext : IdentityDbContext<IdentityUser>
    {
        public DPDbContext(DbContextOptions<DPDbContext> options):base(options)
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

            modelBuilder.Entity<DoctorProfile>()
                .HasOne(dc => dc.Chamber)
                .WithMany(dp => dp.Profile)
                .HasForeignKey(f => f.ChamberId);

            modelBuilder.Entity<DoctorProfile>()
                .HasOne(da => da.Address)
                .WithOne(dp => dp.Profile);

            modelBuilder.Entity<DoctorProfile>()
                .HasMany(dapp => dapp.Appointment)
                .WithMany(dp => dp.Profile);

            modelBuilder.Entity<DoctorProfile>()
                .HasMany(df => df.Favorite)
                .WithMany(dp => dp.Profile);

            modelBuilder.Entity<DoctorAddress>()
                .HasOne(dp => dp.Profile)
                .WithOne(da => da.Address);

            modelBuilder.Entity<DoctorAddress>()
                .HasOne(dp => dp.Chamber)
                .WithOne(da => da.Address);

            modelBuilder.Entity<DoctorChamber>()
                .HasMany(dp => dp.Profile)
                .WithOne(dp => dp.Chamber);

            modelBuilder.Entity<DoctorChamber>()
                .HasOne(da => da.Address)
                .WithOne(dc => dc.Chamber);

            modelBuilder.Entity<DoctorChamber>()
                .HasOne(ds => ds.Scheudle)
                .WithOne(dc => dc.Chamber);

            modelBuilder.Entity<DoctorAppointment>()
                .HasMany(dp => dp.Profile)
                .WithMany(dapp => dapp.Appointment);

            modelBuilder.Entity<DoctorFavorite>()
                .HasMany(dp => dp.Profile)
                .WithMany(df => df.Favorite);





        }

        }
}